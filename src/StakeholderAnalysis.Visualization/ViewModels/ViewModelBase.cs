using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public abstract class ViewModelBase : NotifyPropertyChangedObservable
    {
        protected ViewModelBase(ViewModelFactory factory)
        {
            ViewModelFactory = factory;
            CommandFactory = ViewModelFactory?.GetCommandFactory();
        }

        protected ViewModelFactory ViewModelFactory { get; }

        protected CommandFactory CommandFactory { get; }
    }
}
