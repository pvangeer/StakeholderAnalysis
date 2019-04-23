using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public abstract class StakeholderViewModelBase : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        
        protected StakeholderViewModelBase(Analysis analysis)
        {
            this.analysis = analysis;

            if (analysis != null)
            {
                analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<StakeholderViewModel>(analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder)));
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

    public class StakeholderTableViewModel : StakeholderViewModelBase
    {
        public StakeholderTableViewModel(Analysis analysis) : base(analysis)
        {
        }
    }

    public class StakeholderForcesDiagramViewModel : StakeholderViewModelBase
    {
        public StakeholderForcesDiagramViewModel(Analysis analysis) : base(analysis)
        {
        }
    }

    public class StakeholderAttitudeImpactDiagramViewModel : StakeholderViewModelBase
    {
        public StakeholderAttitudeImpactDiagramViewModel(Analysis analysis) : base(analysis)
        {
        }
    }
}
