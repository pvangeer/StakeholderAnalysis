using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagram : NotifyPropertyChangedObservable, IRankedStakeholderDiagram<AttitudeImpactDiagramStakeholder>, ITwoAxisDiagram
    {
        public AttitudeImpactDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<AttitudeImpactDiagramStakeholder>();
            BackgroundBrush = new LinearGradientBrush(Colors.LightYellow, Colors.PaleVioletRed, new Point(0, 0), new Point(1, 1));
            BackgroundTextLeftTop = "Informeren";
            BackgroundTextLeftBottom = "Monitoren";
            BackgroundTextRightTop = "Betrekken";
            BackgroundTextRightBottom = "Overtuigen";
            YAxisMaxLabel = "Positief";
            YAxisMinLabel = "Negatief";
            XAxisMaxLabel = "Hoge impact";
            XAxisMinLabel = "Lage impact";
        }

        public AttitudeImpactDiagram() : this("") { }

        public string Name { get; set; }

        public Brush BackgroundBrush { get; set; }

        public string BackgroundTextLeftTop { get; set; }

        public string BackgroundTextRightTop { get; set; }

        public string BackgroundTextLeftBottom { get; set; }

        public string BackgroundTextRightBottom { get; set; }

        public string YAxisMaxLabel { get; set; }

        public string YAxisMinLabel { get; set; }

        public string XAxisMaxLabel { get; set; }

        public string XAxisMinLabel { get; set; }

        public ObservableCollection<AttitudeImpactDiagramStakeholder> Stakeholders { get; }
    }
}
