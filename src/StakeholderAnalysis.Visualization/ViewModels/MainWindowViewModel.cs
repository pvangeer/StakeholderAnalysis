using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
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

        public MainWindowViewModel(Analysis analysisInput, Gui.Gui guiInput)
        {
            analysis = analysisInput;
            gui = guiInput;

            // TODO: Add to Project explorer
            gui.ViewManager.OpenView(new ViewInfo("Tabel", new StakeholderTableViewModel(analysis), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/table.png"));

            // Add button in ribbon to enable/disable this window
            gui.ViewManager.OpenToolWindow(new ToolWindowViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis, gui.ViewManager), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/SaveImage.png"));
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel => new MainContentPresenterViewModel(gui);

        public RibbonViewModel RibbonViewModel => new RibbonViewModel(analysis,gui);
    }
}