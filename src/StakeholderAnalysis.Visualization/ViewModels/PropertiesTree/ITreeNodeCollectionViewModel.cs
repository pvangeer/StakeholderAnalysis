using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface ITreeNodeCollectionViewModel : ITreeNodeViewModel
    {
        ObservableCollection<ITreeNodeViewModel> Items { get; }
        CollectionType CollectionType { get; }
    }

    public enum CollectionType
    {
        PropertyValue,
        PropertyItemsCollection
    }
}