using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public interface IExpandableContentViewModel
    {
        string DisplayName { get; }

        bool IsExpanded { get; set; }

        ICommand ToggleIsExpandedCommand { get; }
    }
}
