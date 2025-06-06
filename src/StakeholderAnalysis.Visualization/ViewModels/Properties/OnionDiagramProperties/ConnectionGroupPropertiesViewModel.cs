using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class ConnectionGroupPropertiesViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly StakeholderConnectionGroup connectionGroup;
        private readonly OnionDiagram diagram;
        private bool isExpanded;
        private readonly StakeholderConnectionGroupSelection connectionGroupSelection;

        public ConnectionGroupPropertiesViewModel(ViewModelFactory factory, StakeholderConnectionGroup connectionGroup,
            OnionDiagram selectedOnionDiagram, StakeholderAnalysisGui gui) : base(factory)
        {
            diagram = selectedOnionDiagram;
            this.connectionGroup = connectionGroup;
            connectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;

            if (gui != null)
            {
                this.connectionGroupSelection = gui.SelectedStakeholderGroupRegister.SelectedStakeholderConnectionGroups.FirstOrDefault(g => g.OnionDiagram == diagram);
                if (connectionGroupSelection != null)
                {
                    connectionGroupSelection.PropertyChanged += OnionDiagramSelectedConnectionGroupChanged;
                }
            }

            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.Name), "Naam"),
                new BooleanPropertyValueTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.Visible), "Weergeven"),
                new ColorPropertyValueTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.StrokeColor), "Lijnkleur"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.StrokeThickness), "Lijndikte", 0.0, 40.0, 0.5, "0.##"),
                new LineStylePropertyValueTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.LineStyle), "Lijnstijl")
            };

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        private void OnionDiagramSelectedConnectionGroupChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsVisible));
        }

        public bool IsVisible
        {
            get => connectionGroupSelection.StakeholderConnectionGroup == connectionGroup;
            set
            {
                if (value == false)
                    return;

                connectionGroupSelection.StakeholderConnectionGroup = connectionGroup;
                connectionGroupSelection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection.StakeholderConnectionGroup));
            }
        }

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return null;
        }

        public string DisplayName => connectionGroup.Name;

        public string IconSourceString { get; }

        public bool CanRemove => true;

        public bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            AnalysisServices.RemoveStakeholderConnectionGroupFromOnionDiagram(diagram, connectionGroup);
            connectionGroupSelection.StakeholderConnectionGroup = null;
            connectionGroupSelection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection.StakeholderConnectionGroup));
        });

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public override bool IsViewModelFor(object o)
        {
            return o as StakeholderConnectionGroup == connectionGroup;
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        private void ConnectionGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderConnectionGroup.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
                case nameof(StakeholderConnectionGroup.Visible):
                    OnPropertyChanged(nameof(IsSelected));
                    break;
            }
        }
    }
}