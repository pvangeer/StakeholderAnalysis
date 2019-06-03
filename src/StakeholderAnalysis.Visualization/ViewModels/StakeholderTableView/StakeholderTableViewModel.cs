using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView
{
    public class StakeholderTableViewModel : ViewModelBase
    {
        private readonly Analysis analysis;

        public StakeholderTableViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
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

        public ObservableCollection<StakeholderType> StakeholderTypes => analysis.StakeholderTypes;


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
                        AnalysisServices.RemoveStakeholderFromAnalysis(analysis,stakeholderViewModel.Stakeholder);
                    }
                    break;
            }

            analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
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