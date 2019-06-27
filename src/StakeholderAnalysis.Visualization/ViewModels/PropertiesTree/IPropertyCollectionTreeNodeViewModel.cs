using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface IPropertyCollectionTreeNodeViewModel : ITreeNodeViewModel
    {
        ObservableCollection<ITreeNodeViewModel> Items { get; }
    }
}