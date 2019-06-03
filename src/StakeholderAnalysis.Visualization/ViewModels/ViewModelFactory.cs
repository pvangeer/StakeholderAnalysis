using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewModelFactory
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly ViewManager viewManager;
        private readonly CommandFactory commandFactory;

        public ViewModelFactory(StakeholderAnalysisGui gui, ViewManager viewManager)
        {
            this.gui = gui;
            this.viewManager = viewManager;
            this.commandFactory = new CommandFactory(gui.Analysis);
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
    }
}
