using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public static class ColorExtensions
    {
        public static Color Clone(this Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
