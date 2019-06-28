using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface IPropertyCollectionTreeNodeViewModel : ITreeNodeViewModel
    {
        ObservableCollection<ITreeNodeViewModel> Items { get; }
        CollectionType CollectionType { get; }
    }

    public enum CollectionType
    {
        PropertyValue,
        CollectionList
    }
}