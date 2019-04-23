using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public abstract class StakeholderViewModelBase : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly ISelectionRegister selectionRegister;

        protected StakeholderViewModelBase(Analysis analysis, ISelectionRegister selectionRegister)
        {
            this.selectionRegister = selectionRegister;
            this.analysis = analysis;

            if (analysis != null)
            {
                analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                Stakeholders = new ObservableCollection<StakeholderViewModel>(analysis.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, selectionRegister)));
            }
        }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(new StakeholderViewModel(item, selectionRegister));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }

    public class StakeholderTableViewModel : StakeholderViewModelBase
    {
        public StakeholderTableViewModel(Analysis analysis, ISelectionRegister selectionRegister) : base(analysis, selectionRegister)
        {
        }
    }

    public class StakeholderForcesDiagramViewModel : StakeholderViewModelBase
    {
        public StakeholderForcesDiagramViewModel(Analysis analysis, ISelectionRegister selectionRegister) : base(analysis, selectionRegister)
        {
        }
    }

    public class StakeholderAttitudeImpactDiagramViewModel : StakeholderViewModelBase
    {
        public StakeholderAttitudeImpactDiagramViewModel(Analysis analysis, ISelectionRegister selectionRegister) : base(analysis, selectionRegister)
        {
        }
    }
}
