using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : PropertyChangedElement, ISelectionRegister
    {
        private StakeholderViewModel selectedStakeholder;
        public MainWindowViewModel() : this(new Analysis()) { }

        public MainWindowViewModel(Analysis analysis)
        {
            Analysis = analysis;
            Analysis.Onion.Rings.CollectionChanged += RingsCollectionChanged;
            Analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
            Analysis.Connections.CollectionChanged += ConnectorsCollectionChanged;

            Asymmetry = 0.7;

            OnionRings = new ObservableCollection<OnionRingViewModel>(Analysis.Onion.Rings.Select(r => new OnionRingViewModel(r)));
            Stakeholders = new ObservableCollection<StakeholderViewModel>(Analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, this)));
            StakeholderConnections = new ObservableCollection<StakeholderConnectionViewModel>(Analysis.Connections.Select(c => new StakeholderConnectionViewModel(c)));
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
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(sh => sh.Stakeholder == stakeholder));
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

        public void Select(object o)
        {
            if (o is StakeholderViewModel stakeholderViewModel)
            {
                SelectedStakeholder = stakeholderViewModel;
            }
        }
    }
}