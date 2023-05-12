﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramStakeholdersViewModel : ViewModelBase
    {
        private readonly OnionDiagram diagram;
        private readonly IDrawConnectionHandler drawConnectionHandler;
        private readonly ISelectionRegister selectionRegister;

        public OnionDiagramStakeholdersViewModel(ViewModelFactory factory, OnionDiagram onionDiagram,
            ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory)
        {
            diagram = onionDiagram;
            this.drawConnectionHandler = drawConnectionHandler;
            this.selectionRegister = selectionRegister;
            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += OnionDiagramStakeholdersCollectionChanged;
                OnionDiagramStakeholders = new ObservableCollection<OnionDiagramStakeholderViewModel>(
                    diagram.Stakeholders.Select(stakeholder =>
                        ViewModelFactory.CreateOnionDiagramStakeholderViewModel(diagram, stakeholder, selectionRegister,
                            drawConnectionHandler)));
            }
        }

        public ObservableCollection<OnionDiagramStakeholderViewModel> OnionDiagramStakeholders { get; }

        private void OnionDiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Add(ViewModelFactory.CreateOnionDiagramStakeholderViewModel(diagram, item,
                        selectionRegister, drawConnectionHandler));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Remove(OnionDiagramStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }
}