using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerAttitudeImpactDiagramsViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded = true;

        public ProjectExplorerAttitudeImpactDiagramsViewModel(ViewModelFactory factory, Analysis analysis) :
            base(factory)
        {
            this.analysis = analysis;
            analysis.AttitudeImpactDiagrams.CollectionChanged += AttitudeImpactDiagramsCollectionChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>();
            foreach (var forceFieldDiagram in analysis.AttitudeImpactDiagrams)
                Items.Add(ViewModelFactory.CreateProjectExplorerDiagramViewModel(forceFieldDiagram));

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyItemsCollection;

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

        public bool CanAdd => true;

        public ICommand AddItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            analysis.AttitudeImpactDiagrams.Add(new AttitudeImpactDiagram("Nieuw diagram"));
        });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItem { get; }

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
        {
            return false;
        }

        public string DisplayName => "Houding - impact";

        public string IconSourceString { get; }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        private void AttitudeImpactDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var attitudeImpactDiagram in e.NewItems.OfType<AttitudeImpactDiagram>())
                {
                    var projectExplorerDiagramViewModel = ViewModelFactory.CreateProjectExplorerDiagramViewModel(attitudeImpactDiagram);
                    Items.Add(projectExplorerDiagramViewModel);
                    if (IsExpanded)
                        projectExplorerDiagramViewModel.SelectItem?.Execute(null);
                }

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var attitudeImpactDiagram in e.OldItems.OfType<AttitudeImpactDiagram>())
                {
                    var diagramToRemove = Items.FirstOrDefault(d => d.IsViewModelFor(attitudeImpactDiagram));
                    if (diagramToRemove != null) Items.Remove(diagramToRemove);
                }
        }
    }
}