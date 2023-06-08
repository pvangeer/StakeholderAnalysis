using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public interface IStakeholderDiagram<TStakeholder> : IStakeholderDiagram where TStakeholder : class, IRankedStakeholder
    {
        ObservableCollection<TStakeholder> Stakeholders { get; }
    }

    public interface IStakeholderDiagram: INotifyPropertyChangedImplementation
    {
        string Name { get; set; }

        object Clone();
    }
}