using System.ComponentModel;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public abstract class DoubleUpDownPropertyTreeNodeViewModelBase<T> : PropertyTreeNodeViewModelBaseBase, IDoubleUpDownPropertyTreeNodeViewModelBase
        where T : INotifyPropertyChanged
    {
        protected DoubleUpDownPropertyTreeNodeViewModelBase(T viewModel, string displayName, double minValue, double maxValue, double increment, string stringFormat) 
            : base(displayName)
        {
            Content = viewModel;
            MinValue = minValue;
            MaxValue = maxValue;
            Increment = increment;
            StringFormat = stringFormat;

            if (Content != null)
            {
                Content.PropertyChanged += ViewModelPropertyChanged;
            }
        }

        public abstract double DoubleValue { get; set; }

        public double MinValue { get; }

        public double MaxValue { get; }

        public double Increment { get; }

        public string StringFormat { get; }

        protected virtual void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) { }

        protected T Content { get; }
    }
}
