using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews
{
    public class StakeholderViewModel : ViewModelBase, IDropHandler, IRemoveStakeholderViewModel
    {
        private readonly IDrawConnectionHandler drawConnectionHandler;
        private readonly ISelectionRegister selectionRegister;

        public StakeholderViewModel() : this(null, new Stakeholder(), null, null)
        {
        }

        public StakeholderViewModel(ViewModelFactory factory, Stakeholder stakeholder,
            ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory)
        {
            this.drawConnectionHandler = drawConnectionHandler;
            this.selectionRegister = selectionRegister;
            Stakeholder = stakeholder;

            if (Stakeholder != null) Stakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        public Stakeholder Stakeholder { get; }

        public string Name
        {
            get => Stakeholder.Name;
            set
            {
                Stakeholder.Name = value;
                Stakeholder.OnPropertyChanged();
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
                    Stakeholder.OnPropertyChanged();
                }
            }
        }

        public bool IsConnectionToTarget =>
            drawConnectionHandler != null && drawConnectionHandler.IsConnectionTarget(Stakeholder);

        public ICommand StakeholderClickedCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            selectionRegister?.SelectObject(Stakeholder);
        });

        public virtual void Moved(double xRelativeNew, double yRelativeNew)
        {
        }

        public bool IsSelectedStakeholder => selectionRegister != null && selectionRegister.IsSelectedObject(Stakeholder);

        public virtual void RemoveFromDiagram()
        {
        }

        public ICommand RemoveStakeholderCommand =>
            CommandFactory.CreateRemoveSelectedStakeholderFromDiagramCommand(this);

        public bool IsViewModelFor(Stakeholder stakeholder)
        {
            return Stakeholder == stakeholder;
        }

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

        public void SelectStakeholder()
        {
            selectionRegister?.SelectObject(Stakeholder);
        }
    }
}