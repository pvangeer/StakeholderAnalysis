using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class StrokeColorPropertyTreeNodeViewModel : ColorPropertyTreeNodeViewModelBase<IStrokeProperty>
    {
        public StrokeColorPropertyTreeNodeViewModel(IStrokeProperty ring) : base(ring, "Lijnkleur") { }

        public override Color ColorValue
        {
            get => Content.StrokeColor;
            set
            {
                Content.StrokeColor = value;
                Content.OnPropertyChanged(nameof(IStrokeProperty.StrokeColor));
            }
        }
    }
}