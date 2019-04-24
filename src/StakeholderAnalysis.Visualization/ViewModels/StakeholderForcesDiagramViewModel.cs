using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderForcesDiagramViewModel
    {
        private readonly ForceFieldDiagram diagram;

        public StakeholderForcesDiagramViewModel(ForceFieldDiagram diagram)
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