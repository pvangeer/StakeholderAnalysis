using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Controls;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.Commands.Ribbon
{
    public class AddStakeholdersToDiagramCommand : ICommand
    {
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;
        private AttitudeImpactDiagram selectedAttitudeImpactDiagram;

        private ForceFieldDiagram selectedForceFieldDiagram;

        // TODO: Solve this with a generic interfact (similar to IRankedStakeholderDiagram)
        private OnionDiagram selectedOnionDiagram;

        public AddStakeholdersToDiagramCommand(ViewManager viewManager, Analysis analysis)
        {
            this.viewManager = viewManager;
            if (viewManager != null) viewManager.PropertyChanged += ViewManagerPropertyChanged;
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return selectedOnionDiagram != null || selectedAttitudeImpactDiagram != null ||
                   selectedForceFieldDiagram != null;
        }

        public void Execute(object parameter)
        {
            // Show dialog and select stakeholders
            var dialog = new SelectStakeholdersDialog(analysis)
            {
                Owner = Application.Current.MainWindow
            };

            if (dialog.ShowDialog() != true) return;

            // Add stakeholders to viewManager
            var selectedStakeholders = dialog.SelectedStakeholders.ToArray();

            if (selectedOnionDiagram != null)
            {
                var currentStakeholders = selectedOnionDiagram.Stakeholders.Select(s => s.Stakeholder);
                foreach (var selectedStakeholder in selectedStakeholders.Except(currentStakeholders))
                {
                    var onionDiagramStakeholder = new OnionDiagramStakeholder(selectedStakeholder, 0.5, 0.5)
                    {
                        Rank = selectedOnionDiagram.Stakeholders.Count
                    };
                    selectedOnionDiagram.Stakeholders.Add(onionDiagramStakeholder);
                }
            }

            if (selectedAttitudeImpactDiagram != null)
            {
                var currentStakeholders = selectedAttitudeImpactDiagram.Stakeholders.Select(s => s.Stakeholder);
                foreach (var selectedStakeholder in selectedStakeholders.Except(currentStakeholders))
                {
                    var attitudeImpactDiagramStakeholder =
                        new AttitudeImpactDiagramStakeholder(selectedStakeholder, 0.5, 0.5)
                        {
                            Rank = selectedAttitudeImpactDiagram.Stakeholders.Count
                        };
                    selectedAttitudeImpactDiagram.Stakeholders.Add(attitudeImpactDiagramStakeholder);
                }
            }

            if (selectedForceFieldDiagram != null)
            {
                var currentStakeholders = selectedForceFieldDiagram.Stakeholders.Select(s => s.Stakeholder);
                foreach (var selectedStakeholder in selectedStakeholders.Except(currentStakeholders))
                {
                    var forceFieldDiagramStakeholder = new ForceFieldDiagramStakeholder(selectedStakeholder, 0.5, 0.5)
                    {
                        Rank = selectedForceFieldDiagram.Stakeholders.Count
                    };
                    selectedForceFieldDiagram.Stakeholders.Add(forceFieldDiagramStakeholder);
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    selectedOnionDiagram = viewManager?.ActiveDocument?.ViewModel is OnionDiagramViewModel viewModel
                        ? viewModel.GetDiagram()
                        : null;
                    selectedForceFieldDiagram =
                        viewManager?.ActiveDocument?.ViewModel is ForceFieldDiagramViewModel viewModel2
                            ? viewModel2.GetDiagram() as ForceFieldDiagram
                            : null;
                    selectedAttitudeImpactDiagram =
                        viewManager?.ActiveDocument?.ViewModel is AttitudeImpactDiagramViewModel viewModel3
                            ? viewModel3.GetDiagram() as AttitudeImpactDiagram
                            : null;

                    CanExecuteChanged?.Invoke(this, null);
                    break;
            }
        }
    }
}