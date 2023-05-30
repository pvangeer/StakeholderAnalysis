using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Gui
{
    public interface ISelectable : INotifyPropertyChangedImplementation
    {
        bool CanSelect { get; }

        bool IsSelected { get; set; }

        ICommand SelectItemCommand { get; }
    }
}