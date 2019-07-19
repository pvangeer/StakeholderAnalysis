using System;
using System.ComponentModel;
using System.Reflection;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class StringPropertyTreeNodeViewModel<TContent> : PropertyTreeNodeViewModelBaseBase, IStringPropertyTreeNodeViewModel where TContent : INotifyPropertyChangedImplementation
    {
        private readonly PropertyInfo propertyInfo;
        private INotifyPropertyChangedImplementation content;

        public StringPropertyTreeNodeViewModel(TContent content, string propertyName, string displayName) 
            : base(displayName)
        {
            this.Content = content;

            propertyInfo = typeof(TContent).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                throw new ArgumentException();
            }

            if (this.Content != null)
            {
                this.Content.PropertyChanged += ContentPropertyChanged;
            }
        }

        public INotifyPropertyChangedImplementation Content
        {
            get => content;
            set
            {
                if (this.Content != null)
                {
                    this.Content.PropertyChanged -= ContentPropertyChanged;
                }
                content = value;
                if (this.Content != null)
                {
                    this.Content.PropertyChanged += ContentPropertyChanged;
                }
                OnPropertyChanged(nameof(StringValue));
            }
        }

        public string StringValue
        {
            get => Content == null ? "" : (string)propertyInfo.GetValue(Content);
            set
            {
                if (propertyInfo.CanWrite && Content != null)
                {
                    propertyInfo.SetValue(Content, value, null);
                    Content.OnPropertyChanged(propertyInfo.Name);
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
            return ReferenceEquals(o, Content);
        }
    }
}
