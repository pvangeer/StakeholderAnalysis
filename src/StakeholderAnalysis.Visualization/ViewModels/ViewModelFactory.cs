using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;
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

        public ConnectionGroupsPropertiesViewModel CreateConnectionGroupsPropertiesViewModel()
        {
            return new ConnectionGroupsPropertiesViewModel(this, gui?.ViewManager);
        }

        public OnionDiagramPropertiesViewModel CreateOnionDiagramPropertiesViewModel()
        {
            return new OnionDiagramPropertiesViewModel(this, gui?.ViewManager);
        }

        public ProjectExplorerViewModel CreateProjectExplorerViewModel()
        {
            return new ProjectExplorerViewModel(this, gui);
        }

        public OnionRingPropertiesViewModel CreateOnionRingPropertiesViewModel(OnionRing onionRing, OnionDiagram selectedOnionDiagram)
        {
            return new OnionRingPropertiesViewModel(this, onionRing, selectedOnionDiagram);
        }

        public OnionRingsPropertiesViewModel CreateOnionRingsPropertiesViewModel()
        {
            return new OnionRingsPropertiesViewModel(this, gui.ViewManager);
        }

        public ConnectionGroupPropertiesViewModel CreateConnectionGroupPropertiesViewModel(StakeholderConnectionGroup connectionGroup, OnionDiagram selectedOnionDiagram)
        {
            return new ConnectionGroupPropertiesViewModel(this, connectionGroup, selectedOnionDiagram);
        }

        public ProjectExplorerForceFieldDiagramsViewModel CreateProjectExplorerForceFieldDiagramsViewModel(Analysis analysis)
        {
            return new ProjectExplorerForceFieldDiagramsViewModel(this, analysis);
        }

        public IProjectExplorerDiagramViewModel CreateProjectExplorerForceFieldDiagramViewModel(Analysis analysis, ForceFieldDiagram forceFieldDiagram)
        {
            return new ProjectExplorerForceFieldDiagramViewModel(this, analysis, forceFieldDiagram, gui.ViewManager);
        }

        public ProjectExplorerAttitudeImpactDiagramsViewModel CreateProjectExplorerAttitudeImpactDiagramsViewModel(Analysis guiAnalysis)
        {
            return new ProjectExplorerAttitudeImpactDiagramsViewModel(this, guiAnalysis);
        }

        public IProjectExplorerDiagramViewModel CreateProjectExplorerDiagramViewModel(Analysis analysis, AttitudeImpactDiagram forceFieldDiagram)
        {
            return new ProjectExplorerDiagramViewModel(this, analysis, forceFieldDiagram, gui.ViewManager);
        }
    }
}
