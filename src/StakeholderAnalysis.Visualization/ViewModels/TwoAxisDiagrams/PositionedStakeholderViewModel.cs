using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    // TODO: Also use this interface in onion diagram and merge the two datatemplates
    public interface IPositionedStakeholderViewModel
    {
        bool IsViewModelFor(Stakeholder stakeholder);

        double RelativePositionLeft { get; set; }

        double RelativePositionTop { get; set; }
    }
}
