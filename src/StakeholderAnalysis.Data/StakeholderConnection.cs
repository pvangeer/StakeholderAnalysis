namespace StakeholderAnalysis.Data
{
    public class StakeholderConnection
    {
        public StakeholderConnection(ConnectorGroup group, Stakeholder connectFrom, Stakeholder connectTo)
        {
            ConnectionGroup = group;
            ConnectFrom = connectFrom;
            ConnectTo = connectTo;
        }

        public ConnectorGroup ConnectionGroup { get; }

        public Stakeholder ConnectFrom { get; }

        public Stakeholder ConnectTo { get; }
    }
}
