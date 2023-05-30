using System.ComponentModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface IExpandable : INotifyPropertyChanged
    {
        bool IsExpandable { get; }

        bool IsExpanded { get; set; }

        ICommand ToggleIsExpandedCommand { get; }
    }
}