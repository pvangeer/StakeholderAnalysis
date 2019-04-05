using System.Dynamic;

namespace StakeholderAnalysis.Data
{
    public class Stakeholder : PropertyChangedElement
    {
        public Stakeholder(string name, double leftPercentage, double topPercentage, StakeholderType type)
        {
            Name = name;
            LeftPercentage = leftPercentage;
            TopPercentage = topPercentage;
            Type = type;
        }

        public string Name { get; set; }

        public double LeftPercentage { get; set; }

        public double TopPercentage { get; set; }

        public StakeholderType Type { get; }
    }
}
