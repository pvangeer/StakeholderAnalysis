using System;
using System.ComponentModel;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class ForceFieldDiagramStakeholderViewModel : StakeholderViewModel, IPositionedStakeholderViewModel
    {
        private readonly ForceFieldDiagramStakeholder forceFieldDiagramStakeholder;
        private readonly ForceFieldDiagram diagram;

        public ForceFieldDiagramStakeholderViewModel(ViewModelFactory factory, ForceFieldDiagram diagram, ForceFieldDiagramStakeholder stakeholder, ISelectionRegister selectionRegister) : base(factory, stakeholder?.Stakeholder, selectionRegister, null)
        {
            this.diagram = diagram;
            forceFieldDiagramStakeholder = stakeholder;
            if (forceFieldDiagramStakeholder != null)
            {
                forceFieldDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
        }

        public double RelativePositionLeft
        {
            get => forceFieldDiagramStakeholder.Interest;
            set
            {
                forceFieldDiagramStakeholder.Interest = value;
                forceFieldDiagramStakeholder.OnPropertyChanged(nameof(forceFieldDiagramStakeholder.Interest));
            }
        }

        public double RelativePositionTop
        {
            get => 1 - forceFieldDiagramStakeholder.Influence;
            set
            {
                forceFieldDiagramStakeholder.Influence = 1 - value;
                forceFieldDiagramStakeholder.OnPropertyChanged(nameof(forceFieldDiagramStakeholder.Influence));
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
                diagram.Stakeholders.Remove(forceFieldDiagramStakeholder);
            }
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(forceFieldDiagramStakeholder.Interest):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(forceFieldDiagramStakeholder.Influence):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }
            base.StakeholderPropertyChanged(sender,e);
        }
    }
}
