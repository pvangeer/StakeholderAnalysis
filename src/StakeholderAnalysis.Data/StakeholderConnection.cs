namespace StakeholderAnalysis.Data
{
    public class StakeholderConnection : NotifyPropertyChangedObservable
    {
        public StakeholderConnection(StakeholderConnectionGroup group, Stakeholder connectFrom, Stakeholder connectTo)
        {
            StakeholderConnectionGroup = group;
            ConnectFrom = connectFrom;
            ConnectTo = connectTo;
        }

        public StakeholderConnectionGroup StakeholderConnectionGroup { get; }

        public Stakeholder ConnectFrom { get; }

        public Stakeholder ConnectTo { get; }
    }
}