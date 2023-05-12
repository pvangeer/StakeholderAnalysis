namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class StakeholderConnection : NotifyPropertyChangedObservable
    {
        public StakeholderConnection(StakeholderConnectionGroup group, OnionDiagramStakeholder connectFrom,
            OnionDiagramStakeholder connectTo)
        {
            StakeholderConnectionGroup = group;
            ConnectFrom = connectFrom;
            ConnectTo = connectTo;
        }

        public StakeholderConnectionGroup StakeholderConnectionGroup { get; }

        public OnionDiagramStakeholder ConnectFrom { get; }

        public OnionDiagramStakeholder ConnectTo { get; }
    }
}