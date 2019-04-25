using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ForceFieldDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly Gui.Gui gui;

        public MainWindowViewModel() : this(new Analysis(), new Gui.Gui()){ }

        public MainWindowViewModel(Analysis analysis, Gui.Gui gui)
        {
            this.analysis = analysis;

            this.gui = gui;

            foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
            {
                this.gui.ViewManager.OpenView(new ViewInfo(forceFieldDiagram.Name, new ForceFieldDiagramViewModel(forceFieldDiagram), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/forces.png"));
            }
            foreach (var attitudeImpactDiagram in analysis.AttitudeImpactDiagrams)
            {
                this.gui.ViewManager.OpenView(new ViewInfo(attitudeImpactDiagram.Name, new AttitudeImpactDiagramViewModel(attitudeImpactDiagram), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/involvement.png"));
            }
            foreach (var onionDiagram in analysis.OnionDiagrams)
            {
                this.gui.ViewManager.OpenView(new ViewInfo(onionDiagram.Name, new OnionDiagramViewModel(onionDiagram), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png"));
            }
            this.gui.ViewManager.OpenView(new ViewInfo("Tabel", new StakeholderTableViewModel(analysis), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/table.png"));
            this.gui.ViewManager.OpenToolWindow(new ToolWindowViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis, gui.ViewManager), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/SaveImage.png"));
            MainContentPresenterViewModel = new MainContentPresenterViewModel(this.gui);
        }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(gui.ViewManager);

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }
    }
}