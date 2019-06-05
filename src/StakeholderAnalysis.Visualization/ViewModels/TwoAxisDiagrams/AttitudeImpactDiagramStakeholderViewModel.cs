using System;
using System.ComponentModel;
using System.Windows.Media.TextFormatting;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramStakeholderViewModel : StakeholderViewModel, IPositionedStakeholderViewModel
    {
        private readonly AttitudeImpactDiagramStakeholder attitudeImpactDiagramStakeholder;
        private readonly AttitudeImpactDiagram diagram;

        public AttitudeImpactDiagramStakeholderViewModel(ViewModelFactory factory, AttitudeImpactDiagram diagram, AttitudeImpactDiagramStakeholder stakeholder, ISelectionRegister selectionRegister) : base(factory, stakeholder?.Stakeholder, selectionRegister, null)
        {
            this.diagram = diagram;
            attitudeImpactDiagramStakeholder = stakeholder;
            if (attitudeImpactDiagramStakeholder != null)
            {
                attitudeImpactDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
        }

        public double RelativePositionLeft
        {
            get => attitudeImpactDiagramStakeholder.Impact;
            set
            {
                attitudeImpactDiagramStakeholder.Impact = value;
                attitudeImpactDiagramStakeholder.OnPropertyChanged(nameof(attitudeImpactDiagramStakeholder.Impact));
            }
        }

        public double RelativePositionTop
        {
            get => 1- attitudeImpactDiagramStakeholder.Attitude;
            set
            {
                attitudeImpactDiagramStakeholder.Attitude = 1 - value;
                attitudeImpactDiagramStakeholder.OnPropertyChanged(nameof(attitudeImpactDiagramStakeholder.Attitude));
            }
        }

        public override void Moved(double xRelativeNew, double yRelativeNew)
        {
            RelativePositionLeft = Math.Min(1.0, Math.Max(0.0, xRelativeNew));
            RelativePositionTop = Math.Min(1.0, Math.Max(0.0, yRelativeNew));
        }

        public override void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder)
            {
                diagram.Stakeholders.Remove(attitudeImpactDiagramStakeholder);
            }
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(attitudeImpactDiagramStakeholder.Impact):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(attitudeImpactDiagramStakeholder.Attitude):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }
            base.StakeholderPropertyChanged(sender,e);
        }
    }
}
