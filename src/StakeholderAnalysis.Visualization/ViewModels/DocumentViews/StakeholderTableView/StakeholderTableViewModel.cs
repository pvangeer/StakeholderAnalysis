﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
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

                Stakeholders = new ObservableCollection<StakeholderViewModel>(
                    this.analysis.Stakeholders.Select(stakeholder =>
                    {
                        var tableStakeholderViewModel = ViewModelFactory.CreateTableStakeholderViewModel(stakeholder);
                        tableStakeholderViewModel.PropertyChanged += StakeholderViewModelPropertyChanged;
                        return tableStakeholderViewModel;
                    }));

                Stakeholders.CollectionChanged += StakeholderViewModelsCollectionChanged;

                foreach (var analysisStakeholderType in analysis.StakeholderTypes)
                    analysisStakeholderType.PropertyChanged += StakeholderTypePropertyChanged;

                analysis.StakeholderTypes.CollectionChanged += StakeholderTypesCollectionChanged;

                StakeholderViewSource = new CollectionViewSource
                {
                    IsLiveGroupingRequested = true,
                    Source = Stakeholders,
                    GroupDescriptions = { new PropertyGroupDescription(nameof(StakeholderViewModel.Type)) }
                };
            }
        }

        public bool StakeholderTypesChangedProperty { get; set; }

        public CollectionViewSource StakeholderViewSource { get; }

        public ObservableCollection<StakeholderViewModel> Stakeholders { get; }

        public StakeholderDetailsViewModel LastSelectedStakeholderViewModel { get; set; }

        public ObservableCollection<StakeholderType> StakeholderTypes => analysis.StakeholderTypes;

        public ICommand DeleteStakeholderCommand => new RemoveSelectedStakeholderCommand(this, analysis);

        public ObservableCollection<OnionDiagram> OnionDiagrams => analysis.OnionDiagrams;

        public ObservableCollection<TwoAxisDiagram> ForceFieldDiagrams => analysis.ForceFieldDiagrams;

        public ObservableCollection<TwoAxisDiagram> AttitudeImpactDiagrams => analysis.AttitudeImpactDiagrams;

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return "StakeholderTable";
        }


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
                    foreach (var stakeholderViewModel in e.NewItems.OfType<StakeholderViewModel>())
                    {
                        var stakeholder = stakeholderViewModel.Stakeholder;
                        stakeholder.Type = stakeholderType;
                        stakeholder.OnPropertyChanged(nameof(Stakeholder.Type));
                        analysis.Stakeholders.Add(stakeholder);

                        stakeholderViewModel.PropertyChanged += StakeholderViewModelPropertyChanged;
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderViewModel in e.OldItems.OfType<StakeholderViewModel>())
                    {
                        stakeholderViewModel.PropertyChanged -= StakeholderViewModelPropertyChanged;
                        AnalysisServices.RemoveStakeholderFromAnalysis(analysis, stakeholderViewModel.Stakeholder);
                    }

                    break;
            }

            analysis.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
        }

        private void StakeholderViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is StakeholderViewModel stakeholder))
                return;

            switch (e.PropertyName)
            {
                case nameof(StakeholderViewModel.IsSelected):
                    if (stakeholder.IsSelected)
                    {
                        LastSelectedStakeholderViewModel = ViewModelFactory.CreateStakeholderDetailsViewModel(stakeholder.Stakeholder);
                        OnPropertyChanged(nameof(LastSelectedStakeholderViewModel));
                    }
                    else
                    {
                        if (LastSelectedStakeholderViewModel.IsViewModelFor(stakeholder.Stakeholder))
                        {
                            var selectedStakeholderViewModel = Stakeholders.FirstOrDefault(s => s.IsSelected);
                            LastSelectedStakeholderViewModel = selectedStakeholderViewModel == null
                                ? null
                                : ViewModelFactory.CreateStakeholderDetailsViewModel(selectedStakeholderViewModel?.Stakeholder);
                            OnPropertyChanged(nameof(LastSelectedStakeholderViewModel));
                        }
                    }

                    break;
            }
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