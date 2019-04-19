using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Controls
{
    /// <summary>
    /// Interaction logic for ConnectionGroupsListBoxControl.xaml
    /// </summary>
    public partial class ConnectionGroupsListBoxControl : UserControl
    {
        public static readonly DependencyProperty StakeholderConnectionsListProperty = DependencyProperty.Register("StakeholderConnectionsList", typeof(ObservableCollection<ConnectionGroupViewModel>), typeof(ConnectionGroupsListBoxControl), new PropertyMetadata(default(ObservableCollection<ConnectionGroupViewModel>)));

        public ConnectionGroupsListBoxControl()
        {
            InitializeComponent();
        }

        public ObservableCollection<ConnectionGroupViewModel> StakeholderConnectionsList
        {
            get => (ObservableCollection<ConnectionGroupViewModel>) GetValue(StakeholderConnectionsListProperty);
            set => SetValue(StakeholderConnectionsListProperty, value);
        }
    }
}
