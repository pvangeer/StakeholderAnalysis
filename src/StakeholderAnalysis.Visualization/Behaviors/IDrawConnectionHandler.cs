using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.Behaviors
{
    public interface IDrawConnectionHandler : INotifyPropertyChanged
    {
        void PositionMoved(double relativeLeft, double relativeTop);

        void ChangeTarget(OnionDiagramStakeholderViewModel viewModel);

        void InitializeConnection(OnionDiagramStakeholderViewModel stakeholderViewModel);

        void FinishConnecting();

        bool IsConnectionTarget(Stakeholder stakeholder);

        bool IsActive { get; }
    }
}