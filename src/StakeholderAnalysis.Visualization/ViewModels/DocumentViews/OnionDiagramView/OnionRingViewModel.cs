using System.ComponentModel;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class OnionRingViewModel : ViewModelBase
    {
        public OnionRingViewModel(ViewModelFactory factory, OnionRing ring) : base(factory)
        {
            Ring = ring;
            ring.PropertyChanged += RingPropertyChanged;
        }

        public OnionRing Ring { get; }

        public Brush BackgroundColor => new SolidColorBrush(Ring.BackgroundColor);

        public Brush StrokeColor => new SolidColorBrush(Ring.StrokeColor);

        public double StrokeThickness => Ring.StrokeThickness;

        public LineStyle LineStyle => Ring.LineStyle;

        public double Percentage
        {
            get => Ring.Percentage;
            set => Ring.Percentage = value;
        }

        private void RingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionRing.BackgroundColor):
                    OnPropertyChanged(nameof(BackgroundColor));
                    break;
                case nameof(OnionRing.StrokeColor):
                    OnPropertyChanged(nameof(StrokeColor));
                    break;
                case nameof(OnionRing.StrokeThickness):
                    OnPropertyChanged(nameof(StrokeThickness));
                    break;
                case nameof(OnionRing.LineStyle):
                    OnPropertyChanged(nameof(LineStyle));
                    break;
                case nameof(OnionRing.Percentage):
                    OnPropertyChanged(nameof(Percentage));
                    break;
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}