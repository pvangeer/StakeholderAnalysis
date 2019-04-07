namespace StakeholderAnalysis.Data
{
    public class StakeholderConnection : PropertyChangedElement
    {
        public StakeholderConnection(ConnectionGroup group, Stakeholder connectFrom, Stakeholder connectTo)
        {
            ConnectionGroup = group;
            ConnectFrom = connectFrom;
            ConnectTo = connectTo;
        }

        public ConnectionGroup ConnectionGroup { get; }

        public Stakeholder ConnectFrom { get; }

        public Stakeholder ConnectTo { get; }
    }
}
