using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly StakeholderAnalysisGui gui;

        public MainWindowViewModel() : this(new Analysis(), new StakeholderAnalysisGui()){ }

        public MainWindowViewModel(Analysis analysisInput, StakeholderAnalysisGui guiInput)
        {
            analysis = analysisInput;
            gui = guiInput;
            RibbonViewModel.ToggleToolWindowCommand.Execute(typeof(ProjectExplorerViewModel));
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel => new MainContentPresenterViewModel(gui);

        public RibbonViewModel RibbonViewModel => new RibbonViewModel(analysis,gui);

        public StatusBarViewModel StatusBarViewModel => new StatusBarViewModel(analysis, gui);
    }
}