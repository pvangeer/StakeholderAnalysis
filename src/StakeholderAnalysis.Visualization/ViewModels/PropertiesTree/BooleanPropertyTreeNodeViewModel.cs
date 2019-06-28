using System;
using System.ComponentModel;
using System.Reflection;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class BooleanPropertyTreeNodeViewModel : PropertyTreeNodeViewModelBaseBase, IBooleanPropertyTreeNodeViewModel
    {
        private readonly PropertyInfo propertyInfo;
        private readonly INotifyPropertyChangedImplementation content;

        public BooleanPropertyTreeNodeViewModel(INotifyPropertyChangedImplementation content, string propertyName, string displayName) : base(displayName)
        {
            this.content = content;
            propertyInfo = this.content.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(bool))
            {
                throw new ArgumentException();
            }

            if (this.content != null)
            {
                this.content.PropertyChanged += ContentPropertyChanged;
            }
        }

        public bool BooleanValue
        {
            get => (bool)propertyInfo.GetValue(content);
            set
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(content, value, null);
                    content.OnPropertyChanged(propertyInfo.Name);
                }
            }
        }

        private void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyInfo.Name)
            {
                OnPropertyChanged(nameof(BooleanValue));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, content);
        }
    }
}
