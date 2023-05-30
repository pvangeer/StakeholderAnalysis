using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public abstract class ViewModelBase : NotifyPropertyChangedObservable, IViewModel
    {
        protected ViewModelBase(ViewModelFactory factory)
        {
            ViewModelFactory = factory;
            CommandFactory = ViewModelFactory?.GetCommandFactory();
        }

        protected ViewModelFactory ViewModelFactory { get; }

        protected CommandFactory CommandFactory { get; }

        public abstract bool IsViewModelFor(object o);
    }
}