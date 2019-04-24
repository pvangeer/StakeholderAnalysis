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
        
        public MainWindowViewModel() : this(new Analysis(), new Gui.Gui()){ }

        public MainWindowViewModel(Analysis analysis, Gui.Gui gui)
        {
            this.analysis = analysis;

            Margin = 10;

            this.gui = gui;

            var onionViewInfo = new ViewInfo("UI-diagram", new OnionDiagramViewModel(this.analysis.OnionDiagrams.FirstOrDefault()));
            this.gui.ViewManager.OpenView(onionViewInfo);
            this.gui.ViewManager.OpenView(new ViewInfo("Krachtenveld", new StakeholderForcesDiagramViewModel(analysis)));
            this.gui.ViewManager.OpenView(new ViewInfo("Tabel", new StakeholderTableViewModel(analysis)));
            this.gui.ViewManager.OpenView(new ViewInfo("Impact/houding", new StakeholderAttitudeImpactDiagramViewModel(analysis)));
            this.gui.ViewManager.OpenToolWindow(new ToolWindowViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis)));
            this.gui.ViewManager.BringToFront(onionViewInfo);
            MainContentPresenterViewModel = new MainContentPresenterViewModel(this.gui);
        }

        public double Margin { get; set; }

        private Gui.Gui gui;

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(gui.ViewManager);

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }
    }
}