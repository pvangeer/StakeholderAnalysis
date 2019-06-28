using System.ComponentModel;
using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public abstract class ColorPropertyTreeNodeViewModelBase<T> : PropertyTreeNodeViewModelBaseBase, IColorPropertyTreeNodeViewModel where T : INotifyPropertyChanged
    {
        protected ColorPropertyTreeNodeViewModelBase(T content, string displayName) : base(displayName)
        {
            this.Content = content;
        }

        protected T Content { get; }

        public abstract Color ColorValue { get; set; }
    }
}
