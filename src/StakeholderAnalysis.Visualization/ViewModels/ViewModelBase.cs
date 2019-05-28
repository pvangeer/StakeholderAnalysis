using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ViewModelBase : NotifyPropertyChangedObservable
    {
        public ViewModelBase(ViewModelFactory factory)
        {
            ViewModelFactory = factory;
        }

        protected ViewModelFactory ViewModelFactory { get; }
    }
}
