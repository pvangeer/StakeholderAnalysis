using System.Linq;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewModelFactory
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly ViewManager viewManager;

        public ViewModelFactory(StakeholderAnalysisGui gui, ViewManager viewManager)
        {
            this.gui = gui;
            this.viewManager = viewManager;
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
    }
}
