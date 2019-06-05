using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    // TODO: Move generic interfaces to one namespace
    public interface IExpandableContentViewModel
    {
        string DisplayName { get; }

        bool IsExpanded { get; set; }

        ICommand ToggleIsExpandedCommand { get; }
    }
}
