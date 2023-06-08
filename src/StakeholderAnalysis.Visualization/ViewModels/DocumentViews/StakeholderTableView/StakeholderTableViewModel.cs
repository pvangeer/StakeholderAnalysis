using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands.Diagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView
{
    public class StakeholderTableViewModel : ViewModelBase, ISelectable
    {
        private readonly Analysis analysis;

        public StakeholderTableViewModel(ViewModelFactory factory, Analysis analysis) : base(factory)
        {
            this.analysis = analysis;
            if (analysis != null)
            {
                analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;

                Stakeholders = new ObservableCollection<TableStakeholderViewModel>(
                    this.analysis.Stakeholders.Select(stakeholder =>
                        ViewModelFactory.CreateTableStakeholderViewModel(stakeholder)));
                Stakeholders.CollectionChanged += StakeholderViewModelsCollectionChanged;

                foreach (var analysisStakeholderType in analysis.StakeholderTypes)
                    analysisStakeholderType.PropertyChanged += StakeholderTypePropertyChanged;

                analysis.StakeholderTypes.CollectionChanged += StakeholderTypesCollectionChanged;

                StakeholderViewSource = new CollectionViewSource
                {
                    Source = Stakeholders,
                    GroupDescriptions = { new PropertyGroupDescription(nameof(TableStakeholderViewModel.Type)) }
                };
            }
        }

        public bool StakeholderTypesChangedProperty { get; set; }

        public CollectionViewSource StakeholderViewSource { get; }

        public ObservableCollection<TableStakeholderViewModel> Stakeholders { get; }

        public ObservableCollection<StakeholderType> StakeholderTypes => analysis.StakeholderTypes;

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return "StakeholderTable";
        }

        public IEnumerable<IStakeholderDiagram> AllDiagrams => analysis.OnionDiagrams.OfType<IStakeholderDiagram>()
            .Concat(analysis.ForceFieldDiagrams)
            .Concat(analysis.AttitudeImpactDiagrams)
            .ToList();

        public ICommand DeleteStakeholderCommand => new RemoveSelectedStakeholderCommand(this, analysis);

        private void StakeholderTypePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderType.Name):
                    OnPropertyChanged(nameof(StakeholderTypesChangedProperty));
                    break;
            }
        }

        private void StakeholderTypesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var stakeholderType in e.NewItems.OfType<StakeholderType>())
                        stakeholderType.PropertyChanged += StakeholderTypePropertyChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderType in e.NewItems.OfType<StakeholderType>())
                        stakeholderType.PropertyChanged -= StakeholderTypePropertyChanged;
                    break;
            }
        }

        private void StakeholderViewModelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (analysis == null) return;

            analysis.Stakeholders.CollectionChanged -= StakeholdersCollectionChanged;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var stakeholderType = GetDefaultStakeholderType();
                    foreach (var stakeholderViewModel in e.NewItems.OfType<TableStakeholderViewModel>())
                    {
                        var stakeholder = stakeholderViewModel.Stakeholder;
                        stakeholder.Type = stakeholderType;
                        stakeholder.OnPropertyChanged(nameof(Stakeholder.Type));
                        analysis.Stakeholders.Add(stakeholder);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderViewModel in e.OldItems.OfType<TableStakeholderViewModel>())
                        AnalysisServices.RemoveStakeholderFromAnalysis(analysis, stakeholderViewModel.Stakeholder);

                    break;
            }

            analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
        }

        private StakeholderType GetDefaultStakeholderType()
        {
            var stakeholderType = new StakeholderType();
            if (!analysis.StakeholderTypes.Any())
                analysis.StakeholderTypes.Add(stakeholderType);
            else
                stakeholderType = analysis.StakeholderTypes.First();

            return stakeholderType;
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    Stakeholders.Add(ViewModelFactory.CreateTableStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    Stakeholders.Remove(Stakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }

        public override bool IsViewModelFor(object o)
        {
            return o is string s && s == "StakeholderTable";
        }
    }
}