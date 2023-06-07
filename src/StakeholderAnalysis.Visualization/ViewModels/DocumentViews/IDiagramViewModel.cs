using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews
{
    public interface IDiagramViewModel
    {
        IStakeholderDiagram GetDiagram();
    }
}