using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.ForceFieldDiagramView
{
    public class ForceFieldDiagramViewModel
    {
        private readonly ForceFieldDiagram diagram;

        public ForceFieldDiagramViewModel(ForceFieldDiagram forceFieldDiagram)
        {
            this.diagram = forceFieldDiagram;

            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<ForceFieldDiagramStakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new ForceFieldDiagramStakeholderViewModel(stakeholder)));
            }
        }

        public ObservableCollection<ForceFieldDiagramStakeholderViewModel> Stakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(new ForceFieldDiagramStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }
}