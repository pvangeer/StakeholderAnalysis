using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Converters;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class StakeholderTypeViewModel : ViewModelBase, IExpandableContentViewModel
    {
        private readonly StakeholderType stakeholderType;
        private bool isExpanded;

        public StakeholderTypeViewModel(ViewModelFactory factory, StakeholderType stakeholderType, ICommand removeStakeholderTypeCommand) : base(factory)
        {
            this.stakeholderType = stakeholderType;
            RemoveStakeholderTypeCommand = removeStakeholderTypeCommand;
            stakeholderType.PropertyChanged += StakeholderTypePropertyChanged;
        }

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

        public string IconSourceString => (string)new IconTypeToIconSourceConverter().Convert(stakeholderType.IconType, typeof(string), null, null);

        public StakeholderIconType IconType
        {
            get => stakeholderType.IconType;
            set
            {
                stakeholderType.IconType = value;
                stakeholderType.OnPropertyChanged(nameof(StakeholderType.IconType));
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

        public Color Color
        {
            get => stakeholderType.Color;
            set
            {
                stakeholderType.Color = value;
                stakeholderType.OnPropertyChanged(nameof(StakeholderType.Color));
            }
        }

        public ICommand RemoveStakeholderTypeCommand { get; }

        public bool IsViewModelFor(StakeholderType otherStakeholderType)
        {
            return stakeholderType == otherStakeholderType;
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);
    }
}
