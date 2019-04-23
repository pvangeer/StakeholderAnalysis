using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable, ISelectionRegister
    {
        private bool isMagnifierActive;
        private bool isSaveToImage;
        private RelayCommand saveCanvasCommand;
        private StakeholderViewModel selectedStakeholder;

        public MainWindowViewModel() : this(new Analysis())
        {
        }

        public MainWindowViewModel(Analysis analysis)
        {
            ViewList = new ObservableCollection<StakeholderViewInfo>(new[]
            {
                new StakeholderViewInfo(StakeholderViewType.Onion, this),
                new StakeholderViewInfo(StakeholderViewType.StakeholderTable, this),
                new StakeholderViewInfo(StakeholderViewType.StakeholderForces, this),
                new StakeholderViewInfo(StakeholderViewType.CommunicationStrategy, this)
            });
            SelectedViewInfo = ViewList.ElementAt(0);

            Margin = 10;

            Analysis = analysis;

            // TODO: Following code should be redirected to different viewmodels (that possibly partly have the same base)
            var diagram1 = Analysis.OnionDiagrams.FirstOrDefault();
            if (diagram1 != null)
            {
                // TODO: This wireing will not work correctly: split in different viewmodels etc.
                Analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                diagram1.Stakeholders.CollectionChanged += OnionDiagramStakeholdersCollectionChanged;
                diagram1.ConnectionGroups.CollectionChanged += ConnectionGroupsCollectionChanged;
                
                Stakeholders = new ObservableCollection<StakeholderViewModel>(Analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, this)));
                OnionDiagramStakeholders = new ObservableCollection<StakeholderViewModel>(diagram1.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, this)));
                StakeholderConnectionGroups = new ObservableCollection<ConnectionGroupViewModel>(diagram1.ConnectionGroups.Select(g => new ConnectionGroupViewModel(g)));
            }
        }

        private Analysis Analysis { get; }

        public ObservableCollection<StakeholderViewModel> OnionDiagramStakeholders { get; }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

        public double Margin { get; set; }

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

        public ICommand CloseApplication => new CloseApplicationCommand();

        public ObservableCollection<StakeholderViewInfo> ViewList { get; }

        public StakeholderViewInfo SelectedViewInfo { get; set; }

        public ObservableCollection<ConnectionGroupViewModel> StakeholderConnectionGroups { get; }

        public bool IsMagnifierActive
        {
            get => isMagnifierActive;
            set
            {
                isMagnifierActive = value;
                OnPropertyChanged(nameof(IsMagnifierActive));
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                return saveCanvasCommand ?? (saveCanvasCommand = new RelayCommand(() =>
                {
                    IsSaveToImage = true;
                    IsSaveToImage = false;
                }));
            }
        }

        public bool IsSaveToImage
        {
            get => isSaveToImage;
            set
            {
                isSaveToImage = value;
                OnPropertyChanged(nameof(IsSaveToImage));
            }
        }

        public OnionRingsCanvasViewModel OnionRingsCanvasViewModel => new OnionRingsCanvasViewModel(Analysis.OnionDiagrams.FirstOrDefault());

        public OnionConnectionsPresenterViewModel OnionConnectionsPresenterViewModel => new OnionConnectionsPresenterViewModel(Analysis.OnionDiagrams.FirstOrDefault());

        public void Select(object o)
        {
            if (o is StakeholderViewModel stakeholderViewModel)
            {
                SelectedStakeholder = stakeholderViewModel;
            }
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(new StakeholderViewModel(item, this));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }

        private void OnionDiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Add(new StakeholderViewModel(item, this));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Remove(OnionDiagramStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }

        private void ConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<StakeholderConnectionGroup>())
                    StakeholderConnectionGroups.Add(new ConnectionGroupViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var connectionGroup in e.OldItems.OfType<StakeholderConnectionGroup>())
                    StakeholderConnectionGroups.Remove(
                        StakeholderConnectionGroups.FirstOrDefault(vm => vm.StakeholderConnectionGroup == connectionGroup));
        }
    }
}