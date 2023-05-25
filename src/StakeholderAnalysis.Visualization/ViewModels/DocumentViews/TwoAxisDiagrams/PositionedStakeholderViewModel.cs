using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    // TODO: Also use this interface in onion diagram and merge the two datatemplates
    public interface IPositionedStakeholderViewModel : IRankedStakeholderViewModel
    {
        double RelativePositionLeft { get; set; }

        double RelativePositionTop { get; set; }
        bool IsViewModelFor(Stakeholder stakeholder);
    }
}