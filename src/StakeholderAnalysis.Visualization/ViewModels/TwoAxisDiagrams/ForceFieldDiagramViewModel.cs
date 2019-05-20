﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class ForceFieldDiagramViewModel : ITwoAxisDiagramViewModel
    {
        private ForceFieldDiagram diagram;
        private object selectedObject;

        public ForceFieldDiagramViewModel(ForceFieldDiagram diagram)
        {
            this.diagram = diagram;

            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new ForceFieldDiagramStakeholderViewModel(stakeholder, this)));
            }
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<ForceFieldDiagramStakeholder>())
                    PositionedStakeholders.Add(new ForceFieldDiagramStakeholderViewModel(item, this));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<ForceFieldDiagramStakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder.Stakeholder)));
        }

        public Brush BackgroundBrush => new LinearGradientBrush(Colors.CornflowerBlue, Colors.LimeGreen, new Point(0,1), new Point(1,0));

        public string BackgroundTextLeftTop => "Tevreden houden";

        public string BackgroundTextRightTop => "Betrekken";

        public string BackgroundTextLeftBottom => "Monitoren";

        public string BackgroundTextRightBottom => "Informeren";

        public string YAxisMaxLabel => "Veel invloed";
        public string YAxisMinLabel => "Weinig invloed";
        public string XAxisMaxLabel => "Groot belang";
        public string XAxisMinLabel => "Klein belang";

        public ICommand GridClickedCommand => new ClearSelectionCommand(this);

        public bool IsViewModelFor(ForceFieldDiagram otherDiagram)
        {
            return otherDiagram == diagram;
        }

        public bool IsSelected(object o)
        {
            return selectedObject == o;
        }

        public void Select(object o)
        {
            selectedObject = o;
            foreach (var stakeholderViewModel in PositionedStakeholders.OfType<StakeholderViewModel>())
            {
                stakeholderViewModel.OnPropertyChanged(nameof(StakeholderViewModel.IsSelectedStakeholder));
            }
        }
    }
}