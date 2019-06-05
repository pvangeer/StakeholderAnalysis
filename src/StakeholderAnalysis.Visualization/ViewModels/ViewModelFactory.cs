using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView;
using StakeholderAnalysis.Visualization.ViewModels.StatusBar;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

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
            return new OnionDiagramDrawConnectionViewModel(this, onionDiagram)
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
            return new ViewManagerViewModel(this, guiViewManager);
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

        public AttitudeImpactDiagramViewModel CrateAttitudeImpactDiagramViewModel(AttitudeImpactDiagram diagram)
        {
            return new AttitudeImpactDiagramViewModel(this, diagram);
        }

        public ForceFieldDiagramViewModel CreateForceFieldDiagramViewModel(ForceFieldDiagram diagram)
        {
            return new ForceFieldDiagramViewModel(this, diagram);
        }

        public AttitudeImpactDiagramStakeholderViewModel CreateAttitudeImpactDiagramStakeholderViewModel(AttitudeImpactDiagram diagram, AttitudeImpactDiagramStakeholder stakeholder, ISelectionRegister selectionRegister)
        {
            return new AttitudeImpactDiagramStakeholderViewModel(this, diagram, stakeholder, selectionRegister);
        }

        public StakeholderViewModel CreateStakeholderViewModel(Stakeholder stakeholder, ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler)
        {
            return new StakeholderViewModel(this, stakeholder, selectionRegister, drawConnectionHandler);
        }

        public ForceFieldDiagramStakeholderViewModel CreateForceFieldDiagramStakeholderViewModel(ForceFieldDiagram diagram, ForceFieldDiagramStakeholder stakeholder, ISelectionRegister selectionRegister)
        {
            return new ForceFieldDiagramStakeholderViewModel(this, diagram, stakeholder, selectionRegister);
        }

        public OnionDiagramStakeholderViewModel CreateOnionDiagramStakeholderViewModel(OnionDiagram diagram, OnionDiagramStakeholder stakeholder, ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler)
        {
            return new OnionDiagramStakeholderViewModel(this, diagram, stakeholder, selectionRegister, drawConnectionHandler);
        }

        public OnionDiagramStakeholdersViewModel CreateOnionDiagramStakeholdersViewModel(OnionDiagram onionDiagram, OnionDiagramViewModel onionDiagramViewModel, IDrawConnectionHandler drawConnectionHandler)
        {
            return new OnionDiagramStakeholdersViewModel(this, onionDiagram, onionDiagramViewModel, drawConnectionHandler);
        }

        public OnionDiagramConnectionsPresenterViewModel CreateOnionDiagramConnectionsPresenterViewModel(OnionDiagram diagram)
        {
            return new OnionDiagramConnectionsPresenterViewModel(this, diagram);
        }

        public OnionDiagramRingsCanvasViewModel CreateOnionDiagramRingsCanvasViewModel(OnionDiagram diagram)
        {
            return new OnionDiagramRingsCanvasViewModel(this, diagram);
        }

        public OnionRingViewModel CreateOnionRingViewModel(OnionRing onionRing)
        {
            return new OnionRingViewModel(this, onionRing);
        }

        public StakeholderConnectionViewModel CreateStakeholderConnectionViewModel(StakeholderConnection stakeholderConnection)
        {
            return new StakeholderConnectionViewModel(this, stakeholderConnection);
        }

        public ProjectExplorerStakeholderOverviewTableViewModel CreateProjectExplorerStakeholderOverviewTableViewModel(Analysis guiAnalysis)
        {
            return new ProjectExplorerStakeholderOverviewTableViewModel(this, guiAnalysis, gui.ViewManager);
        }

        public ProjectExplorerOnionDiagramsViewModel CreateProjectExplorerOnionDiagramsViewModel(Analysis guiAnalysis)
        {
            return new ProjectExplorerOnionDiagramsViewModel(this, guiAnalysis, gui.ViewManager);
        }

        public OnionDiagramViewModel CreateOnionDiagramViewModel(OnionDiagram diagram)
        {
            return new OnionDiagramViewModel(this, diagram);
        }

        public IProjectExplorerDiagramViewModel CreateProjectExplorerOnionDiagramViewModel(Analysis analysis, OnionDiagram analysisOnionDiagram)
        {
            return new ProjectExplorerOnionDiagramViewModel(this, analysis, analysisOnionDiagram, gui?.ViewManager);
        }

        public StakeholderTableViewModel CreateStakeholderTableViewModel(Analysis analysis)
        {
            return new StakeholderTableViewModel(this, analysis);
        }
    }
}
