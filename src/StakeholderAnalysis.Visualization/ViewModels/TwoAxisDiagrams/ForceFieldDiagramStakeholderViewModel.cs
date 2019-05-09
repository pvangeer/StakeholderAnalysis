using System;
using System.ComponentModel;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class ForceFieldDiagramStakeholderViewModel : StakeholderViewModel, IPositionedStakeholderViewModel
    {
        public ForceFieldDiagramStakeholderViewModel(Stakeholder stakeholder) : base(stakeholder)
        {
        }

        public double RelativePositionLeft
        {
            get => Stakeholder.Interest;
            set
            {
                Stakeholder.Interest = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Interest));
            }
        }

        public double RelativePositionTop
        {
            get => 1 - Stakeholder.Influence;
            set
            {
                Stakeholder.Influence = 1 - value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Influence));
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
                case nameof(Stakeholder.Interest):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(Stakeholder.Influence):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }
            base.StakeholderPropertyChanged(sender,e);
        }
    }
}
