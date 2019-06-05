using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.Commands;

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

            OnionDiagramStakeholdersViewModel = ViewModelFactory.CreateOnionDiagramStakeholdersViewModel(onionDiagram, this, OnionDiagramDrawConnectionViewModel);
        }

        public OnionDiagramRingsCanvasViewModel OnionDiagramRingsCanvasViewModel => ViewModelFactory.CreateOnionDiagramRingsCanvasViewModel(diagram);

        public OnionDiagramConnectionsPresenterViewModel OnionDiagramConnectionsPresenterViewModel => ViewModelFactory.CreateOnionDiagramConnectionsPresenterViewModel(diagram);

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
            selectedObject = o;
            foreach (var onionDiagramStakeholderViewModel in OnionDiagramStakeholdersViewModel.OnionDiagramStakeholders)
            {
                onionDiagramStakeholderViewModel.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder));
            }
        }
    }
}
