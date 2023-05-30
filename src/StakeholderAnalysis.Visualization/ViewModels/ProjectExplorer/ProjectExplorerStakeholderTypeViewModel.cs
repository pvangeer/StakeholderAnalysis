using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Converters;
using StakeholderAnalysis.Visualization.ViewModels.Properties;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerStakeholderTypeViewModel : ViewModelBase, ITreeNodeCollectionViewModel
    {
        private readonly StakeholderType stakeholderType;

        public ProjectExplorerStakeholderTypeViewModel(ViewModelFactory factory, StakeholderType stakeholderType) : base(factory)
        {
            this.stakeholderType = stakeholderType;

            Items = new ObservableCollection<ITreeNodeViewModel>();

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
            RemoveItemCommand = CommandFactory.CreateRemoveStakeholderTypeCommand(stakeholderType);
            SelectItemCommand = CommandFactory.CreateSelectItemCommand(this);

            stakeholderType.PropertyChanged += StakeholderTypePropertyChanged;
        }

        public Color Color
        {
            get => stakeholderType.Color;
            set
            {
                stakeholderType.Color = value;
                stakeholderType.OnPropertyChanged();
            }
        }

        public string DisplayName
        {
            get => stakeholderType?.Name;
            set
            {
                stakeholderType.Name = value;
                stakeholderType.OnPropertyChanged(nameof(StakeholderType.Name));
            }
        }

        public bool CanRemove => true;

        public ICommand RemoveItemCommand { get; }

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand { get; }

        public object GetSelectableObject()
        {
            return stakeholderType;
        }

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, stakeholderType);
        }

        public string IconSourceString =>
            (string)new IconTypeToIconSourceConverter().Convert(stakeholderType.IconType, typeof(string), null, null);

        public bool IsExpandable => false;

        public bool IsExpanded { get; set; }

        public ICommand ToggleIsExpandedCommand => null;

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        private void StakeholderTypePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderType.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
                case nameof(StakeholderType.IconType):
                    OnPropertyChanged(nameof(IconSourceString));
                    break;
                case nameof(StakeholderType.Color):
                    OnPropertyChanged(nameof(Color));
                    break;
            }
        }

        public StakeholderTypePropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateStakeholderTypePropertiesViewModel(stakeholderType);
        }
    }
}