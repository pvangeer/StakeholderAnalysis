using System.Windows;
using System.Windows.Media;

namespace StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams
{
    public class ForceFieldDiagram : TwoAxisDiagram
    {
        public ForceFieldDiagram(string name)
        {
            Name = name;
            BrushStartColor = Colors.PowderBlue;
            BrushEndColor = Colors.LightGreen;
            BackgroundTextLeftTop = "Consulteren";
            BackgroundTextLeftBottom = "Monitoren";
            BackgroundTextRightTop = "Betrekken";
            BackgroundTextRightBottom = "Informeren";
            BackgroundFontFamily = SystemFonts.CaptionFontFamily;
            BackgroundFontColor = Colors.DimGray;
            BackgroundFontSize = 64;
            BackgroundFontBold = true;
            BackgroundFontItalic = true;
            YAxisMaxLabel = "Veel invloed";
            YAxisMinLabel = "Weinig invloed";
            XAxisMaxLabel = "Groot belang";
            XAxisMinLabel = "Klein belang";
            AxisFontFamily = SystemFonts.CaptionFontFamily;
            AxisFontColor = Colors.Black;
            AxisFontSize = 24;
            AxisFontBold = false;
            AxisFontItalic = false;
        }

        public ForceFieldDiagram() : this("")
        {
        }
    }
}