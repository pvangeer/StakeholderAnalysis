using System;
using System.ComponentModel;
using System.Reflection;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class DoubleUpDownPropertyTreeNodeViewModel : PropertyTreeNodeViewModelBaseBase, IDoubleUpDownPropertyTreeNodeViewModel
    {
        private readonly PropertyInfo propertyInfo;
        private readonly INotifyPropertyChangedImplementation content;

        public DoubleUpDownPropertyTreeNodeViewModel(INotifyPropertyChangedImplementation content,string propertyName, string displayName, double minValue, double maxValue, double increment, string stringFormat) 
            : base(displayName)
        {
            this.content = content;
            propertyInfo = this.content.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(double))
            {
                throw new ArgumentException();
            }

            MinValue = minValue;
            MaxValue = maxValue;
            Increment = increment;
            StringFormat = stringFormat;

            if (this.content != null)
            {
                this.content.PropertyChanged += ContentPropertyChanged;
            }
        }

        public double DoubleValue
        {
            get => (double)propertyInfo.GetValue(content);
            set
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(content, value, null);
                    content.OnPropertyChanged(propertyInfo.Name);
                }
            }
        }

        public double MinValue { get; }

        public double MaxValue { get; }

        public double Increment { get; }

        public string StringFormat { get; }

        private void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyInfo.Name)
            {
                OnPropertyChanged(nameof(DoubleValue));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o,content);
        }
    }
}
