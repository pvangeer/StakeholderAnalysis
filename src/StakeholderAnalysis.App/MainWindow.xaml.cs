using System.Linq;
using System.Windows;
using System.Windows.Media;
using Fluent;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var analysis = AnalysisGenerator.GetAnalysis();
            
            var mainWindowViewModel = new MainWindowViewModel(analysis);
            mainWindowViewModel.SelectedStakeholder = mainWindowViewModel.Stakeholders.Last();
            DataContext = mainWindowViewModel;
            
        }
    }

    public class AnalysisGenerator
    {
        
        public static Analysis GetAnalysis()
        {
            var analysis = new Analysis();
            // Ringen
            analysis.Onion.Rings.Add(new OnionRing("", 1.0) { BackgroundColor = Colors.LightBlue});
            analysis.Onion.Rings.Add(new OnionRing("", 0.7) { BackgroundColor = Colors.CornflowerBlue });
            analysis.Onion.Rings.Add(new OnionRing("", 0.3) { BackgroundColor = Colors.DarkSlateBlue });

            //Team
            analysis.Stakeholders.Add(new Stakeholder("WVL", 0.5, 0.8, 0.7,1.0, StakeholderType.Rijksoverheid));
            analysis.Stakeholders.Add(new Stakeholder("DGWB", 0.45, 0.75,0.5,1.0, StakeholderType.Rijksoverheid));
            analysis.Stakeholders.Add(new Stakeholder("Deltares", 0.55, 0.75, 0.7, 0.8, StakeholderType.Rijksoverheid));
            analysis.Stakeholders.Add(new Stakeholder("Markt (ontwikkelaars)", 0.5, 0.65, 0.75,0.7, StakeholderType.Rijksoverheid));

            // Groepen
            analysis.Stakeholders.Add(new Stakeholder("KKP", 0.4, 0.5, 1.0, 0.5, StakeholderType.Stakeholdergroep));
            analysis.Stakeholders.Add(new Stakeholder("UvW", 0.44, 0.48, 0.8, 0.6, StakeholderType.Stakeholdergroep));
            analysis.Stakeholders.Add(new Stakeholder("ENW", 0.7, 0.6, 0.4, 0.4, StakeholderType.Stakeholdergroep));
            analysis.Stakeholders.Add(new Stakeholder("WWK/CWK", 0.5, 0.45, 0.6, 0.5, StakeholderType.Stakeholdergroep));
            analysis.Stakeholders.Add(new Stakeholder("AIO/DKI", 0.55, 0.48, 0.5, 0.7, StakeholderType.Stakeholdergroep));
            var coastGroupStakeholder = new Stakeholder("Themagroep Kust", 0.45, 0.4, 0.7, 0.3, StakeholderType.Stakeholdergroep);
            analysis.Stakeholders.Add(coastGroupStakeholder);

            // Waterschappen
            var hhnk = new Stakeholder("Hollands Noorderkwartier", 0.16, 0.8, 0.8, 0.4, StakeholderType.Waterschap);
            var scheldestromen = new Stakeholder("Scheldestromen", 0.44, 0.08, 0.8, 0.4, StakeholderType.Waterschap);
            var wetterskip = new Stakeholder("Wetterskip", 0.14, 0.61, 0.8, 0.4, StakeholderType.Waterschap);
            var rijnland = new Stakeholder("Rijnland", 0.26, 0.26, 0.8, 0.4, StakeholderType.Waterschap);
            var delfland = new Stakeholder("Delfland", 0.43, 0.2, 0.8, 0.4, StakeholderType.Waterschap);
            var hollandseDelta = new Stakeholder("Hollandse Delta", 0.36, 0.1, 0.8, 0.4, StakeholderType.Waterschap);
            var rws = new Stakeholder("RWS", 0.09, 0.47, 0.8, 0.4, StakeholderType.Waterschap);

            analysis.Stakeholders.Add(hhnk);
            analysis.Stakeholders.Add(wetterskip);
            analysis.Stakeholders.Add(new Stakeholder("Noordezijlvest", 0.1, 0.72, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Hunze en Aa's", 0.21, 0.16, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Drents Overijsselse Delta", 0.07, 0.60, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Zuiderzeeland", 0.14, 0.50, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(rws);
            analysis.Stakeholders.Add(new Stakeholder("Amstel Gooi en Vecht", 0.11, 0.36, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Vallei en Veluwen", 0.18, 0.39, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Rijn en IJssel", 0.21, 0.29, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(rijnland);
            analysis.Stakeholders.Add(new Stakeholder("Aa en Maas", 0.31, 0.23, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Limburg", 0.37, 0.22, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Brabantse Delta", 0.29, 0.11, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(delfland);
            analysis.Stakeholders.Add(hollandseDelta);
            analysis.Stakeholders.Add(scheldestromen);
            analysis.Stakeholders.Add(new Stakeholder("Rivierenland", 0.48, 0.16, 0.8, 0.4, StakeholderType.Waterschap));
            analysis.Stakeholders.Add(new Stakeholder("Stichtse Rijnlanden", 0.5, 0.08, 0.8, 0.4, StakeholderType.Waterschap));

            // Connectiongroepen
            var coastGroup = new ConnectionGroup("Themagroep kust",Colors.DarkRed);

            // Connections
            analysis.Connections.Add(new StakeholderConnection(coastGroup, hhnk, coastGroupStakeholder));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, scheldestromen, coastGroupStakeholder));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, wetterskip, coastGroupStakeholder));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, rijnland, coastGroupStakeholder));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, delfland, coastGroupStakeholder));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, hollandseDelta, coastGroupStakeholder));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, rws, coastGroupStakeholder));

            return analysis;
        }
    }
}
