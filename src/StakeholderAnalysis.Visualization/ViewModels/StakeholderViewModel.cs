using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : ViewModelBase, IDropHandler, IRemoveStakeholderViewModel
    {
        private readonly ISelectionRegister selectionRegister;
        private readonly IDrawConnectionHandler drawConnectionHandler;

        public StakeholderViewModel(ViewModelFactory factory, Stakeholder stakeholder, ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory)
        {
            this.drawConnectionHandler = drawConnectionHandler;
            this.selectionRegister = selectionRegister;
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

        public StakeholderType Type
        {
            get => Stakeholder.Type;
            set
            {
                if (Stakeholder != null)
                {
                    Stakeholder.Type = value;
                    Stakeholder.OnPropertyChanged(nameof(Stakeholder.Type));
                }
            }
        }

        public bool IsSelectedStakeholder => selectionRegister != null && selectionRegister.IsSelected(Stakeholder);

        public virtual void RemoveFromDiagram() { }

        public bool IsConnectionToTarget => drawConnectionHandler != null && drawConnectionHandler.IsConnectionTarget(Stakeholder);

        public ICommand StakeholderClickedCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            selectionRegister?.Select(Stakeholder);
        });

        public ICommand RemoveStakeholderCommand => CommandFactory.CreateRemoveSelectedStakeholderFromDiagramCommand(this);

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
            selectionRegister?.Select(Stakeholder);
        }
    }
}