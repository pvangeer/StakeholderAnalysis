using System.ComponentModel;
using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public interface IStrokeProperty : INotifyPropertyChangedImplementation
    {
        double StrokeThickness { get; set; }

        Color StrokeColor { get; set; }
    }
}