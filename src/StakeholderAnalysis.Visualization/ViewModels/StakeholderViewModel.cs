using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : NotifyPropertyChangedObservable, IDropHandler
    {
        protected readonly ISelectionRegister SelectionRegister;

        public StakeholderViewModel(Stakeholder stakeholder, ISelectionRegister selectionRegister)
        {
            this.SelectionRegister = selectionRegister;
            Stakeholder = stakeholder;
            if (Stakeholder != null) Stakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        public Stakeholder Stakeholder { get; }

        public bool IsViewModelFor(Stakeholder stakeholder)
        {
            return Stakeholder == stakeholder;
        }

        public string Name
        {
            get => Stakeholder.Name;
            set
            {
                Stakeholder.Name = value;
                Stakeholder.OnPropertyChanged(nameof(Stakeholder.Name));
            }
        }

        public StakeholderType Type => Stakeholder.Type;

        public bool IsSelectedStakeholder => SelectionRegister != null && SelectionRegister.IsSelected(Stakeholder);

        public ICommand StakeholderClickedCommand => new StakeholderClickedCommand(this);
        
        protected virtual void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(Stakeholder.Type):
                    OnPropertyChanged(nameof(Type));
                    break;
            }
        }

        public virtual void Moved(double xRelativeNew, double yRelativeNew) { }

        public void SelectStakeholder()
        {
            SelectionRegister?.Select(Stakeholder);
        }
    }
}