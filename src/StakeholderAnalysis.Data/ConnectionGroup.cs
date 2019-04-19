using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public class ConnectionGroup : PropertyChangedElement
    {
        public ConnectionGroup(string name, Color color, bool visible = true)
        {
            Name = name;
            Color = color;
            Visible = visible;
        }

        public string Name { get; set; }

        public Color Color { get; set; }

        public bool Visible { get; set; }
    }
}