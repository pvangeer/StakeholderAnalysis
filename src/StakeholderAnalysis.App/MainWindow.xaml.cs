using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var analysis = new Analysis();
            analysis.Onion.Rings.Add(new OnionRing("Ring 1", 1.0) { BackgroundColor = Colors.LavenderBlush });
            analysis.Onion.Rings.Add(new OnionRing("Ring 1", 0.9) { BackgroundColor = Colors.CadetBlue });
            analysis.Onion.Rings.Add(new OnionRing("Ring 1", 0.6) { BackgroundColor = Colors.AliceBlue });
            analysis.Onion.Rings.Add(new OnionRing("Ring 1", 0.3) { BackgroundColor = Colors.Beige });
            analysis.Stakeholders.Add(new Stakeholder("HHNK",0.3,0.6, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("WVL", 0.5, 0.5, StakeholderType.Rijksoverheid));

            DataContext = new MainWindowViewModel(analysis);
        }
    }
}
