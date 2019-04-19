using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public class ConnectionGroup : PropertyChangedElement
    {
        public ConnectionGroup(string name, Color color, bool visible = true)
        {
            this.Name = name;
            this.Color = color;
            Visible = visible;
        }

        public string Name { get; set; }

        public Color Color { get; set; }

        public bool Visible { get; set; }
    }
}
