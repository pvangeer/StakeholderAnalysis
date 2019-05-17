namespace StakeholderAnalysis.Data
{
    public class Stakeholder : NotifyPropertyChangedObservable
    {
        public Stakeholder(string name, StakeholderType type)
        {
            Name = name;
            Type = type;
        }

        public Stakeholder() { }

        public string Name { get; set; }

        public StakeholderType Type { get; set; }
    }
}