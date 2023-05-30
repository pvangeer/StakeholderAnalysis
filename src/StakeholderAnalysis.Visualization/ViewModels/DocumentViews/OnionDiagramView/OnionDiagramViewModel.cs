using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class OnionDiagramViewModel : ViewModelBase, ISelectionRegister, INameableViewModel, ISelectable
    {
        private readonly OnionDiagram diagram;
        private object selectedObject;

        public OnionDiagramViewModel(ViewModelFactory factory, OnionDiagram onionDiagram) : base(factory)
        {
            diagram = onionDiagram;
            onionDiagram.PropertyChanged += DiagramPropertyChanged;
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

        public string DisplayName
        {
            get => diagram.Name;
            set
            {
                if (diagram != null)
                {
                    diagram.Name = value;
                    diagram.OnPropertyChanged(nameof(OnionDiagram.Name));
                }
            }
        }

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return diagram;
        }

        public bool IsSelectedObject(object o)
        {
            return selectedObject == o;
        }

        public void SelectObject(object o)
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
            return ViewModelFactory.CreateOnionDiagramPropertiesViewModel(diagram);
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }

        private void RaiseIsSelectedPropertyChanged(object o)
        {
            if (o is Stakeholder stakeholder)
            {
                var viewModel = OnionDiagramStakeholdersViewModel.OnionDiagramStakeholders.FirstOrDefault(vm =>
                    vm.IsViewModelFor(stakeholder));
                viewModel?.OnPropertyChanged(nameof(OnionDiagramStakeholderViewModel.IsSelectedStakeholder));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return o == diagram;
        }
    }
}