using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class ConnectionGroupPropertiesViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel,
        IQuickSelectionViewModel
    {
        private readonly StakeholderConnectionGroup connectionGroup;
        private readonly OnionDiagram diagram;
        private bool isExpanded;

        public ConnectionGroupPropertiesViewModel(ViewModelFactory factory, StakeholderConnectionGroup connectionGroup,
            OnionDiagram selectedOnionDiagram) : base(factory)
        {
            diagram = selectedOnionDiagram;
            this.connectionGroup = connectionGroup;
            connectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;

            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.Name), "Naam"),
                new BooleanPropertyTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.Visible), "Weergeven"),
                new ColorPropertyTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.StrokeColor), "Lijnkleur"),
                new DoubleUpDownPropertyTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.StrokeThickness), "Lijndikte", 0.0, 40.0, 0.5, "0.##"),
                new LineStylePropertyTreeNodeViewModel<StakeholderConnectionGroup>(connectionGroup,
                    nameof(StakeholderConnectionGroup.LineStyle), "Lijnstijl")
            };

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
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

        public bool IsQuickSelection => true;

        public bool IsSelected
        {
            get => connectionGroup.Visible;
            set
            {
                connectionGroup.Visible = value;
                connectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Visible));
            }
        }

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

    public interface IQuickSelectionViewModel
    {
        bool IsQuickSelection { get; }

        bool IsSelected { get; set; }
    }
}