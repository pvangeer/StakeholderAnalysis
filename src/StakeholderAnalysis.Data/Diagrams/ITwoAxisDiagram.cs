using System.Windows.Media;

namespace StakeholderAnalysis.Data.Diagrams
{
    public interface ITwoAxisDiagram : IStakeholderDiagram
    {
        Color BrushStartColor { get; set; }

        Color BrushEndColor { get; set; }

        string BackgroundTextLeftTop { get; set; }

        string BackgroundTextRightTop { get; set; }

        string BackgroundTextLeftBottom { get; set; }

        string BackgroundTextRightBottom { get; set; }

        FontFamily BackgroundFontFamily { get; set; }

        Color BackgroundFontColor { get; set; }

        bool BackgroundFontBold { get; set; }

        bool BackgroundFontItalic { get; set; }

        double BackgroundFontSize { get; set; }

        string YAxisMaxLabel { get; set; }

        string YAxisMinLabel { get; set; }

        string XAxisMaxLabel { get; set; }

        string XAxisMinLabel { get; set; }

        FontFamily AxisFontFamily { get; set; }

        Color AxisFontColor { get; set; }

        bool AxisFontBold { get; set; }

        bool AxisFontItalic { get; set; }

        double AxisFontSize { get; set; }
    }
}