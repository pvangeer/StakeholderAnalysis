using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;
using static System.Double;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class OnionDiagramStakeholderViewModel : DiagramStakeholderViewModelBase
    {
        private readonly OnionDiagram diagram;
        private readonly PositionedStakeholder positionedStakeholder;

        public OnionDiagramStakeholderViewModel(ViewModelFactory factory, OnionDiagram diagram,
            PositionedStakeholder stakeholder,
            ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory,
            stakeholder?.Stakeholder, selectionRegister, drawConnectionHandler)
        {
            DrawConnectionHandler = drawConnectionHandler;
            this.diagram = diagram;
            if (diagram != null) diagram.Stakeholders.CollectionChanged += DiagramStakeholdersCollectionChanged;
            positionedStakeholder = stakeholder;
            if (positionedStakeholder != null) positionedStakeholder.PropertyChanged += StakeholderPropertyChanged;
            MoveStakeholderToBottomCommand = CommandFactory.CreateMoveToBottomCommand(diagram, positionedStakeholder);
            MoveStakeholderToTopCommand = CommandFactory.CreateMoveToTopCommand(diagram, positionedStakeholder);
            MoveStakeholderUpCommand = CommandFactory.CreateMoveUpCommand(diagram, positionedStakeholder);
            MoveStakeholderDownCommand = CommandFactory.CreateMoveDownCommand(diagram, positionedStakeholder);
        }

        public int Rank
        {
            get => positionedStakeholder.Rank;
            set
            {
                positionedStakeholder.Rank = value;
                positionedStakeholder.OnPropertyChanged();
            }
        }

        public double LeftPercentage
        {
            get => positionedStakeholder?.Left ?? NaN;
            set
            {
                positionedStakeholder.Left = value;
                positionedStakeholder.OnPropertyChanged(nameof(positionedStakeholder.Left));
            }
        }

        public double TopPercentage
        {
            get => positionedStakeholder?.Top ?? NaN;
            set
            {
                positionedStakeholder.Top = value;
                positionedStakeholder.OnPropertyChanged(nameof(positionedStakeholder.Top));
            }
        }

        public IDrawConnectionHandler DrawConnectionHandler { get; }

        public ICommand MoveStakeholderToBottomCommand { get; }

        public bool CanMoveToBottom => MoveStakeholderToBottomCommand.CanExecute(null);

        public ICommand MoveStakeholderToTopCommand { get; }

        public bool CanMoveToTop => MoveStakeholderToTopCommand.CanExecute(null);

        public ICommand MoveStakeholderUpCommand { get; }

        public bool CanMoveUp => MoveStakeholderUpCommand.CanExecute(null);

        public ICommand MoveStakeholderDownCommand { get; }

        public bool CanMoveDown => MoveStakeholderDownCommand.CanExecute(null);

        private void DiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanMoveToBottom));
            OnPropertyChanged(nameof(CanMoveDown));
            OnPropertyChanged(nameof(CanMoveToTop));
            OnPropertyChanged(nameof(CanMoveUp));
        }

        public override void Moved(double xRelativeNew, double yRelativeNew)
        {
            LeftPercentage = Math.Min(1.0, Math.Max(0.0, xRelativeNew));
            TopPercentage = Math.Min(1.0, Math.Max(0.0, yRelativeNew));
        }

        protected override void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(PositionedStakeholder.Left):
                    OnPropertyChanged(nameof(LeftPercentage));
                    break;
                case nameof(PositionedStakeholder.Top):
                    OnPropertyChanged(nameof(TopPercentage));
                    break;
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

        public bool IsViewModelFor(PositionedStakeholder stakeholder)
        {
            return IsViewModelFor(stakeholder.Stakeholder);
        }

        public override void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder)
            {
                diagram.Stakeholders.Remove(positionedStakeholder);
                var connectionsToRemove = diagram.Connections.Where(c =>
                    c.ConnectFrom == positionedStakeholder || c.ConnectTo == positionedStakeholder).ToList();
                foreach (var connection in connectionsToRemove) diagram.Connections.Remove(connection);
            }
        }

        public PositionedStakeholder GetOnionDiagramStakeholder()
        {
            return positionedStakeholder;
        }
    }
}