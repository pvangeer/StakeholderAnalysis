using System;
using System.ComponentModel;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramStakeholderViewModel : StakeholderViewModel, IPositionedStakeholderViewModel
    {
        public AttitudeImpactDiagramStakeholderViewModel(Stakeholder stakeholder) : base(stakeholder)
        {
        }

        public double RelativePositionLeft
        {
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

        public override void Moved(double xRelativeNew, double yRelativeNew)
        {
            RelativePositionLeft = Math.Min(1.0, Math.Max(0.0, xRelativeNew));
            RelativePositionTop = Math.Min(1.0, Math.Max(0.0, yRelativeNew));
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Impact):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(Stakeholder.Attitude):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }
            base.StakeholderPropertyChanged(sender,e);
        }
    }
}
