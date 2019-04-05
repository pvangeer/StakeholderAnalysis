using System.ComponentModel;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : PropertyChangedElement
    {
        public StakeholderViewModel(Stakeholder stakeholder)
        {
            Stakeholder = stakeholder;
            if (Stakeholder != null)
            {
                Stakeholder.PropertyChanged += StakeholderPropertyChanged;
            }
        }

        public Stakeholder Stakeholder { get; }

        public string Name => Stakeholder.Name;

        public double LeftPercentage => Stakeholder.LeftPercentage;

        public double TopPercentage => Stakeholder.TopPercentage;

        public StakeholderType Type => Stakeholder.Type;

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
            }
        }
    }
}
