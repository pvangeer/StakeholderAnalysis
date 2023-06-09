using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public interface IPositionedStakeholderViewModel : IRankedStakeholderViewModel
    {
        double RelativePositionLeft { get; set; }

        double RelativePositionTop { get; set; }

        bool IsViewModelFor(Stakeholder stakeholder);
    }
}