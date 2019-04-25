using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramView
{
    public class AttitudeImpactDiagramViewModel
    {
        private readonly AttitudeImpactDiagram diagram;

        public AttitudeImpactDiagramViewModel(AttitudeImpactDiagram attitudeImpactDiagram)
        {
            this.diagram = attitudeImpactDiagram;

            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<AttitudeImpactDiagramStakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new AttitudeImpactDiagramStakeholderViewModel(stakeholder)));
            }
        }

        public ObservableCollection<AttitudeImpactDiagramStakeholderViewModel> Stakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(new AttitudeImpactDiagramStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }
}