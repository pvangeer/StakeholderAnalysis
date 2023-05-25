using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly Analysis analysis;
        private readonly OnionDiagram diagram;
        private readonly ViewManager viewManager;
        private bool isExpanded;

        public ProjectExplorerOnionDiagramViewModel(ViewModelFactory factory, Analysis analysis,
            OnionDiagram onionDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = onionDiagram;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<OnionDiagram>(diagram, nameof(OnionDiagram.Name), "Naam"),
                new DoubleUpDownPropertyTreeNodeViewModel<OnionDiagram>(diagram, nameof(OnionDiagram.Asymmetry),
                    "Asymmetrie", 0, 1, 0.1, "0.#####"),
                new SliderPropertyTreeNodeViewModel<OnionDiagram>(diagram, nameof(OnionDiagram.Orientation), "Orientatie", 0, 360)
            };

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>
            {
                ViewModelFactory.CreateDuplicateMenuItemViewModel(diagram,
                    CommandFactory.CreateCanAlwaysExecuteActionCommand(
                        p => { analysis.OnionDiagrams.Add(diagram.Clone() as OnionDiagram); }))
            };

            if (diagram != null) diagram.PropertyChanged += DiagramPropertyChanged;
        }

        public string DisplayName => diagram.Name;

        public bool CanRemove => true;

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi =>
                vi.ViewModel is OnionDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null) viewManager.CloseView(viewInfo);
            analysis.OnionDiagrams.Remove(diagram);
        });

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => true;

        public ICommand OpenViewCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is OnionDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                var onionDiagramViewModel = ViewModelFactory.CreateOnionDiagramViewModel(diagram);
                viewInfo = new ViewInfo(diagram.Name, onionDiagramViewModel, IconSourceString);
                viewManager.OpenView(viewInfo);
            }

            viewManager.BringToFront(viewInfo);
        });

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object otherObject)
        {
            return otherObject as OnionDiagram == diagram;
        }

        public string IconSourceString =>
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png";

        public bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }
    }
}