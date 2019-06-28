using System.ComponentModel;
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

        protected override void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(INameProperty.Name):
                    OnPropertyChanged(nameof(StringValue));
                    break;
            }
            base.ContentPropertyChanged(sender, e);
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, Content);
        }
    }
}