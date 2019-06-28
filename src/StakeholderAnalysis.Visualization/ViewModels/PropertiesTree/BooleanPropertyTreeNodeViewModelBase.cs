using System.ComponentModel;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public abstract class BooleanPropertyTreeNodeViewModelBase<T> : PropertyTreeNodeViewModelBaseBase, IBooleanPropertyTreeNodeViewModel where T : INotifyPropertyChanged
    {
        protected BooleanPropertyTreeNodeViewModelBase(T content, string displayName) : base(displayName)
        {
            this.Content = content;
        }

        protected T Content { get; }

        public abstract bool BooleanValue { get; set; }
    }
}
