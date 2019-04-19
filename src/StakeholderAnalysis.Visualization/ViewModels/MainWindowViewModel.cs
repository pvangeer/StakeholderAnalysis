using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : PropertyChangedElement, ISelectionRegister
    {
        private StakeholderViewModel selectedStakeholder;
        private StakeholderViewInfo selectedViewInfo;

        public MainWindowViewModel() : this(new Analysis()) { }

        public MainWindowViewModel(Analysis analysis)
        {
            ViewList = new ObservableCollection<StakeholderViewInfo>(new[]
            {
                new StakeholderViewInfo(StakeholderViewType.Onion, this),
                new StakeholderViewInfo(StakeholderViewType.StakeholderTable, this),
                new StakeholderViewInfo(StakeholderViewType.StakeholderForces, this),
                new StakeholderViewInfo(StakeholderViewType.CommunicationStrategy, this),
            });
            SelectedViewInfo = ViewList.ElementAt(0);

            Analysis = analysis;
            Analysis.Onion.Rings.CollectionChanged += RingsCollectionChanged;
            Analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
            Analysis.Connections.CollectionChanged += ConnectorsCollectionChanged;
            Analysis.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;

            Asymmetry = 0.7;

            OnionRings = new ObservableCollection<OnionRingViewModel>(Analysis.Onion.Rings.Select(r => new OnionRingViewModel(r)));
            Stakeholders = new ObservableCollection<StakeholderViewModel>(Analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, this)));
            StakeholderConnections = new ObservableCollection<StakeholderConnectionViewModel>(Analysis.Connections.Select(c => new StakeholderConnectionViewModel(c)));
            StakeholderConnectionGroups = new ObservableCollection<ConnectionGroupViewModel>(Analysis.ConnectionGroups.Select(g => new ConnectionGroupViewModel(g)));
            Margin = 10;
        }

        public Analysis Analysis { get; }

        public ObservableCollection<OnionRingViewModel> OnionRings { get; }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

        public ObservableCollection<StakeholderConnectionViewModel> StakeholderConnections { get; }

        public double Margin { get; set; }

        public double Asymmetry { get; set; }

        public StakeholderViewModel SelectedStakeholder
        {
            get => selectedStakeholder;
            set
            {
                selectedStakeholder = value;
                OnPropertyChanged(nameof(SelectedStakeholder));
            }
        }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand ToggleView => new ToggleViewCommand(this);

        public ICommand AddStakeholderCommand => new AddStakeholderCommand(this);

        public bool IsOnionViewOpened
        {
            get => SelectedViewInfo.Type == StakeholderViewType.Onion;
            set { }
        }

        public bool IsTableViewOpened
        {
            get => SelectedViewInfo.Type == StakeholderViewType.StakeholderTable;
            set { }
        }

        public bool IsCommunicationStrategyViewOpened
        {
            get => SelectedViewInfo.Type == StakeholderViewType.CommunicationStrategy;
            set { }
        }

        public bool IsForcesViewOpened
        {
            get => SelectedViewInfo.Type == StakeholderViewType.StakeholderForces;
            set { }
        }

        public ICommand CloseApplication => new CloseApplicationCommand();

        public ObservableCollection<StakeholderViewInfo> ViewList { get; }

        public StakeholderViewInfo SelectedViewInfo
        {
            get => selectedViewInfo;
            set
            {
                selectedViewInfo = value;
                OnPropertyChanged(nameof(SelectedViewInfo));
            }
        }

        public ObservableCollection<ConnectionGroupViewModel> StakeholderConnectionGroups { get; }

        private void RingsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems.OfType<OnionRing>())
                {
                    OnionRings.Insert(OnionRings.IndexOf(OnionRings.FirstOrDefault(r => r.Percentage >= item.Percentage)),new OnionRingViewModel(item));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var onionRing in e.OldItems.OfType<OnionRing>())
                {
                    OnionRings.Remove(OnionRings.FirstOrDefault(r => r.Ring == onionRing));
                }
            }
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                {
                    Stakeholders.Add(new StakeholderViewModel(item, this));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                {
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel => viewModel.IsViewModelFor(stakeholder)));
                }
            }
        }

        private void ConnectorsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems.OfType<StakeholderConnection>())
                {
                    StakeholderConnections.Add(new StakeholderConnectionViewModel(item));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var stakeholder in e.OldItems.OfType<StakeholderConnection>())
                {
                    StakeholderConnections.Remove(StakeholderConnections.FirstOrDefault(vm => vm.StakeholderConnection == stakeholder));
                }
            }
        }

        private void ConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems.OfType<ConnectionGroup>())
                {
                    StakeholderConnectionGroups.Add(new ConnectionGroupViewModel(item));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var connectionGroup in e.OldItems.OfType<ConnectionGroup>())
                {
                    StakeholderConnectionGroups.Remove(
                        StakeholderConnectionGroups.FirstOrDefault(vm => vm.ConnectionGroup == connectionGroup));
                }
            }
        }


        public void Select(object o)
        {
            if (o is StakeholderViewModel stakeholderViewModel)
            {
                SelectedStakeholder = stakeholderViewModel;
            }
        }
    }
}