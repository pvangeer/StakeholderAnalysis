using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public interface IExpandableDiagramCollectionViewModel : IExpandableContentViewModel
    {
        ObservableCollection<IProjectExplorerDiagramViewModel> Diagrams { get; }

        ICommand AddNewDiagramCommand { get; }
    }
}
