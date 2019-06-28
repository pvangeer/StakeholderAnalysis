using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class ColorPropertyTreeNodeViewModel : PropertyTreeNodeViewModelBaseBase, IColorPropertyTreeNodeViewModel
    {
        private readonly PropertyInfo propertyInfo;
        private readonly INotifyPropertyChangedImplementation content;

        public ColorPropertyTreeNodeViewModel(INotifyPropertyChangedImplementation content, string propertyName, string displayName) : base(displayName)
        {
            this.content = content;

            propertyInfo = this.content.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(Color))
            {
                throw new ArgumentException();
            }

            if (this.content != null)
            {
                this.content.PropertyChanged += ContentPropertyChanged;
            }
        }

        public Color ColorValue
        {
            get => (Color)propertyInfo.GetValue(content);
            set
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(content, value, null);
                    content.OnPropertyChanged(propertyInfo.Name);
                }
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, content);
        }

        private void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyInfo.Name)
            {
                OnPropertyChanged(nameof(ColorValue));
            }
        }
    }
}
