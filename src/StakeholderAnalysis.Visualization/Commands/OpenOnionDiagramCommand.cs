using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class OpenOnionDiagramCommand : ICommand
    {
        private readonly OnionDiagramViewModel onionDiagramViewModel;

        public OpenOnionDiagramCommand(OnionDiagramViewModel onionDiagramViewModel)
        {
            this.onionDiagramViewModel = onionDiagramViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            onionDiagramViewModel.OpenDiagramInDocumentView();
            
        }

        public event EventHandler CanExecuteChanged;
    }
}
