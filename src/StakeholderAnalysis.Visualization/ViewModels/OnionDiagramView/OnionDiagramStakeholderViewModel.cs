using System;
using System.ComponentModel;
using StakeholderAnalysis.Data.OnionDiagrams;
using static System.Double;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramStakeholderViewModel : StakeholderViewModel
    {
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        public OnionDiagramStakeholderViewModel(OnionDiagramStakeholder stakeholder) : base(stakeholder?.Stakeholder)
        {
            onionDiagramStakeholder = stakeholder;
            if (onionDiagramStakeholder != null)
            {
                onionDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
        }

        public double LeftPercentage
        {
            get => onionDiagramStakeholder?.Left ?? NaN;
            set
            {
                onionDiagramStakeholder.Left = value;
                onionDiagramStakeholder.OnPropertyChanged(nameof(onionDiagramStakeholder.Left));
            }
        }

        public double TopPercentage
        {
            get => onionDiagramStakeholder?.Top ?? NaN;
            set
            {
                onionDiagramStakeholder.Top = value;
                onionDiagramStakeholder.OnPropertyChanged(nameof(onionDiagramStakeholder.Top));
            }
        }

        public override void Moved(double xRelativeNew, double yRelativeNew)
        {
            LeftPercentage = Math.Min(1.0,Math.Max(0.0,xRelativeNew));
            TopPercentage = Math.Min(1.0, Math.Max(0.0, yRelativeNew));
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagramStakeholder.Left):
                    OnPropertyChanged(nameof(LeftPercentage));
                    break;
                case nameof(OnionDiagramStakeholder.Top):
                    OnPropertyChanged(nameof(TopPercentage));
                    break;
            }
            base.StakeholderPropertyChanged(sender,e);
        }

        public bool IsViewModelFor(OnionDiagramStakeholder stakeholder)
        {
            return IsViewModelFor(stakeholder.Stakeholder);
        }
    }
}
