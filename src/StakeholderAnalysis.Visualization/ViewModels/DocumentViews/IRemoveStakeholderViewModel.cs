using System.ComponentModel;
using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews
{
    public interface IRemoveStakeholderViewModel : INotifyPropertyChanged
    {
        bool IsSelectedStakeholder { get; }

        ICommand RemoveStakeholderCommand { get; }

        void RemoveFromDiagram();
    }
}