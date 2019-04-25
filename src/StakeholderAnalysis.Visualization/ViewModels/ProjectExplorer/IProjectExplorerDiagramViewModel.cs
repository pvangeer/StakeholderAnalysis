using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public interface IProjectExplorerDiagramViewModel
    {
        ICommand OpenViewForDiagramCommand { get; }

        ICommand RemoveDiagramCommand { get; }

        bool IsViewModelFor(object diagram);

        string IconSourceString { get; }
    }
}