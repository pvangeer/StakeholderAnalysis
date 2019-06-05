using System.ComponentModel;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public interface IRemoveStakeholderViewModel : INotifyPropertyChanged
    {
        bool IsSelectedStakeholder { get; }

        void RemoveFromDiagram();
    }
}