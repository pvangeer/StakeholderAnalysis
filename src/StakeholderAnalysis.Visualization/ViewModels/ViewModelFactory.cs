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
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;
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

        private ViewManager ViewManager => gui?.ViewManager;
        private Analysis Analysis => gui?.Analysis;

        public ViewModelFactory(StakeholderAnalysisGui gui)
        {
            this.gui = gui;
            commandFactory = new CommandFactory(gui, this);
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
            return new StakeholderTypesViewModel(this, Analysis);
        }

        public StakeholderTypeViewModel CreateStakeholderTypeViewModel(StakeholderType stakeholderType)
        {
            return new StakeholderTypeViewModel(this, stakeholderType);
        }

        public RibbonViewModel CreateRibbonViewModel()
        {
            return new RibbonViewModel(this, gui);
        }

        public MainContentPresenterViewModel CreateMainContentPresenterViewModel()
        {
            return new MainContentPresenterViewModel(this, gui);
        }

        public StatusBarViewModel CreateStatusBarViewModel()
        {
            return new StatusBarViewModel(this,gui);
        }

        public CommandFactory GetCommandFactory()
        {
            return commandFactory;
        }

        public ViewManagerViewModel CreateViewManagerViewModel()
        {
            return new ViewManagerViewModel(this, ViewManager);
        }

        public MessageListViewModel CreateMessageListViewModel()
        {
            return new MessageListViewModel(this,gui?.Messages);
        }

        public ConnectionGroupsPropertiesViewModel CreateConnectionGroupsPropertiesViewModel()
        {
            return new ConnectionGroupsPropertiesViewModel(this, ViewManager);
        }

        public OnionDiagramPropertiesViewModel CreateOnionDiagramPropertiesViewModel()
        {
            return new OnionDiagramPropertiesViewModel(this, ViewManager);
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
            return new OnionRingsPropertiesViewModel(this, ViewManager);
        }

        public ConnectionGroupPropertiesViewModel CreateConnectionGroupPropertiesViewModel(StakeholderConnectionGroup connectionGroup, OnionDiagram selectedOnionDiagram)
        {
            return new ConnectionGroupPropertiesViewModel(this, connectionGroup, selectedOnionDiagram);
        }

        public ProjectExplorerForceFieldDiagramsViewModel CreateProjectExplorerForceFieldDiagramsViewModel()
        {
            return new ProjectExplorerForceFieldDiagramsViewModel(this, Analysis);
        }

        public ITreeNodeViewModel CreateProjectExplorerForceFieldDiagramViewModel(ForceFieldDiagram forceFieldDiagram)
        {
            return new ProjectExplorerForceFieldDiagramViewModel(this, Analysis, forceFieldDiagram, ViewManager);
        }

        public ProjectExplorerAttitudeImpactDiagramsViewModel CreateProjectExplorerAttitudeImpactDiagramsViewModel()
        {
            return new ProjectExplorerAttitudeImpactDiagramsViewModel(this, Analysis);
        }

        public ITreeNodeViewModel CreateProjectExplorerDiagramViewModel(AttitudeImpactDiagram forceFieldDiagram)
        {
            return new ProjectExplorerAttitudeImpactDiagramViewModel(this, Analysis, forceFieldDiagram, ViewManager);
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

        public OnionDiagramStakeholdersViewModel CreateOnionDiagramStakeholdersViewModel(OnionDiagram onionDiagram, ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler)
        {
            return new OnionDiagramStakeholdersViewModel(this, onionDiagram, selectionRegister, drawConnectionHandler);
        }

        public OnionDiagramConnectionsPresenterViewModel CreateOnionDiagramConnectionsPresenterViewModel(OnionDiagram diagram, ISelectionRegister selectionRegister)
        {
            return new OnionDiagramConnectionsPresenterViewModel(this, diagram, selectionRegister);
        }

        public OnionDiagramRingsCanvasViewModel CreateOnionDiagramRingsCanvasViewModel(OnionDiagram diagram)
        {
            return new OnionDiagramRingsCanvasViewModel(this, diagram);
        }

        public OnionRingViewModel CreateOnionRingViewModel(OnionRing onionRing)
        {
            return new OnionRingViewModel(this, onionRing);
        }

        public StakeholderConnectionViewModel CreateStakeholderConnectionViewModel(StakeholderConnection stakeholderConnection, ISelectionRegister selectionRegister)
        {
            return new StakeholderConnectionViewModel(this, stakeholderConnection, selectionRegister);
        }

        public ProjectExplorerStakeholderOverviewTableViewModel CreateProjectExplorerStakeholderOverviewTableViewModel()
        {
            return new ProjectExplorerStakeholderOverviewTableViewModel(this, ViewManager);
        }

        public ProjectExplorerOnionDiagramsViewModel CreateProjectExplorerOnionDiagramsViewModel()
        {
            return new ProjectExplorerOnionDiagramsViewModel(this, Analysis);
        }

        public OnionDiagramViewModel CreateOnionDiagramViewModel(OnionDiagram diagram)
        {
            return new OnionDiagramViewModel(this, diagram);
        }

        public ITreeNodeViewModel CreateProjectExplorerOnionDiagramViewModel(OnionDiagram analysisOnionDiagram)
        {
            return new ProjectExplorerOnionDiagramViewModel(this, Analysis, analysisOnionDiagram, ViewManager);
        }

        public StakeholderTableViewModel CreateStakeholderTableViewModel()
        {
            return new StakeholderTableViewModel(this, Analysis);
        }
    }
}
