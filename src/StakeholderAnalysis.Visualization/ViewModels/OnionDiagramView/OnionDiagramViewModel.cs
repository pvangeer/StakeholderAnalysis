using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramViewModel: ViewModelBase, ISelectionRegister
    {
        private readonly OnionDiagram diagram;
        private object selectedObject = null;

        public OnionDiagramViewModel(ViewModelFactory factory, OnionDiagram onionDiagram) : base(factory)
        {
            diagram = onionDiagram;
            OnionDiagramDrawConnectionViewModel = factory.CreateOnionDiagramDrawConnectionViewModel(onionDiagram);
            OnionDiagramRingsCanvasViewModel = ViewModelFactory.CreateOnionDiagramRingsCanvasViewModel(diagram);
            OnionDiagramConnectionsPresenterViewModel = ViewModelFactory.CreateOnionDiagramConnectionsPresenterViewModel(diagram, this);
            OnionDiagramStakeholdersViewModel = ViewModelFactory.CreateOnionDiagramStakeholdersViewModel(onionDiagram, this, OnionDiagramDrawConnectionViewModel);
        }

        public OnionDiagramRingsCanvasViewModel OnionDiagramRingsCanvasViewModel { get; }

        public OnionDiagramConnectionsPresenterViewModel OnionDiagramConnectionsPresenterViewModel { get; }

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel { get; }

        public ICommand GridClickedCommand => CommandFactory.CreateClearSelectionCommand(this);

        public OnionDiagramDrawConnectionViewModel OnionDiagramDrawConnectionViewModel { get; }

        public bool IsViewModelFor(OnionDiagram otherDiagram)
        {
            return diagram == otherDiagram;
        }

        public OnionDiagram GetDiagram()
        {
            return diagram;
        }


        public bool IsSelected(object o)
        {
            return selectedObject == o;
        }

        public void Select(object o)
        {
            var oldSelectedObject = selectedObject;
            selectedObject = o;

            RaiseIsSelectedPropertyChanged(oldSelectedObject);
            RaiseIsSelectedPropertyChanged(selectedObject);
        }

        private void RaiseIsSelectedPropertyChanged(object o)
        {
            if (o is Stakeholder stakeholder)
            {
                var viewModel = OnionDiagramStakeholdersViewModel.OnionDiagramStakeholders.FirstOrDefault(vm =>
                        vm.IsViewModelFor(stakeholder));
                viewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder));
            }

            if (o is StakeholderConnection connection)
            {
                var viewModel = OnionDiagramConnectionsPresenterViewModel.StakeholderConnections.FirstOrDefault(vm =>
                        vm.IsViewModelFor(connection));
                viewModel?.OnPropertyChanged(nameof(StakeholderConnectionViewModel.IsSelected));
            }
        }
    }
}
