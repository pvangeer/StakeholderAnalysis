using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class StakeholderTypesViewModel : ViewModelBase, IExpandableContentViewModel
    {
        private readonly Analysis analysis;
        private bool isExpanded;

        public StakeholderTypesViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            if (analysis != null)
            {
                StakeholderTypes = new ObservableCollection<StakeholderTypeViewModel>(analysis?.StakeholderTypes.Select(st => ViewModelFactory.CreateStakeholderTypeViewModel(st)));
                analysis.StakeholderTypes.CollectionChanged += StakeholderTypesCollectionChanged;
            }
            else
            {
                StakeholderTypes = new ObservableCollection<StakeholderTypeViewModel>();
            }

        }

        public string DisplayName => "Stakeholder types";

        public bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand AddNewContentCommand => CommandFactory.CreateAddNewStakeholderTypeCommand(analysis);

        public ObservableCollection<StakeholderTypeViewModel> StakeholderTypes { get; }

        private void StakeholderTypesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var stakeholderType in e.NewItems.OfType<StakeholderType>())
                    {
                        StakeholderTypes.Add(ViewModelFactory.CreateStakeholderTypeViewModel(stakeholderType));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderType in e.OldItems.OfType<StakeholderType>())
                    {
                        var viewModelToRemove = StakeholderTypes.FirstOrDefault(viewModel => viewModel.IsViewModelFor(stakeholderType));
                        if (viewModelToRemove != null)
                        {
                            StakeholderTypes.Remove(viewModelToRemove);
                        }
                    }
                    break;
            }
        }
    }
}
