using System.Collections.ObjectModel;

namespace StakeholderAnalysis.Data
{
    public interface IRankedStakeholderDiagram<TStakeholder> where TStakeholder : class, IRankedStakeholder
    {
        ObservableCollection<TStakeholder> Stakeholders { get; }
    }
}