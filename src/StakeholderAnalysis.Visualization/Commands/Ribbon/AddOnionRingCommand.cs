using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.Ribbon
{
    public class AddOnionRingCommand : ICommand
    {
        private readonly OnionDiagram onionDiagram;

        public AddOnionRingCommand(OnionDiagram onionDiagram)
        {
            this.onionDiagram = onionDiagram;
        }

        public bool CanExecute(object parameter)
        {
            return onionDiagram != null;
        }

        public void Execute(object parameter)
        {
            var percentage = 1.0;
            var onionRingsSmallerThanOne = onionDiagram.OnionRings.Where(r => r.Percentage < 1.0).Select(r => r.Percentage).ToArray();
            if (onionRingsSmallerThanOne.Any())
            {
                percentage = (onionRingsSmallerThanOne.Max() + 1.0) / 2.0;
            }
            else if (onionDiagram.OnionRings.Count != onionRingsSmallerThanOne.Length)
            {
                percentage = 0.5;
            }
            onionDiagram.OnionRings.Add(new OnionRing(percentage));
        }

        public event EventHandler CanExecuteChanged;
    }
}
