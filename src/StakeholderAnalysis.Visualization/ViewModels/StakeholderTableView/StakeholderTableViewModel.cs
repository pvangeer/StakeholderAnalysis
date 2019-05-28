using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView
{
    public class StakeholderTableViewModel
    {
        private readonly Analysis analysis;

        public StakeholderTableViewModel(Analysis analysis)
        {
            this.analysis = analysis;
            if (analysis != null)
            {
                analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<StakeholderViewModel>(analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, null, null)));
                Stakeholders.CollectionChanged += StakeholderViewModelsCollectionChanged;
            }
        }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

        private void StakeholderViewModelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (analysis == null)
            {
                return;
            }

            analysis.Stakeholders.CollectionChanged -= StakeholdersCollectionChanged;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var stakeholderViewModel in e.NewItems.OfType<StakeholderViewModel>())
                    {
                        analysis.Stakeholders.Add(stakeholderViewModel.Stakeholder);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderViewModel in e.OldItems.OfType<StakeholderViewModel>())
                    {
                        RemoveStakeholderFromAnalysis(stakeholderViewModel.Stakeholder);
                    }
                    break;
            }

            analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
        }

        private void RemoveStakeholderFromAnalysis(Stakeholder stakeholder)
        {
            if (analysis.Stakeholders.Contains(stakeholder))
            {
                analysis.Stakeholders.Remove(stakeholder);
                foreach (var onionDiagram in analysis.OnionDiagrams)
                {
                    foreach (var onionDiagramStakeholder in onionDiagram.Stakeholders.Where(s => s.Stakeholder == stakeholder)
                        .ToArray())
                    {
                        onionDiagram.Stakeholders.Remove(onionDiagramStakeholder);
                        foreach (var stakeholderConnection in onionDiagram.Connections.Where(c =>
                            c.ConnectFrom == onionDiagramStakeholder || c.ConnectTo == onionDiagramStakeholder).ToArray())
                        {
                            onionDiagram.Connections.Remove(stakeholderConnection);
                        }
                    }
                }

                foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
                {
                    foreach (var forceFieldDiagramStakeholder in forceFieldDiagram.Stakeholders
                        .Where(s => s.Stakeholder == stakeholder).ToArray())
                    {
                        forceFieldDiagram.Stakeholders.Remove(forceFieldDiagramStakeholder);
                    }
                }

                foreach (var attitudeImpactDiagram in analysis.AttitudeImpactDiagrams)
                {
                    foreach (var attitudeImpactDiagramStakeholder in attitudeImpactDiagram.Stakeholders
                        .Where(s => s.Stakeholder == stakeholder).ToArray())
                    {
                        attitudeImpactDiagram.Stakeholders.Remove(attitudeImpactDiagramStakeholder);
                    }
                }
            }
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(new StakeholderViewModel(item, null, null));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }
}