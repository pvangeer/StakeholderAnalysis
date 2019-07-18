using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagramProperties
{
    public class TwoAxisDiagramBushViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly ITwoAxisDiagram twoAxisDiagram;
        private bool isExpanded;

        public TwoAxisDiagramBushViewModel(ViewModelFactory viewModelFactory, ITwoAxisDiagram activeTwoAxisDiagram) : base(viewModelFactory)
        {
            this.twoAxisDiagram = activeTwoAxisDiagram;
            Items = twoAxisDiagram != null
                ? new ObservableCollection<ITreeNodeViewModel>
                {
                    new ColorPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.BrushStartColor), "Startkleur"),
                    new ColorPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.BrushEndColor), "Eindkleur"),
                }
                : new ObservableCollection<ITreeNodeViewModel>();
        }

        public bool IsExpandable => true;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public CollectionType CollectionType => CollectionType.PropertyValue;

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public string DisplayName => "Achtergrond";

        public string IconSourceString { get; }

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, twoAxisDiagram);
        }

        public ObservableCollection<ITreeNodeViewModel> Items { get; }
    }
}