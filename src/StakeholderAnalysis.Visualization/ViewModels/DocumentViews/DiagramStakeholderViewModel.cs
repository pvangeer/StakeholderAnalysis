using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews
{
    public class DiagramStakeholderViewModel : StakeholderViewModel, IDropHandler, IRemoveStakeholderViewModel
    {
        protected readonly IStakeholderDiagram Diagram;
        protected readonly PositionedStakeholder PositionedStakeholder;
        private readonly ISelectionRegister selectionRegister;

        public DiagramStakeholderViewModel(ViewModelFactory factory, PositionedStakeholder stakeholder, IStakeholderDiagram diagram,
            ISelectionRegister selectionRegister) :
            base(factory, stakeholder?.Stakeholder)
        {
            Diagram = diagram;

            if (Diagram != null)
                Diagram.Stakeholders.CollectionChanged += DiagramStakeholdersCollectionChanged;

            PositionedStakeholder = stakeholder;

            if (PositionedStakeholder != null)
                PositionedStakeholder.PropertyChanged += StakeholderPropertyChanged;

            MoveStakeholderToBottomCommand = CommandFactory.CreateMoveToBottomCommand(Diagram, PositionedStakeholder);
            MoveStakeholderToTopCommand = CommandFactory.CreateMoveToTopCommand(Diagram, PositionedStakeholder);
            MoveStakeholderUpCommand = CommandFactory.CreateMoveUpCommand(Diagram, PositionedStakeholder);
            MoveStakeholderDownCommand = CommandFactory.CreateMoveDownCommand(Diagram, PositionedStakeholder);

            this.selectionRegister = selectionRegister;
        }

        #region ConnectionTarget

        public virtual bool IsConnectionToTarget => false;

        #endregion

        public PositionedStakeholder GetDiagramStakeholder()
        {
            return PositionedStakeholder;
        }

        public bool IsViewModelFor(Stakeholder stakeholder)
        {
            return Stakeholder == stakeholder;
        }

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
                case nameof(PositionedStakeholder.Left):
                    OnPropertyChanged(nameof(RelativePositionLeft));
                    break;
                case nameof(PositionedStakeholder.Top):
                    OnPropertyChanged(nameof(RelativePositionTop));
                    break;
            }

            base.StakeholderPropertyChanged(sender, e);
        }

        #region Positioning

        public double RelativePositionLeft
        {
            get => PositionedStakeholder.Left;
            set
            {
                PositionedStakeholder.Left = value;
                PositionedStakeholder.OnPropertyChanged(nameof(PositionedStakeholder.Left));
            }
        }

        public double RelativePositionTop
        {
            get => PositionedStakeholder.Top;
            set
            {
                PositionedStakeholder.Top = value;
                PositionedStakeholder.OnPropertyChanged(nameof(PositionedStakeholder.Top));
            }
        }

        #endregion

        #region Rank

        public int Rank
        {
            get => PositionedStakeholder.Rank;
            set
            {
                PositionedStakeholder.Rank = value;
                PositionedStakeholder.OnPropertyChanged();
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

        #endregion

        #region Select and move

        public ICommand StakeholderClickedCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            selectionRegister?.SelectObject(Stakeholder);
        });

        public virtual void Moved(double xRelativeNew, double yRelativeNew)
        {
            RelativePositionLeft = Math.Min(1.0, Math.Max(0.0, xRelativeNew));
            RelativePositionTop = Math.Min(1.0, Math.Max(0.0, yRelativeNew));
        }

        public bool IsSelectedStakeholder => selectionRegister != null && selectionRegister.IsSelectedObject(Stakeholder);

        #endregion

        #region Remove

        public virtual void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder)
                Diagram.Stakeholders.Remove(PositionedStakeholder);
        }

        public ICommand RemoveStakeholderCommand =>
            CommandFactory.CreateRemoveSelectedStakeholderFromDiagramCommand(this);

        #endregion
    }
}