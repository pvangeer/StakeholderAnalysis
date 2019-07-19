using System.Windows.Media;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public interface ITwoAxisDiagram : INotifyPropertyChangedImplementation
    {
        string Name { get; set; }

        Color BrushStartColor { get; set; }

        Color BrushEndColor { get; set; }

        string BackgroundTextLeftTop { get; set; }

        string BackgroundTextRightTop { get; set; }

        string BackgroundTextLeftBottom { get; set; }

        string BackgroundTextRightBottom { get; set; }

        string YAxisMaxLabel { get; set; }

        string YAxisMinLabel { get; set; }

        string XAxisMaxLabel { get; set; }

        string XAxisMinLabel { get; set; }
    }
}