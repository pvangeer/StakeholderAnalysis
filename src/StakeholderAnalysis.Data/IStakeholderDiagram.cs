using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public interface IStakeholderDiagram : INotifyPropertyChangedImplementation
    {
        ObservableCollection<PositionedStakeholder> Stakeholders { get; }
    
        string Name { get; set; }

        object Clone();
    }
}