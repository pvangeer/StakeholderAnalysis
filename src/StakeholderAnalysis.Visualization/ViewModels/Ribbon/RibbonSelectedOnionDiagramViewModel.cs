using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonSelectedOnionDiagramViewModel
    {
        private readonly OnionDiagram onionDiagram;

        public RibbonSelectedOnionDiagramViewModel(OnionDiagram onionDiagram)
        {
            this.onionDiagram = onionDiagram;
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