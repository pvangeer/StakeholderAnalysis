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
            var mainWindowViewModel = new MainWindowViewModel(new StakeholderAnalysisGui(AnalysisGenerator.GetAnalysis()));
            DataContext = mainWindowViewModel;

            mainWindowViewModel.OnInvalidateVisual += (o, e) =>
            {
                //InvalidateVisual();
            };

        }

        private void MainWindowClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.ForcedClosingMainWindow();
            }
        }
    }
}