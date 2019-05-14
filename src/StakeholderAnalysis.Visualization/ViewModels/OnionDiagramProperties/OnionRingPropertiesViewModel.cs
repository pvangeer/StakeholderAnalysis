using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class OnionRingPropertiesViewModel : NotifyPropertyChangedObservable, IExpandableContentViewModel
    {
        private readonly OnionRing ring;
        private readonly OnionDiagram diagram;
        private bool isExpanded;

        public OnionRingPropertiesViewModel(OnionRing ring, OnionDiagram diagram)
        {
            this.diagram = diagram;
            this.ring = ring;
        }

        public Color StrokeColor
        {
            get => ring.StrokeColor;
            set
            {
                ring.StrokeColor = value;
                ring.OnPropertyChanged(nameof(OnionRing.StrokeColor));
            }
        }

        public Color BackgroundColor
        {
            get => ring.BackgroundColor;
            set
            {
                ring.BackgroundColor = value;
                ring.OnPropertyChanged(nameof(OnionRing.BackgroundColor));
            }
        }

        public double StrokeThickness
        {
            get => ring.StrokeThickness;
            set
            {
                ring.StrokeThickness = value;
                ring.OnPropertyChanged(nameof(OnionRing.StrokeThickness));
            }
        }

        public double Percentage
        {
            get => ring.Percentage;
            set
            {
                ring.Percentage = value;
                ring.OnPropertyChanged(nameof(OnionRing.Percentage));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public ICommand RemoveRingCommand => new RemoveOnionRingCommand(this);

        public ICommand ToggleIsExpandedCommand => new ToggleIsExpandedCommand(this);

        public bool IsViewModelFor(OnionRing ringViewModel)
        {
            return ringViewModel == ring;
        }

        public void RemoveOnionRing()
        {
            diagram.OnionRings.Remove(ring);
        }

        public string DisplayName => Percentage.ToString("0.####");

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }
    }
}
