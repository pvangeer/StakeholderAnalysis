using System.ComponentModel;
using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public abstract class ColorPropertyTreeNodeViewModelBase<T> : PropertyTreeNodeViewModelBaseBase, IColorPropertyTreeNodeViewModel where T : INotifyPropertyChanged
    {
        protected ColorPropertyTreeNodeViewModelBase(T Content, string displayName) : base(displayName)
        {
            this.Content = Content;
        }

        protected T Content { get; }

        public abstract Color ColorValue { get; set; }
    }
}
