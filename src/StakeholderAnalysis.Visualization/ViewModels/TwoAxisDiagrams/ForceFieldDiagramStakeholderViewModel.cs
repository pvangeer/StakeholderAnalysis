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
                OnPropertyChanged(nameof(Stakeholder.Interest));
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
