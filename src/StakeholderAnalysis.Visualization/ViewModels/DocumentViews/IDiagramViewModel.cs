using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews
{
    public interface IDiagramViewModel
    {
        IStakeholderDiagram GetDiagram();
    }
}