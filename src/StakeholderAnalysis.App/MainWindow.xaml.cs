using System.Windows;
using Fluent;
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

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel(AnalysisGenerator.GetAnalysis(), new Gui.Gui());
        }
    }
}