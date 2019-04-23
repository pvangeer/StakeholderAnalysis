using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.DataTemplates;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private bool isMagnifierActive;
        private bool isSaveToImage;
        private RelayCommand saveCanvasCommand;
        private readonly Analysis analysis;
        private readonly OnionDiagram currentOnionDiagram;

        public MainWindowViewModel() : this(new Analysis())
        {
        }

        public MainWindowViewModel(Analysis analysis)
        {
            this.analysis = analysis;

            ViewList = new ObservableCollection<StakeholderViewInfo>(new[]
            {
                new StakeholderViewInfo(StakeholderViewType.Onion, this),
                new StakeholderViewInfo(StakeholderViewType.StakeholderTable, this),
                new StakeholderViewInfo(StakeholderViewType.StakeholderForces, this),
                new StakeholderViewInfo(StakeholderViewType.AttitudeImpact, this)
            }); // TODO: Move this property and list to a viewmanager
            SelectedViewInfo = ViewList.ElementAt(0);// TODO: Move this property and list to a viewmanager

            Margin = 10;

            //TODO: Move this to separate viewinfos (as part of a ViewInfo) and the ViewManager
            if (analysis != null)
            {
                currentOnionDiagram = this.analysis.OnionDiagrams.FirstOrDefault();
            }
        }

        public double Margin { get; set; }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand ToggleView => new ToggleViewCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public ObservableCollection<StakeholderViewInfo> ViewList { get; }

        public StakeholderViewInfo SelectedViewInfo { get; set; }

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

        public OnionRingsCanvasViewModel OnionRingsCanvasViewModel => new OnionRingsCanvasViewModel(currentOnionDiagram);

        public OnionConnectionsPresenterViewModel OnionConnectionsPresenterViewModel => new OnionConnectionsPresenterViewModel(currentOnionDiagram);

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel => new OnionDiagramStakeholdersViewModel(currentOnionDiagram);

        public StakeholderTableViewModel StakeholderTableViewModel => new StakeholderTableViewModel(analysis);

        public StakeholderForcesDiagramViewModel StakeholderForcesDiagramViewModel => new StakeholderForcesDiagramViewModel(analysis);

        public StakeholderAttitudeImpactDiagramViewModel StakeholderAttitudeImpactDiagramViewModel => new StakeholderAttitudeImpactDiagramViewModel(analysis);

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(currentOnionDiagram);
    }
}