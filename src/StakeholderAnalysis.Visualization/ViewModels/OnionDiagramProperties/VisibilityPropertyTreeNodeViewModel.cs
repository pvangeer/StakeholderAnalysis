using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class VisibilityPropertyTreeNodeViewModel : BooleanPropertyTreeNodeViewModelBase<IVisibilityProperty>
    {
        public VisibilityPropertyTreeNodeViewModel(IVisibilityProperty ring) : base(ring, "Weergeven")
        {
            Content.PropertyChanged += ContentPropertyChanged;
        }

        private void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IVisibilityProperty.Visible))
            {
                OnPropertyChanged(nameof(BooleanValue));
            }
        }

        public override bool BooleanValue
        {
            get => Content.Visible;
            set
            {
                Content.Visible = value;
                Content.OnPropertyChanged(nameof(IVisibilityProperty.Visible));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o,Content);
        }
    }
}