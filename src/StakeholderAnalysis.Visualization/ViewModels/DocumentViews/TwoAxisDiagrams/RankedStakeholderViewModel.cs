using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    public abstract class RankedStakeholderViewModel : DiagramStakeholderViewModelBase, IRankedStakeholderViewModel
    {
        protected readonly IStakeholderDiagram Diagram;

        protected RankedStakeholderViewModel(ViewModelFactory factory, PositionedStakeholder stakeholder,
            IStakeholderDiagram diagram, ISelectionRegister selectionRegister1,
            IDrawConnectionHandler drawConnectionHandler)
            : base(factory, stakeholder?.Stakeholder, selectionRegister1, drawConnectionHandler)
        {
            Diagram = diagram;

            if (Diagram != null) Diagram.Stakeholders.CollectionChanged += DiagramStakeholdersCollectionChanged;
            RankedStakeholder = stakeholder;
            if (RankedStakeholder != null) RankedStakeholder.PropertyChanged += StakeholderPropertyChanged;

            MoveStakeholderToBottomCommand = CommandFactory.CreateMoveToBottomCommand(Diagram, RankedStakeholder);
            MoveStakeholderToTopCommand = CommandFactory.CreateMoveToTopCommand(Diagram, RankedStakeholder);
            MoveStakeholderUpCommand = CommandFactory.CreateMoveUpCommand(Diagram, RankedStakeholder);
            MoveStakeholderDownCommand = CommandFactory.CreateMoveDownCommand(Diagram, RankedStakeholder);
        }

        // TODO: Remove RankedStakeholder (and interface) as all PositionedStakeholders are ranked
        public PositionedStakeholder RankedStakeholder { get; set; }

        public int Rank
        {
            get => RankedStakeholder.Rank;
            set
            {
                RankedStakeholder.Rank = value;
                RankedStakeholder.OnPropertyChanged(nameof(IRankedStakeholder.Rank));
            }
        }

        public ICommand MoveStakeholderDownCommand { get; }

        public ICommand MoveStakeholderUpCommand { get; }

        public ICommand MoveStakeholderToTopCommand { get; }

        public ICommand MoveStakeholderToBottomCommand { get; }

        public bool CanMoveToBottom => MoveStakeholderToBottomCommand.CanExecute(null);

        public bool CanMoveToTop => MoveStakeholderToTopCommand.CanExecute(null);

        public bool CanMoveUp => MoveStakeholderUpCommand.CanExecute(null);

        public bool CanMoveDown => MoveStakeholderDownCommand.CanExecute(null);

        private void DiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanMoveToBottom));
            OnPropertyChanged(nameof(CanMoveDown));
            OnPropertyChanged(nameof(CanMoveToTop));
            OnPropertyChanged(nameof(CanMoveUp));
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PositionedStakeholder.Rank):
                    OnPropertyChanged(nameof(CanMoveToBottom));
                    OnPropertyChanged(nameof(CanMoveDown));
                    OnPropertyChanged(nameof(CanMoveToTop));
                    OnPropertyChanged(nameof(CanMoveUp));
                    OnPropertyChanged(nameof(Rank));
                    break;
            }

            base.StakeholderPropertyChanged(sender, e);
        }
    }

    public interface IRankedStakeholderViewModel : IRemoveStakeholderViewModel
    {
        int Rank { get; set; }
        ICommand MoveStakeholderDownCommand { get; }
        ICommand MoveStakeholderUpCommand { get; }
        ICommand MoveStakeholderToTopCommand { get; }
        ICommand MoveStakeholderToBottomCommand { get; }
        bool CanMoveToBottom { get; }
        bool CanMoveToTop { get; }
        bool CanMoveUp { get; }
        bool CanMoveDown { get; }
    }
}