using System.ComponentModel;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : NotifyPropertyChangedObservable
    {
        public StakeholderViewModel(Stakeholder stakeholder)
        {
            Stakeholder = stakeholder;
            if (Stakeholder != null) Stakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        protected Stakeholder Stakeholder { get; }

        public string Name
        {
            get => Stakeholder.Name;
            set
            {
                Stakeholder.Name = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Name));
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