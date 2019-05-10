using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Controls
{
    /// <summary>
    /// Interaction logic for SelectStakeholdersDialog.xaml
    /// </summary>
    public partial class SelectStakeholdersDialog : Window
    {
        public SelectStakeholdersDialog(Analysis analysis)
        {
            InitializeComponent();
            ListBox.ItemsSource = analysis.Stakeholders.Select(st => new StakeholderViewModel(st)).ToList();
        }

        public IEnumerable<Stakeholder> SelectedStakeholders { get; private set; }

        private void ButtonCancelClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonOkClicked(object sender, RoutedEventArgs e)
        {
            var selectedStakeholders = new List<Stakeholder>();
            foreach (var item in ListBox.SelectedItems.OfType<StakeholderViewModel>())
            {
                selectedStakeholders.Add(item.Stakeholder);
            }

            SelectedStakeholders = selectedStakeholders;
            DialogResult = true;
            Close();
        }
    }
}
