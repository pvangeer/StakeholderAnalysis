using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public interface ITreeNodeViewModel : IExpandable
    {
        string DisplayName { get; }

        string IconSourceString { get; }

        bool CanRemove { get; }

        ICommand RemoveItemCommand { get; }

        bool CanAdd { get; }

        ICommand AddItemCommand { get; }

        bool CanOpen { get; }

        ICommand OpenViewCommand { get; }

        ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        bool IsViewModelFor(object o);
    }
}