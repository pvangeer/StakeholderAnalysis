using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public class ConnectorGroup : PropertyChangedElement
    {
        public ConnectorGroup(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }

        public string Name { get; set; }

        public Color Color { get; set; }
    }
}
