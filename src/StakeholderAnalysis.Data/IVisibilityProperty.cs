namespace StakeholderAnalysis.Data
{
    public interface IVisibilityProperty: INotifyPropertyChangedImplementation
    {
        bool Visible { get; set; }
    }
}
