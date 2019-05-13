using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonViewModel : NotifyPropertyChangedObservable
    {
        private readonly StakeholderAnalysisGui gui;
        private readonly Analysis analysis;
        private RelayCommand saveCanvasCommand;

        public RibbonViewModel(Analysis analysisInput, StakeholderAnalysisGui guiInput)
        {
            gui = guiInput;
            analysis = analysisInput;

            ViewManagerViewModel = new ViewManagerViewModel(gui.ViewManager);
            gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
            gui.ViewManager.ToolWindows.CollectionChanged += ToolwindowsCollectionChanged;
            gui.PropertyChanged += GuiPropertyChanged;
        }

        public ViewManagerViewModel ViewManagerViewModel { get; }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(gui.ViewManager);


        public bool IsMagnifierActive
        {
            get => gui.IsMagnifierActive;
            set
            {
                gui.IsMagnifierActive = value;
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsMagnifierActive));
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                return saveCanvasCommand ?? (saveCanvasCommand = new RelayCommand(() =>
                {
                    gui.IsSaveToImage = true;
                    gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
                    gui.IsSaveToImage = false;
                    gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
                }, () => gui.ViewManager.ActiveDocument != null));
            }
        }

        public RibbonSelectedOnionDiagramViewModel RibbonSelectedOnionDiagramViewModel =>
            gui?.ViewManager?.ActiveDocument?.ViewModel is OnionDiagramViewModel viewModel
                ? new RibbonSelectedOnionDiagramViewModel(viewModel.GetDiagram(), analysis)
                : null;

        public ICommand ToggleToolWindowCommand => new ToggleProjectExplorerCommand(analysis, gui.ViewManager);

        private void ToolwindowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsProjectExplorerToolwindowActive));
        }

        public bool IsProjectExplorerToolwindowActive
        {
            get => gui.ViewManager.ToolWindows.Any(vi => vi.ViewModel is ProjectExplorerViewModel);
            set { }
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
            }
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(gui.ViewManager.ActiveDocument):
                    OnPropertyChanged(nameof(RibbonSelectedOnionDiagramViewModel));
                    break;
            }
        }
    }
}
