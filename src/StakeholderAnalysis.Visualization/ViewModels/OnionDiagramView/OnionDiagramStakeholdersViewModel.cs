﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramStakeholdersViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;

        public OnionDiagramStakeholdersViewModel(OnionDiagram onionDiagram)
        {
            diagram = onionDiagram;

            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += OnionDiagramStakeholdersCollectionChanged;
                OnionDiagramStakeholders = new ObservableCollection<OnionDiagramStakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new OnionDiagramStakeholderViewModel(stakeholder)));
            }
        }

        public ObservableCollection<OnionDiagramStakeholderViewModel> OnionDiagramStakeholders { get; }

        private void OnionDiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Add(new OnionDiagramStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Remove(OnionDiagramStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }


    }
}