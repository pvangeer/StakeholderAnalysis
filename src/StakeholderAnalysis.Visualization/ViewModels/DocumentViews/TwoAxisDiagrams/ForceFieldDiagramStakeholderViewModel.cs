using System;
using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public class ForceFieldDiagramStakeholderViewModel : RankedStakeholderViewModel, IPositionedStakeholderViewModel
    {
        private readonly ForceFieldDiagram diagram;
        private readonly PositionedStakeholder positionedStakeholder;

        public ForceFieldDiagramStakeholderViewModel(ViewModelFactory factory, ForceFieldDiagram diagram,
            PositionedStakeholder stakeholder, ISelectionRegister selectionRegister) :
            base(factory, stakeholder, diagram, selectionRegister, null)
        {
            this.diagram = diagram;
            positionedStakeholder = stakeholder;
            if (positionedStakeholder != null)
                positionedStakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        public double RelativePositionLeft
        {
            get => positionedStakeholder.Left;
            set
            {
                positionedStakeholder.Left = value;
                positionedStakeholder.OnPropertyChanged(nameof(positionedStakeholder.Left));
            }
        }

        public double RelativePositionTop
        {
            get => positionedStakeholder.Top;
            set
            {
                positionedStakeholder.Top = value;
                positionedStakeholder.OnPropertyChanged(nameof(positionedStakeholder.Top));
            }
        }

        public override void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder) diagram.Stakeholders.Remove(positionedStakeholder);
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
                case nameof(positionedStakeholder.Left):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(positionedStakeholder.Top):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }

            base.StakeholderPropertyChanged(sender, e);
        }
    }
}