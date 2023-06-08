using System;
using System.ComponentModel;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramStakeholderViewModel :
        RankedStakeholderViewModel<AttitudeImpactDiagramStakeholder>, IPositionedStakeholderViewModel
    {
        private readonly AttitudeImpactDiagramStakeholder attitudeImpactDiagramStakeholder;
        private readonly AttitudeImpactDiagram diagram;

        public AttitudeImpactDiagramStakeholderViewModel(ViewModelFactory factory, AttitudeImpactDiagram diagram,
            AttitudeImpactDiagramStakeholder stakeholder, ISelectionRegister selectionRegister)
            : base(factory, stakeholder, diagram, selectionRegister, null)
        {
            this.diagram = diagram;
            attitudeImpactDiagramStakeholder = stakeholder;
            if (attitudeImpactDiagramStakeholder != null)
                attitudeImpactDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        public double RelativePositionLeft
        {
            get => attitudeImpactDiagramStakeholder.Left;
            set
            {
                attitudeImpactDiagramStakeholder.Left = value;
                attitudeImpactDiagramStakeholder.OnPropertyChanged(nameof(attitudeImpactDiagramStakeholder.Left));
            }
        }

        public double RelativePositionTop
        {
            get => attitudeImpactDiagramStakeholder.Top;
            set
            {
                attitudeImpactDiagramStakeholder.Top = value;
                attitudeImpactDiagramStakeholder.OnPropertyChanged(nameof(attitudeImpactDiagramStakeholder.Top));
            }
        }

        public override void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder) diagram.Stakeholders.Remove(attitudeImpactDiagramStakeholder);
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
                case nameof(attitudeImpactDiagramStakeholder.Left):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(attitudeImpactDiagramStakeholder.Top):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }

            base.StakeholderPropertyChanged(sender, e);
        }
    }
}