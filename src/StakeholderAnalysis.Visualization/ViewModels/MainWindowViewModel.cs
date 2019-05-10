using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly Gui.Gui gui;

        public MainWindowViewModel() : this(new Analysis(), new Gui.Gui()){ }

        public MainWindowViewModel(Analysis analysisInput, Gui.Gui guiInput)
        {
            analysis = analysisInput;
            gui = guiInput;
            RibbonViewModel.ToggleToolWindowCommand.Execute(null);
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel => new MainContentPresenterViewModel(gui);

        public RibbonViewModel RibbonViewModel => new RibbonViewModel(analysis,gui);
    }
}