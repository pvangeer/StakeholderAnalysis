using System;
using System.ComponentModel;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class ForceFieldDiagramStakeholderViewModel : StakeholderViewModel, IPositionedStakeholderViewModel
    {
        private readonly ForceFieldDiagramStakeholder firceFieldDiagramStakeholder;

        public ForceFieldDiagramStakeholderViewModel(ForceFieldDiagramStakeholder stakeholder, ISelectionRegister selectionRegister) : base(stakeholder?.Stakeholder, selectionRegister)
        {
            firceFieldDiagramStakeholder = stakeholder;
            if (firceFieldDiagramStakeholder != null)
            {
                firceFieldDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
        }

        public double RelativePositionLeft
        {
            get => firceFieldDiagramStakeholder.Interest;
            set
            {
                firceFieldDiagramStakeholder.Interest = value;
                firceFieldDiagramStakeholder.OnPropertyChanged(nameof(firceFieldDiagramStakeholder.Interest));
            }
        }

        public double RelativePositionTop
        {
            get => 1 - firceFieldDiagramStakeholder.Influence;
            set
            {
                firceFieldDiagramStakeholder.Influence = 1 - value;
                firceFieldDiagramStakeholder.OnPropertyChanged(nameof(firceFieldDiagramStakeholder.Influence));
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
                case nameof(firceFieldDiagramStakeholder.Interest):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(firceFieldDiagramStakeholder.Influence):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }
            base.StakeholderPropertyChanged(sender,e);
        }
    }
}
