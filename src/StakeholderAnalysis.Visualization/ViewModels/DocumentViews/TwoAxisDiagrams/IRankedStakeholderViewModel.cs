using System.Windows.Input;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public interface IRankedStakeholderViewModel : IRemoveStakeholderViewModel
    {
        int Rank { get; set; }

        ICommand MoveStakeholderDownCommand { get; }

        ICommand MoveStakeholderUpCommand { get; }

        ICommand MoveStakeholderToTopCommand { get; }

        ICommand MoveStakeholderToBottomCommand { get; }

        bool CanMoveToBottom { get; }

        bool CanMoveToTop { get; }

        bool CanMoveUp { get; }

        bool CanMoveDown { get; }
    }
}