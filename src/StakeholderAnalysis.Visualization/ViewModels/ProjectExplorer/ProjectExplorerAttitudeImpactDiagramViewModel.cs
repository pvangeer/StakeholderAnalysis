using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerAttitudeImpactDiagramViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly Analysis analysis;
        private readonly AttitudeImpactDiagram diagram;
        private readonly ViewManager viewManager;
        private bool isExpanded;

        public ProjectExplorerAttitudeImpactDiagramViewModel(ViewModelFactory factory, Analysis analysis,
            AttitudeImpactDiagram attitudeImpactDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = attitudeImpactDiagram;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<AttitudeImpactDiagram>(diagram, nameof(AttitudeImpactDiagram.Name),
                    "Naam")
            };

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>
            {
                ViewModelFactory.CreateDuplicateMenuItemViewModel(diagram,
                    CommandFactory.CreateCanAlwaysExecuteActionCommand(
                        p => { analysis.AttitudeImpactDiagrams.Add(diagram.Clone() as AttitudeImpactDiagram); }))
            };

            if (diagram != null) diagram.PropertyChanged += DiagramPropertyChanged;
        }

        public string DisplayName => diagram.Name;

        public bool CanRemove => true;

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager?.Views.FirstOrDefault(vi =>
                vi.ViewModel is AttitudeImpactDiagramViewModel diagramViewModel1 &&
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
                v.ViewModel is AttitudeImpactDiagramViewModel diagramViewModel &&
                diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo(diagram.Name, ViewModelFactory.CrateAttitudeImpactDiagramViewModel(diagram),
                    IconSourceString);
                viewManager.OpenView(viewInfo);
            }

            viewManager.BringToFront(viewInfo);
        });

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public string IconSourceString =>
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/involvement.png";

        public bool IsViewModelFor(object otherObject)
        {
            return otherObject as AttitudeImpactDiagram == diagram;
        }

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
                case nameof(ForceFieldDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }
    }
}