using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.PropertiesTree;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class NamePropertyTreeNodeViewModel : StringPropertyTreeNodeViewModelBase<INameProperty>
    {
        public NamePropertyTreeNodeViewModel(INameProperty nameProperty) : base(nameProperty, "Weergeven") { }

        public override string StringValue
        {
            get => Content.Name;
            set
            {
                Content.Name = value;
                Content.OnPropertyChanged(nameof(INameProperty.Name));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, Content);
        }
    }
}