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
            if (DataContext is MainWindowViewModel viewModel)
                e.Cancel = !viewModel.ForcedClosingMainWindow();
        }

        private void DropHandler(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null && files.Length > 0)
                {
                    (DataContext as MainWindowViewModel)?.RibbonViewModel.OpenFile(files[0]);
                }
            }
        }
    }
}