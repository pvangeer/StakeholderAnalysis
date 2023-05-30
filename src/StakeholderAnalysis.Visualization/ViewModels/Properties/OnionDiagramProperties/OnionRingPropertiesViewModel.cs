using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties
{
    public class OnionRingPropertiesViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly OnionDiagram diagram;
        private readonly OnionRing ring;
        private bool isExpanded;

        public OnionRingPropertiesViewModel(ViewModelFactory factory, OnionRing ring, OnionDiagram diagram) :
            base(factory)
        {
            this.diagram = diagram;
            this.ring = ring;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new DoubleUpDownPropertyValueTreeNodeViewModel<OnionRing>(ring, nameof(OnionRing.Percentage), "Grootte", 0.0,
                    1.0, 0.01, "0.###"),
                new ColorPropertyValueTreeNodeViewModel<OnionRing>(ring, nameof(OnionRing.BackgroundColor),
                    "Achtergrondkleur"),
                new ColorPropertyValueTreeNodeViewModel<OnionRing>(ring, nameof(OnionRing.StrokeColor), "Lijnkleur"),
                new DoubleUpDownPropertyValueTreeNodeViewModel<OnionRing>(ring, nameof(OnionRing.StrokeThickness),
                    "Lijndikte", 0.0, 40.0, 0.5, "0.##"),
                new LineStylePropertyValueTreeNodeViewModel<OnionRing>(ring, nameof(OnionRing.LineStyle), "Lijnstijl")
            };
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public string IconSourceString { get; }

        public bool CanRemove => true;

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p => { diagram.OnionRings.Remove(ring); });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => false;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject() => null;

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
        {
            return o as OnionRing == ring;
        }

        public string DisplayName => ring.Percentage.ToString("0.####");

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

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;
    }
}