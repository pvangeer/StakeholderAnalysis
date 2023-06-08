using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Controls;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;

namespace StakeholderAnalysis.Visualization.Commands.Ribbon
{
    public class AddStakeholdersToDiagramCommand : ICommand
    {
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        private IStakeholderDiagram selectedDiagram;

        public AddStakeholdersToDiagramCommand(ViewManager viewManager, Analysis analysis)
        {
            this.viewManager = viewManager;
            if (viewManager != null) viewManager.PropertyChanged += ViewManagerPropertyChanged;
            this.analysis = analysis;
        }

        public bool CanExecute(object parameter)
        {
            return selectedDiagram != null;
        }

        public void Execute(object parameter)
        {
            // Show dialog and select stakeholders
            var dialog = new SelectStakeholdersDialog(analysis)
            {
                Owner = Application.Current.MainWindow
            };

            if (dialog.ShowDialog() != true) return;

            foreach (var selectedStakeholder in dialog.SelectedStakeholders.ToArray())
                AnalysisServices.AddStakeholderToDiagram(selectedDiagram, selectedStakeholder);
        }

        public event EventHandler CanExecuteChanged;

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    selectedDiagram = viewManager?.ActiveDocument?.ViewModel is IDiagramViewModel diagramViewModel
                        ? diagramViewModel.GetDiagram()
                        : null;

                    CanExecuteChanged?.Invoke(this, null);
                    break;
            }
        }
    }
}