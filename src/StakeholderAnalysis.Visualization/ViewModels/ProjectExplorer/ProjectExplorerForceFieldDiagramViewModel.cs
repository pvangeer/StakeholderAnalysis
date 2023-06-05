﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerForceFieldDiagramViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly Analysis analysis;
        private readonly ForceFieldDiagram diagram;
        private readonly ViewManager viewManager;

        public ProjectExplorerForceFieldDiagramViewModel(ViewModelFactory factory, Analysis analysis,
            ForceFieldDiagram forceFieldDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = forceFieldDiagram;

            Items = new ObservableCollection<ITreeNodeViewModel>();

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>
            {
                // TODO: Add Openen, Verwijderen.. also for other projectexplorer viewmodels.
                ViewModelFactory.CreateDuplicateMenuItemViewModel(diagram,
                    CommandFactory.CreateCanAlwaysExecuteActionCommand(
                        p => { analysis.ForceFieldDiagrams.Add(diagram.Clone() as ForceFieldDiagram); }))
            };
            SelectItemCommand = CommandFactory.CreateSelectItemCommand(this);

            if (diagram != null) diagram.PropertyChanged += DiagramPropertyChanged;
        }

        public string DisplayName => diagram.Name;

        public bool CanRemove => true;

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi =>
                vi.ViewModel is ForceFieldDiagramViewModel diagramViewModel1 &&
                diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null) viewManager.CloseView(viewInfo);
            analysis.ForceFieldDiagrams.Remove(diagram);
        });

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => true;

        public ICommand OpenViewCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is ForceFieldDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, ViewModelFactory.CreateForceFieldDiagramViewModel(diagram),
                    IconSourceString);
                viewManager.OpenView(viewInfo);
            }

            viewManager.BringToFront(viewInfo);
        });

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand { get; }

        public object GetSelectableObject()
        {
            return diagram;
        }

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public string IconSourceString =>
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/forces.png";

        public override bool IsViewModelFor(object otherObject)
        {
            return otherObject as ForceFieldDiagram == diagram;
        }

        public bool IsExpandable => false;

        public bool IsExpanded { get; set; }

        public ICommand ToggleIsExpandedCommand => null;

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ForceFieldDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }

        public TwoAxisDiagramPropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateTwoAxisDiagramPropertiesViewModel(diagram);
        }
    }
}