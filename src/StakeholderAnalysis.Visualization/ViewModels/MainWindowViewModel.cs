using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.DataTemplates;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly OnionDiagram currentOnionDiagram;

        public MainWindowViewModel() : this(new Analysis(), new Gui.Gui()){ }

        public MainWindowViewModel(Analysis analysis, Gui.Gui gui)
        {
            this.analysis = analysis;

            Margin = 10;

            Gui = gui;

            //TODO: Move this to appropriate ModelView
            if (analysis != null)
            {
                currentOnionDiagram = this.analysis.OnionDiagrams.FirstOrDefault();
            }

            var onionViewInfo = new ViewInfo("UI-diagram", new OnionDiagramViewModel(currentOnionDiagram));
            Gui.ViewManager.OpenView(onionViewInfo);
            Gui.ViewManager.OpenView(new ViewInfo("Krachtenveld", new StakeholderForcesDiagramViewModel(analysis)));
            Gui.ViewManager.OpenView(new ViewInfo("Tabel", new StakeholderTableViewModel(analysis)));
            Gui.ViewManager.OpenView(new ViewInfo("Impact/houding", new StakeholderAttitudeImpactDiagramViewModel(analysis)));
            Gui.ViewManager.OpenToolWindow(new ToolWindowViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis)));
            Gui.ViewManager.BringToFront(onionViewInfo);
            MainContentPresenterViewModel = new MainContentPresenterViewModel(Gui);
        }

        public double Margin { get; set; }

        public Gui.Gui Gui { get; }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(Gui.ViewManager);

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }
    }
}