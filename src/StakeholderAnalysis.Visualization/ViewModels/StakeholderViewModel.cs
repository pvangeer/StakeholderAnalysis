using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewModel : NotifyPropertyChangedObservable, IDropHandler, IRemoveStakeholderViewModel
    {
        protected readonly ISelectionRegister SelectionRegister;
        private readonly IDrawConnectionHandler drawConnectionHandler;

        public StakeholderViewModel() : this(new Stakeholder(),null,null) { }

        public StakeholderViewModel(Stakeholder stakeholder, ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler)
        {
            this.drawConnectionHandler = drawConnectionHandler;
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

        public bool IsSelectedStakeholder => SelectionRegister != null && SelectionRegister.IsSelected(Stakeholder);

        public virtual void RemoveFromDiagram() { }

        public bool IsConnectionToTarget => drawConnectionHandler != null && drawConnectionHandler.IsConnectionTarget(Stakeholder);

        public ICommand StakeholderClickedCommand => new StakeholderClickedCommand(this);

        public ICommand RemoveStakeholderCommand => new RemoveSelectedStakeholderFromDiagramCommand(this);

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