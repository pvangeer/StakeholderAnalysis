namespace StakeholderAnalysis.Data
{
    public class StakeholderConnection
    {
        public StakeholderConnection(Stakeholder connectFrom, Stakeholder connectTo)
        {
            ConnectFrom = connectFrom;
            ConnectTo = connectTo;
        }

        public Stakeholder ConnectFrom { get; }

        public Stakeholder ConnectTo { get; }
    }
}
