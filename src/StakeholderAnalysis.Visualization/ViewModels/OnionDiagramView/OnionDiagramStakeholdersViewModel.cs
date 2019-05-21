using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramStakeholdersViewModel : NotifyPropertyChangedObservable
    {
        private readonly ISelectionRegister selectionRegister;
        private readonly OnionDiagram diagram;
        private readonly IDrawConnectionHandler drawConnectionHandler;

        public OnionDiagramStakeholdersViewModel(OnionDiagram onionDiagram, ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler)
        {
            diagram = onionDiagram;
            this.drawConnectionHandler = drawConnectionHandler;
            this.selectionRegister = selectionRegister;
            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += OnionDiagramStakeholdersCollectionChanged;
                OnionDiagramStakeholders = new ObservableCollection<OnionDiagramStakeholderViewModel>(
                    diagram.Stakeholders.Select(stakeholder =>
                        new OnionDiagramStakeholderViewModel(diagram, stakeholder, selectionRegister, drawConnectionHandler)));
            }
        }

        public ObservableCollection<OnionDiagramStakeholderViewModel> OnionDiagramStakeholders { get; }

        private void OnionDiagramStakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Add(new OnionDiagramStakeholderViewModel(diagram, item, selectionRegister, drawConnectionHandler));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<OnionDiagramStakeholder>())
                    OnionDiagramStakeholders.Remove(OnionDiagramStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }
    }
}
