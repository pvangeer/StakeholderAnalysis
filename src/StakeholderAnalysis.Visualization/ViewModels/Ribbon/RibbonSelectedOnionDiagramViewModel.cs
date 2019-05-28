using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands.Ribbon;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonSelectedOnionDiagramViewModel
    {
        private readonly OnionDiagram onionDiagram;
        private readonly Analysis analysis;

        public RibbonSelectedOnionDiagramViewModel(OnionDiagram onionDiagram, Analysis analysis)
        {
            this.onionDiagram = onionDiagram;
            this.analysis = analysis;
        }

        public ICommand AddOnionRingCommand => new AddOnionRingCommand(onionDiagram);

        public double Asymmetry
        {
            get => onionDiagram.Asymmetry;
            set
            {
                onionDiagram.Asymmetry = value;
                onionDiagram.OnPropertyChanged(nameof(OnionDiagram.Asymmetry));
            }
        }
    }
}