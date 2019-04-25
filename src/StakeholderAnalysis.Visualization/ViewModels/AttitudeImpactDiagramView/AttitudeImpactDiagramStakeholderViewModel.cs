using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramView
{
    public class AttitudeImpactDiagramStakeholderViewModel : StakeholderViewModel, IPositionedStakeholderViewModel
    {
        public AttitudeImpactDiagramStakeholderViewModel(Stakeholder stakeholder) : base(stakeholder)
        {
        }

        public double RelativePositionLeft
        {
            // TODO Pull position members up. See also comments in ForceFieldDiagramStakeholderViewModel
            get => Stakeholder.Impact;
            set
            {
                Stakeholder.Impact = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Impact));
            }
        }

        public double RelativePositionTop
        {
            get => 1-Stakeholder.Attitude;
            set
            {
                Stakeholder.Attitude = 1 - value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Attitude));
            }
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Impact):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
            }
            base.StakeholderPropertyChanged(sender, e);
        }

    }
}
