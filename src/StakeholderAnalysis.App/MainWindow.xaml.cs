using System.ComponentModel;
using System.Windows;
using Fluent;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs eLoaded)
        {
            DataContext = new MainWindowViewModel(new StakeholderAnalysisGui());
        }

        private void MainWindowClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel) viewModel.ForcedClosingMainWindow();
        }
    }
}