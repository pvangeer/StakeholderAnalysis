using System;
using System.Windows.Input;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class EscapeCurrentActionCommand : ICommand
    {
        private readonly StakeholderAnalysisGui gui;

        public EscapeCurrentActionCommand(StakeholderAnalysisGui gui)
        {
            this.gui = gui;
        }

        public bool CanExecute(object parameter)
        {
            return gui.IsMagnifierActive || gui.IsSaveToImage;
        }

        public void Execute(object parameter)
        {
            if (gui.IsMagnifierActive)
            {
                gui.IsMagnifierActive = false;
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsMagnifierActive));
            }

            if (gui.IsSaveToImage)
            {
                gui.IsSaveToImage = false;
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}