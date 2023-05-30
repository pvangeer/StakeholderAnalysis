using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface ITreeNodeCollectionViewModel : ITreeNodeViewModel
    {
        ObservableCollection<ITreeNodeViewModel> Items { get; }

        CollectionType CollectionType { get; }
    }
}