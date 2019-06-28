using System.ComponentModel;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public abstract class StringPropertyTreeNodeViewModelBase<T> : PropertyTreeNodeViewModelBaseBase, IStringPropertyTreeNodeViewModelBase where T : INotifyPropertyChanged
    {
        protected StringPropertyTreeNodeViewModelBase(T viewModel, string displayName) 
            : base(displayName)
        {
            Content = viewModel;

            if (Content != null)
            {
                Content.PropertyChanged += ViewModelPropertyChanged;
            }
        }

        public abstract string StringValue { get; set; }

        protected virtual void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) { }

        protected T Content { get; }
    }
}
