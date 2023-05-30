using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels.TreeView
{
    public interface ITreeNodeViewModel : IExpandable, ISelectable, IViewModel
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
    }
}