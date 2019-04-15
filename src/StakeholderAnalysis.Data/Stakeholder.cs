using System.Dynamic;

namespace StakeholderAnalysis.Data
{
    public class Stakeholder : PropertyChangedElement
    {
        public Stakeholder(string name, double leftPercentage, double topPercentage, double interest, double influence, StakeholderType type)
        {
            Name = name;
            LeftPercentage = leftPercentage;
            TopPercentage = topPercentage;
            Type = type;
            Interest = interest;
            Influence = influence;
        }

        public string Name { get; set; }

        public double LeftPercentage { get; set; }

        public double TopPercentage { get; set; }

        public double Interest { get; set; }

        public double Influence { get; set; }

        public StakeholderType Type { get; }
    }
}
