using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewModelFactory
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly CommandFactory commandFactory;

        public ViewModelFactory(StakeholderAnalysisGui gui)
        {
            this.gui = gui;
            commandFactory = new CommandFactory(gui, gui.Analysis, this);
        }

        public OnionDiagramDrawConnectionViewModel CreateOnionDiagramDrawConnectionViewModel(OnionDiagram onionDiagram)
        {
            return new OnionDiagramDrawConnectionViewModel(onionDiagram)
            {
                GetSelectedStakeholderConnectionGroup = d =>
                    gui?.SelectedStakeholderConnectionGroups?.FirstOrDefault(s => s.OnionDiagram == d)
                        ?.StakeholderConnectionGroup ?? d.ConnectionGroups.FirstOrDefault()
            };
        }

        public StakeholderTypesViewModel CreateStakeholderTypesViewModel()
        {
            return new StakeholderTypesViewModel(this, gui.Analysis);
        }

        public StakeholderTypeViewModel CreateStakeholderTypeViewModel(StakeholderType stakeholderType)
        {
            return new StakeholderTypeViewModel(this, stakeholderType,
                commandFactory.CreateRemoveStakeholderTypeCommand(stakeholderType));
        }

        public RibbonViewModel CreateRibbonViewModel()
        {
            return new RibbonViewModel(this, gui);
        }

        public MainContentPresenterViewModel CreateMainContentPresenterViewModel(StakeholderAnalysisGui stakeholderAnalysisGui)
        {
            return new MainContentPresenterViewModel(this, stakeholderAnalysisGui);
        }

        public StatusBarViewModel CreateStatusBarViewModel()
        {
            return new StatusBarViewModel(this,gui);
        }

        public CommandFactory GetCommandFactory()
        {
            return commandFactory;
        }

        public ViewManagerViewModel CreateViewManagerViewModel(ViewManager guiViewManager)
        {
            return new ViewManagerViewModel(guiViewManager);
        }

        public MessageListViewModel CreateMessageListViewModel()
        {
            return new MessageListViewModel(this,gui?.Messages);
        }
    }
}
