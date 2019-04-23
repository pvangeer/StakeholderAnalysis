using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class OnionDiagramStakeholdersViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;
        private readonly ISelectionRegister selectionRegister;

        public OnionDiagramStakeholdersViewModel(OnionDiagram onionDiagram, ISelectionRegister selectionRegister)
        {
            diagram = onionDiagram;
            this.selectionRegister = selectionRegister;

            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += OnionDiagramStakeholdersCollectionChanged;
                OnionDiagramStakeholders = new ObservableCollection<StakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new StakeholderViewModel(stakeholder, selectionRegister)));
            }
        }

        public ObservableCollection<StakeholderViewModel> OnionDiagramStakeholders { get; }

        private void OnionDiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Add(new StakeholderViewModel(item, selectionRegister));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Remove(OnionDiagramStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }


    }
}
