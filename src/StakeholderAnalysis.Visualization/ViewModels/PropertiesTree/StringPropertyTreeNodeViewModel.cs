using System;
using System.ComponentModel;
using System.Reflection;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class StringPropertyTreeNodeViewModel : PropertyTreeNodeViewModelBaseBase, IStringPropertyTreeNodeViewModel
    {
        private readonly PropertyInfo propertyInfo;
        private readonly INotifyPropertyChangedImplementation content;

        public StringPropertyTreeNodeViewModel(INotifyPropertyChangedImplementation content, string propertyName, string displayName) 
            : base(displayName)
        {
            this.content = content;

            propertyInfo = this.content.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                throw new ArgumentException();
            }

            if (this.content != null)
            {
                this.content.PropertyChanged += ContentPropertyChanged;
            }
        }

        public string StringValue
        {
            get => (string)propertyInfo.GetValue(content);
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
                OnPropertyChanged(nameof(StringValue));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, content);
        }
    }
}
