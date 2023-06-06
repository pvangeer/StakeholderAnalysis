using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews
{
    public class DiagramStakeholderViewModelBase : TableStakeholderViewModel, IDropHandler, IRemoveStakeholderViewModel
    {
        private readonly IDrawConnectionHandler drawConnectionHandler;
        private readonly ISelectionRegister selectionRegister;

        public DiagramStakeholderViewModelBase(ViewModelFactory factory, Stakeholder stakeholder,
            ISelectionRegister selectionRegister, IDrawConnectionHandler drawConnectionHandler) : base(factory, stakeholder)
        {
            this.drawConnectionHandler = drawConnectionHandler;
            this.selectionRegister = selectionRegister;
        }

        public bool IsConnectionToTarget =>
            drawConnectionHandler != null && drawConnectionHandler.IsConnectionTarget(Stakeholder);

        public ICommand StakeholderClickedCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            selectionRegister?.SelectObject(Stakeholder);
        });

        public virtual void Moved(double xRelativeNew, double yRelativeNew)
        {
        }

        public bool IsSelectedStakeholder => selectionRegister != null && selectionRegister.IsSelectedObject(Stakeholder);

        public virtual void RemoveFromDiagram()
        {
        }

        public ICommand RemoveStakeholderCommand =>
            CommandFactory.CreateRemoveSelectedStakeholderFromDiagramCommand(this);

        public bool IsViewModelFor(Stakeholder stakeholder)
        {
            return Stakeholder == stakeholder;
        }
    }
}