using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public interface IExpandableContentGroupViewModel : IExpandableContentViewModel
    {
        ObservableCollection<IProjectExplorerDiagramViewModel> Diagrams { get; }

        ICommand ToggleIsExpandedCommand { get; }

        ICommand AddNewDiagramCommand { get; }

        string Name { get; }

        void AddNewDiagram();
    }
}
