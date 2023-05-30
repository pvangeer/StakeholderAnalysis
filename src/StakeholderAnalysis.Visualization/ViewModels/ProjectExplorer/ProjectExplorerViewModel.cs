using System.Collections.ObjectModel;
using System.ComponentModel;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.Properties;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : PropertiesCollectionViewModelBase
    {
        public ProjectExplorerViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                ViewModelFactory.CreateProjectExplorerOnionDiagramsViewModel(),
                ViewModelFactory.CreateProjectExplorerForceFieldDiagramsViewModel(),
                ViewModelFactory.CreateProjectExplorerAttitudeImpactDiagramsViewModel(),
                ViewModelFactory.CreateProjectExplorerStakeholderOverviewTableViewModel(),
                ViewModelFactory.CreateProjectExplorerStakeholderTypesViewModel()
            };
            gui.SelectionManager.PropertyChanged += SelectionManagerPropertyChanged;
        }

        public override ObservableCollection<ITreeNodeViewModel> Items { get; }

        private void SelectionManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectionManager.Selection):
                    // TODO: Find corresponding viewmodel in project explorer and select this item.
                    break;
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;
    }
}