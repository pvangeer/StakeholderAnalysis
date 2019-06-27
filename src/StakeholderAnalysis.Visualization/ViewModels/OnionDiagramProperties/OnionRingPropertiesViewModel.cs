using System.Collections.ObjectModel;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class OnionRingPropertiesViewModel : ViewModelBase, IPropertyCollectionTreeNodeViewModel
    {
        private readonly OnionRing ring;
        private readonly OnionDiagram diagram;
        private bool isExpanded;

        public OnionRingPropertiesViewModel(ViewModelFactory factory, OnionRing ring, OnionDiagram diagram) : base(factory)
        {
            this.diagram = diagram;
            this.ring = ring;
            Items = new ObservableCollection<ITreeNodeViewModel>
            {
                new PercentagePropertyTreeNodeViewModel(ring),
                new BackgroundColorPropertyTreeNodeViewModel(ring),
                new StrokeColorPropertyTreeNodeViewModel(ring),
                new StrokeThicknessPropertyTreeNodeViewModel(ring)
            };
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public string IconSourceString { get; }

        public bool CanRemove => true;

        public ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            diagram.OnionRings.Remove(ring);
        });

        public bool CanOpen => false;

        public ICommand OpenViewCommand => null;

        public bool IsViewModelFor(OnionRing ringViewModel)
        {
            return ringViewModel == ring;
        }

        public string DisplayName => ring.Percentage.ToString("0.####");

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

        public ObservableCollection<ITreeNodeViewModel> Items { get; }

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;
    }
}
