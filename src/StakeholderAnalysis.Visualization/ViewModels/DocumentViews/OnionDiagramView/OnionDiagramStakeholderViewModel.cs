using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;
using static System.Double;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class OnionDiagramStakeholderViewModel : StakeholderViewModel
    {
        private readonly OnionDiagram diagram;
        private readonly OnionDiagramStakeholder onionDiagramStakeholder;

        public OnionDiagramStakeholderViewModel(ViewModelFactory factory, OnionDiagram diagram,
            OnionDiagramStakeholder stakeholder,
            ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory,
            stakeholder?.Stakeholder, selectionRegister, drawConnectionHandler)
        {
            DrawConnectionHandler = drawConnectionHandler;
            this.diagram = diagram;
            if (diagram != null) diagram.Stakeholders.CollectionChanged += DiagramStakeholdersCollectionChanged;
            onionDiagramStakeholder = stakeholder;
            if (onionDiagramStakeholder != null) onionDiagramStakeholder.PropertyChanged += StakeholderPropertyChanged;
            MoveStakeholderToBottomCommand = CommandFactory.CreateMoveToBottomCommand(diagram, onionDiagramStakeholder);
            MoveStakeholderToTopCommand = CommandFactory.CreateMoveToTopCommand(diagram, onionDiagramStakeholder);
            MoveStakeholderUpCommand = CommandFactory.CreateMoveUpCommand(diagram, onionDiagramStakeholder);
            MoveStakeholderDownCommand = CommandFactory.CreateMoveDownCommand(diagram, onionDiagramStakeholder);
        }

        public int Rank
        {
            get => onionDiagramStakeholder.Rank;
            set
            {
                onionDiagramStakeholder.Rank = value;
                onionDiagramStakeholder.OnPropertyChanged();
            }
        }

        public double LeftPercentage
        {
            get => onionDiagramStakeholder?.Left ?? NaN;
            set
            {
                onionDiagramStakeholder.Left = value;
                onionDiagramStakeholder.OnPropertyChanged(nameof(onionDiagramStakeholder.Left));
            }
        }

        public double TopPercentage
        {
            get => onionDiagramStakeholder?.Top ?? NaN;
            set
            {
                onionDiagramStakeholder.Top = value;
                onionDiagramStakeholder.OnPropertyChanged(nameof(onionDiagramStakeholder.Top));
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
                case nameof(OnionDiagramStakeholder.Left):
                    OnPropertyChanged(nameof(LeftPercentage));
                    break;
                case nameof(OnionDiagramStakeholder.Top):
                    OnPropertyChanged(nameof(TopPercentage));
                    break;
                case nameof(OnionDiagramStakeholder.Rank):
                    OnPropertyChanged(nameof(CanMoveToBottom));
                    OnPropertyChanged(nameof(CanMoveDown));
                    OnPropertyChanged(nameof(CanMoveToTop));
                    OnPropertyChanged(nameof(CanMoveUp));
                    OnPropertyChanged(nameof(Rank));
                    break;
            }

            base.StakeholderPropertyChanged(sender, e);
        }

        public bool IsViewModelFor(OnionDiagramStakeholder stakeholder)
        {
            return IsViewModelFor(stakeholder.Stakeholder);
        }

        public override void RemoveFromDiagram()
        {
            if (IsSelectedStakeholder)
            {
                diagram.Stakeholders.Remove(onionDiagramStakeholder);
                var connectionsToRemove = diagram.Connections.Where(c =>
                    c.ConnectFrom == onionDiagramStakeholder || c.ConnectTo == onionDiagramStakeholder).ToList();
                foreach (var connection in connectionsToRemove) diagram.Connections.Remove(connection);
            }
        }

        public OnionDiagramStakeholder GetOnionDiagramStakeholder()
        {
            return onionDiagramStakeholder;
        }
    }
}