using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;

namespace StakeholderAnalysis.Data.Diagrams.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagram : TwoAxisDiagram
    {
        public AttitudeImpactDiagram(string name)
        {
            Name = name;
            BrushStartColor = Colors.LightYellow;
            BrushEndColor = Colors.PaleVioletRed;
            BackgroundTextLeftTop = "Informeren";
            BackgroundTextLeftBottom = "Monitoren";
            BackgroundTextRightTop = "Betrekken";
            BackgroundTextRightBottom = "Overtuigen";
            BackgroundFontFamily = SystemFonts.CaptionFontFamily;
            BackgroundFontColor = Colors.DimGray;
            BackgroundFontSize = 64;
            BackgroundFontBold = true;
            BackgroundFontItalic = true;
            YAxisMaxLabel = "Positief";
            YAxisMinLabel = "Negatief";
            XAxisMaxLabel = "Hoge impact";
            XAxisMinLabel = "Lage impact";
            AxisFontFamily = SystemFonts.CaptionFontFamily;
            AxisFontColor = Colors.Black;
            AxisFontSize = 24;
            AxisFontBold = false;
            AxisFontItalic = false;
        }

        public AttitudeImpactDiagram() : this("")
        {
        }
    }
}