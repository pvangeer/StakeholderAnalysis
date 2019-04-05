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

        public Stakeholder Stakeholder { get; }

        public string Name => Stakeholder.Name;

        public double LeftPercentage => Stakeholder.LeftPercentage;

        public double TopPercentage => Stakeholder.TopPercentage;

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
            }
        }

        public void Select()
        {
            selectionRegister.Select(this);
        }
    }
}
