using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class TwoAxisDiagramTextsViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly ITwoAxisDiagram twoAxisDiagram;
        private bool isExpanded;

        public TwoAxisDiagramTextsViewModel(ViewModelFactory factory, ITwoAxisDiagram twoAxisDiagram) : base(factory)
        {
            this.twoAxisDiagram = twoAxisDiagram;
            Items = twoAxisDiagram != null
                ? new ObservableCollection<ITreeNodeViewModel>
                {
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextLeftBottom), "Linksonder"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextLeftTop), "Linksboven"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextRightBottom), "Rechtsonder"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.BackgroundTextRightTop), "Rechtsboven"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.XAxisMaxLabel), "Horizontaal +"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.XAxisMinLabel), "Horizontaal -"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.YAxisMaxLabel), "Vertikaal +"),
                    new StringPropertyTreeNodeViewModel(twoAxisDiagram, nameof(ITwoAxisDiagram.YAxisMinLabel), "Vertikaal -"),
                    // TODO: Add font characteristics
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

        public string DisplayName => "Teksten";

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
