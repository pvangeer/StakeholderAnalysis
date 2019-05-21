using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView
{
    public class StakeholderTableViewModel
    {
        public StakeholderTableViewModel(Analysis analysis)
        {
            if (analysis != null)
            {
                analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<StakeholderViewModel>(analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, null, null)));
            }
        }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

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