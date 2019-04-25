using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public interface IExpandableContentGroup
    {
        bool IsExpanded { get; set; }

        ICommand ToggleElementsCommand { get; }

        ICommand AddElementCommand { get; }

        string Name { get; }

    }
}
