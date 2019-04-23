using System;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        // TODO: Split this into different viewmodels
        public StakeholderViewModel(OnionDiagramStakeholder stakeholder) : this(stakeholder.Stakeholder)
        {
            onionDiagramStakeholder = stakeholder;
            if (onionDiagramStakeholder != null)
            {
                onionDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
        }

        public StakeholderViewModel(Stakeholder stakeholder)
        {
            Stakeholder = stakeholder;
            if (Stakeholder != null) Stakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        private Stakeholder Stakeholder { get; }

        public string Name
        {
            get => Stakeholder.Name;
            set
            {
                Stakeholder.Name = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Name));
            }
        }

        public double LeftPercentage
        {
            get => onionDiagramStakeholder?.Left ?? double.NaN;
            set
            {
                onionDiagramStakeholder.Left = value;
                onionDiagramStakeholder.OnPropertyChanged(nameof(onionDiagramStakeholder.Left));
            }
        }

        public double TopPercentage
        {
            get => onionDiagramStakeholder?.Top ?? Double.NaN;
            set
            {
                onionDiagramStakeholder.Top = value;
                Stakeholder.OnPropertyChanged(nameof(onionDiagramStakeholder.Top));
            }
        }

        public double Interest
        {
            get => Stakeholder.Interest;
            set
            {
                Stakeholder.Interest = value;
                OnPropertyChanged(nameof(Stakeholder.Interest));
            }
        }

        public double Influence
        {
            get => Stakeholder.Influence;
            set
            {
                Stakeholder.Influence = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Influence));
            }
        }


        public double Attitude
        {
            get => Stakeholder.Attitude;
            set
            {
                Stakeholder.Attitude = value;
                OnPropertyChanged(nameof(Stakeholder.Attitude));
            }
        }

        public double Impact
        {
            get => Stakeholder.Impact;
            set
            {
                Stakeholder.Impact = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Impact));
            }
        }

        public double InfluenceLocation => 1 - Stakeholder.Influence;

        public double AttitudePosition => 1 - Stakeholder.Attitude;

        public StakeholderType Type => Stakeholder.Type;

        private void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(OnionDiagramStakeholder.Left):
                    OnPropertyChanged(nameof(LeftPercentage));
                    break;
                case nameof(OnionDiagramStakeholder.Top):
                    OnPropertyChanged(nameof(TopPercentage));
                    break;
                case nameof(Stakeholder.Type):
                    OnPropertyChanged(nameof(Type));
                    break;
                case nameof(Stakeholder.Interest):
                    OnPropertyChanged(nameof(Interest));
                    break;
                case nameof(Stakeholder.Influence):
                    OnPropertyChanged(nameof(InfluenceLocation));
                    OnPropertyChanged(nameof(Influence));
                    break;
                case nameof(Stakeholder.Attitude):
                    OnPropertyChanged(nameof(Attitude));
                    OnPropertyChanged(nameof(AttitudePosition));
                    break;
                case nameof(Stakeholder.Impact):
                    OnPropertyChanged(nameof(Impact));
                    break;
            }
        }

        public bool IsViewModelFor(OnionDiagramStakeholder stakeholder)
        {
            return IsViewModelFor(stakeholder.Stakeholder);
        }

        public bool IsViewModelFor(Stakeholder stakeholder)
        {
            return Stakeholder == stakeholder;
        }

        public bool IsViewModelFor(StakeholderViewModel stakeholderViewModel)
        {
            return stakeholderViewModel.IsViewModelFor(Stakeholder);
        }
    }
}