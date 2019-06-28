namespace StakeholderAnalysis.Data
{
    public interface INameProperty : INotifyPropertyChangedImplementation
    {
        string Name { get; set; }
    }
}
