using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class ConnectionGroupPropertiesViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly StakeholderConnectionGroup connectionGroup;
        private readonly OnionDiagram diagram;
        private bool isExpanded;
        private bool isVisible;

        public ConnectionGroupPropertiesViewModel(ViewModelFactory factory, StakeholderConnectionGroup connectionGroup,
            OnionDiagram selectedOnionDiagram) : base(factory)
        {
            diagram = selectedOnionDiagram;
            this.connectionGroup = connectionGroup;
            connectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;

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

        public bool IsVisible
        {
            get => connectionGroup.Visible;
            set
            {
                if (value == connectionGroup.Visible)
                    return;
                connectionGroup.Visible = value;
                connectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Visible));
            }
        }

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject() => null;

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
            diagram.ConnectionGroups.Remove(connectionGroup);
        });

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
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