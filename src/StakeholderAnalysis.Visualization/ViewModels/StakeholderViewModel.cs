using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : PropertyChangedElement
    {
        private readonly ISelectionRegister selectionRegister;

        public StakeholderViewModel(Stakeholder stakeholder, ISelectionRegister selectionRegister)
        {
            this.selectionRegister = selectionRegister;
            Stakeholder = stakeholder;
            if (Stakeholder != null)
            {
                Stakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
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
            get => Stakeholder.LeftPercentage;
            set
            {
                Stakeholder.LeftPercentage = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.LeftPercentage));
            }
        }

        public double TopPercentage
        {
            get => Stakeholder.TopPercentage;
            set
            {
                Stakeholder.TopPercentage = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.TopPercentage));
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

        public ICommand StakeholderClickedCommand => new StakeholderClickedCommand(this);

        private void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(Stakeholder.LeftPercentage):
                    OnPropertyChanged(nameof(LeftPercentage));
                    break;
                case nameof(Stakeholder.TopPercentage):
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

        public void Select()
        {
            selectionRegister.Select(this);
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
