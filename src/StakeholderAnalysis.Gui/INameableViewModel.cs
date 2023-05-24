using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Gui
{
    public interface INameableViewModel : INotifyPropertyChangedImplementation
    {
        string DisplayName { get; set; }
    }
}