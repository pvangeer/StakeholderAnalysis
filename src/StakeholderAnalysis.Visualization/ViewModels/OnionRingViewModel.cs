using System.ComponentModel;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class OnionRingViewModel : PropertyChangedElement
    {
        public OnionRingViewModel() : this(new OnionRing())
        {
        }

        public OnionRingViewModel(OnionRing ring)
        {
            Ring = ring;
            ring.PropertyChanged += RingPropertyChanged;
        }

        public OnionRing Ring { get; }

        public string Name => Ring.Name;

        public Brush BackgroundColor => new SolidColorBrush(Ring.BackgroundColor);

        public Brush StrokeColor => new SolidColorBrush(Ring.StrokeColor);

        public double StrokeThickness => Ring.StrokeThickness;

        public double Percentage
        {
            get => Ring.Percentage;
            set => Ring.Percentage = value;
        }

        private void RingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionRing.Name):
                    OnPropertyChanged(Name);
                    break;
                case nameof(OnionRing.BackgroundColor):
                    OnPropertyChanged(nameof(BackgroundColor));
                    break;
                case nameof(OnionRing.StrokeColor):
                    OnPropertyChanged(nameof(StrokeColor));
                    break;
                case nameof(OnionRing.StrokeThickness):
                    OnPropertyChanged(nameof(StrokeThickness));
                    break;
                case nameof(OnionRing.Percentage):
                    OnPropertyChanged(nameof(Percentage));
                    break;
            }
        }
    }
}