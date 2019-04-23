namespace StakeholderAnalysis.Data
{
    public class Stakeholder : NotifyPropertyChangedObservable
    {
        public Stakeholder(string name, double interest, double influence,
            double attitude, double impact, StakeholderType type)
        {
            Name = name;
            Type = type;
            Interest = interest;
            Influence = influence;
            Attitude = attitude;
            Impact = impact;
        }

        public string Name { get; set; }

        public double Interest { get; set; }

        public double Influence { get; set; }

        public double Attitude { get; set; }

        public double Impact { get; set; }

        public StakeholderType Type { get; }
    }
}