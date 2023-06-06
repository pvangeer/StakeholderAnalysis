using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;

namespace StakeholderAnalysis.Visualization.Controls
{
    /// <summary>
    ///     Interaction logic for SelectStakeholdersDialog.xaml
    /// </summary>
    public partial class SelectStakeholdersDialog
    {
        public SelectStakeholdersDialog(Analysis analysis)
        {
            InitializeComponent();
            ListBox.ItemsSource = analysis.Stakeholders.Select(st => new DiagramStakeholderViewModelBase(null, st, null, null))
                .ToList();
        }

        public IEnumerable<Stakeholder> SelectedStakeholders { get; private set; }

        private void ButtonCancelClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonOkClicked(object sender, RoutedEventArgs e)
        {
            var selectedStakeholders = new List<Stakeholder>();
            foreach (var item in ListBox.SelectedItems.OfType<DiagramStakeholderViewModelBase>())
                selectedStakeholders.Add(item.Stakeholder);

            SelectedStakeholders = selectedStakeholders;
            DialogResult = true;
            Close();
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            ListBox.Focus();
        }

        // Can execute
        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Close
        private void CloseDialog(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}