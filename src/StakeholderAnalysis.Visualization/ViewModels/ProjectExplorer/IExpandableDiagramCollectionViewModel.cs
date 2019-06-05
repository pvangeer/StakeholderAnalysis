using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    // TODO: Merge these two interfaces and simplify the associated DataTemplates
    public interface IExpandableDiagramCollectionViewModel : IExpandableContentViewModel
    {
        ObservableCollection<IProjectExplorerDiagramViewModel> Diagrams { get; }

        ICommand AddNewDiagramCommand { get; }
    }
}
