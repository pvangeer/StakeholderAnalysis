using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerTwoAxisDiagramViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly Analysis analysis;
        private readonly TwoAxisDiagram diagram;
        private readonly DiagramType diagramType;
        private readonly ViewManager viewManager;

        public ProjectExplorerTwoAxisDiagramViewModel(ViewModelFactory factory, Analysis analysis,
            TwoAxisDiagram twoAxisDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = twoAxisDiagram;
            diagramType = analysis.ForceFieldDiagrams.Contains(diagram) ? DiagramType.ForceFieldDiagram : DiagramType.AttitudeImpactDiagram;

            Items = new ObservableCollection<ITreeNodeViewModel>();

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>
            {
                ViewModelFactory.CreateDuplicateMenuItemViewModel(diagram,
                    CommandFactory.CreateCanAlwaysExecuteActionCommand(
                        p =>
                        {
                            switch (diagramType)
                            {
                                case DiagramType.AttitudeImpactDiagram:
                                    analysis.AttitudeImpactDiagrams.Add(diagram.Clone() as TwoAxisDiagram);
                                    break;
                                case DiagramType.ForceFieldDiagram:
                                    analysis.ForceFieldDiagrams.Add(diagram.Clone() as TwoAxisDiagram);
                                    break;
                            }
                        }))
            };

            SelectItemCommand = CommandFactory.CreateSelectItemCommand(this);
            if (diagram != null) diagram.PropertyChanged += DiagramPropertyChanged;
        }

        public string DisplayName => diagram.Name;

        public bool CanRemove => true;

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi =>
                vi.ViewModel is TwoAxisDiagramViewModel diagramViewModel1 &&
                diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null) viewManager.CloseView(viewInfo);
            analysis.AttitudeImpactDiagrams.Remove(diagram);
        });

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => true;

        public ICommand OpenViewCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is TwoAxisDiagramViewModel diagramViewModel &&
                diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, ViewModelFactory.CrateAttitudeImpactDiagramViewModel(diagram),
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

        public string IconSourceString => diagramType == DiagramType.AttitudeImpactDiagram
            ? "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/involvement.ico"
            : "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Diagrams/forces.ico";

        public override bool IsViewModelFor(object otherObject)
        {
            return otherObject as TwoAxisDiagram == diagram;
        }

        public bool IsExpandable => false;

        public bool IsExpanded { get; set; }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(TwoAxisDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }

        public TwoAxisDiagramPropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateTwoAxisDiagramPropertiesViewModel(diagram);
        }
    }

    public enum DiagramType
    {
        ForceFieldDiagram,
        AttitudeImpactDiagram
    }
}