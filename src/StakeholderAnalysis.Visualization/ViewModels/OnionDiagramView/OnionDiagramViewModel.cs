using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramViewModel : ViewModelBase, ISelectionRegister
    {
        private readonly OnionDiagram diagram;
        private object selectedObject;

        public OnionDiagramViewModel(ViewModelFactory factory, OnionDiagram onionDiagram) : base(factory)
        {
            diagram = onionDiagram;
            OnionDiagramDrawConnectionViewModel = factory.CreateOnionDiagramDrawConnectionViewModel(onionDiagram);
            OnionDiagramRingsCanvasViewModel = ViewModelFactory.CreateOnionDiagramRingsCanvasViewModel(diagram);
            OnionDiagramConnectionsPresenterViewModel =
                ViewModelFactory.CreateOnionDiagramConnectionsPresenterViewModel(diagram);
            OnionDiagramStakeholdersViewModel =
                ViewModelFactory.CreateOnionDiagramStakeholdersViewModel(onionDiagram, this,
                    OnionDiagramDrawConnectionViewModel);
        }

        public OnionDiagramRingsCanvasViewModel OnionDiagramRingsCanvasViewModel { get; }

        public OnionDiagramConnectionsPresenterViewModel OnionDiagramConnectionsPresenterViewModel { get; }

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel { get; }

        public ICommand GridClickedCommand => CommandFactory.CreateClearSelectionCommand(this);

        public OnionDiagramDrawConnectionViewModel OnionDiagramDrawConnectionViewModel { get; }

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

        public bool IsViewModelFor(OnionDiagram otherDiagram)
        {
            return diagram == otherDiagram;
        }

        public OnionDiagram GetDiagram()
        {
            return diagram;
        }

        public OnionDiagramPropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateOnionDiagramPropertiesViewModel();
        }

        public OnionDiagramPropertiesViewModel PropertiesViewModel => ViewModelFactory.CreateOnionDiagramPropertiesViewModel();
        private void RaiseIsSelectedPropertyChanged(object o)
        {
            if (o is Stakeholder stakeholder)
            {
                var viewModel = OnionDiagramStakeholdersViewModel.OnionDiagramStakeholders.FirstOrDefault(vm =>
                    vm.IsViewModelFor(stakeholder));
                viewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder));
            }
        }
    }
}