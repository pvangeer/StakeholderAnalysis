using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class AttitudeImpactDiagramViewModel
    {
        private readonly AttitudeImpactDiagram diagram;

        public AttitudeImpactDiagramViewModel(AttitudeImpactDiagram diagram)
        {
            this.diagram = diagram;

            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<StakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder)));
            }
        }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(new StakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }
}