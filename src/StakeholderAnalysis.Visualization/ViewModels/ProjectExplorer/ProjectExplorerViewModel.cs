using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.Properties;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerViewModel : PropertiesCollectionViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;

        public ProjectExplorerViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            this.gui = gui;
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
                    FindAndSelectObjects();
                    break;
            }
        }

        private void FindAndSelectObjects()
        {
            foreach (var item in Items)
            {
                IsSelectObjectRecursively(item);
            }
        }

        private void IsSelectObjectRecursively(ITreeNodeViewModel viewModel)
        {
            if (viewModel.IsSelected != viewModel.IsViewModelFor(gui.SelectionManager.Selection))
            {
                viewModel.IsSelected = !viewModel.IsSelected;
                viewModel.OnPropertyChanged(nameof(viewModel.IsSelected));
            }

            if (viewModel is ITreeNodeCollectionViewModel collection)
            {
                foreach (var collectionItem in collection.Items)
                {
                    IsSelectObjectRecursively(collectionItem);
                }
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }

        public override CollectionType CollectionType => CollectionType.PropertyItemsCollection;
    }
}