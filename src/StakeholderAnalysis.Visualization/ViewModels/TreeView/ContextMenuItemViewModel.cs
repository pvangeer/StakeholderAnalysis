using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public class ContextMenuItemViewModel
    {
        public bool IsEnabled { get; set; }

        public string Header { get; set; }

        public ICommand Command { get; set; }

        public string IconReference { get; set; }
    }
}