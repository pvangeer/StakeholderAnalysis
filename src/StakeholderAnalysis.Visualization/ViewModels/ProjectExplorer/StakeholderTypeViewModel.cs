using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Converters;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class StakeholderTypeViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly StakeholderType stakeholderType;
        private bool isExpanded;

        public StakeholderTypeViewModel(ViewModelFactory factory, StakeholderType stakeholderType) : base(factory)
        {
            this.stakeholderType = stakeholderType;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new StringPropertyValueTreeNodeViewModel<StakeholderType>(stakeholderType, nameof(StakeholderType.Name),
                    "Naam"),
                new ColorPropertyValueTreeNodeViewModel<StakeholderType>(stakeholderType, nameof(StakeholderType.Color),
                    "Kleur"),
                new StakeholderTypeIconPropertyValueTreeNodeViewModel(stakeholderType)
            };
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
            RemoveItemCommand = CommandFactory.CreateRemoveStakeholderTypeCommand(stakeholderType);
            SelectItem = CommandFactory.CreateSelectItemCommand(this);
            stakeholderType.PropertyChanged += StakeholderTypePropertyChanged;
        }

        public StakeholderIconType IconType
        {
            get => stakeholderType.IconType;
            set
            {
                stakeholderType.IconType = value;
                stakeholderType.OnPropertyChanged();
            }
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

        public string IconSourceString =>
            (string)new IconTypeToIconSourceConverter().Convert(stakeholderType.IconType, typeof(string), null, null);

        public bool CanRemove => true;

        public ICommand RemoveItemCommand { get; }

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItem { get; }

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, stakeholderType);
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

        public bool IsViewModelFor(StakeholderType otherStakeholderType)
        {
            return stakeholderType == otherStakeholderType;
        }
    }
}