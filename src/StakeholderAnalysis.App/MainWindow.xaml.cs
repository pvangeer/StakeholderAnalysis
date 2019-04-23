using System.Linq;
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
            var analysis = AnalysisGenerator.GetAnalysis();

            var mainWindowViewModel = new MainWindowViewModel(analysis);
            //mainWindowViewModel.SelectedStakeholder = mainWindowViewModel.Stakeholders.Last();
            DataContext = mainWindowViewModel;
        }
    }
}