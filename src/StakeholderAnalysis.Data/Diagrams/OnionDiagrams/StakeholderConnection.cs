namespace StakeholderAnalysis.Data.Diagrams.OnionDiagrams
{
    public class StakeholderConnection : NotifyPropertyChangedObservable
    {
        public StakeholderConnection(StakeholderConnectionGroup group, PositionedStakeholder connectFrom,
            PositionedStakeholder connectTo)
        {
            StakeholderConnectionGroup = group;
            ConnectFrom = connectFrom;
            ConnectTo = connectTo;
        }

        public StakeholderConnectionGroup StakeholderConnectionGroup { get; }

        public PositionedStakeholder ConnectFrom { get; }

        public PositionedStakeholder ConnectTo { get; }
    }
}