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
        private readonly Gui.Gui gui;
        private readonly Analysis analysis;
        private RelayCommand saveCanvasCommand;

        public RibbonViewModel(Analysis analysisInput, Gui.Gui guiInput)
        {
            gui = guiInput;
            analysis = analysisInput;
            // TODO: Move this to separate viewmodel
            gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
            gui.ViewManager.ToolWindows.CollectionChanged += ToolwindowsCollectionChanged;
            gui.PropertyChanged += GuiPropertyChanged;
        }

        // TODO: Move this to separate viewmodel
        private void ToolwindowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsProjectDataToolWindowActive));
        }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(gui.ViewManager);

        // TODO: Move this to separate viewmodel
        public ViewInfo ActiveDocument
        {
            get => gui.ViewManager.ActiveDocument;
            set => gui.ViewManager.ActiveDocument = value;
        }

        public bool IsMagnifierActive
        {
            get => gui.IsMagnifierActive;
            set
            {
                gui.IsMagnifierActive = value;
                gui.OnPropertyChanged(nameof(Gui.Gui.IsMagnifierActive));
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                return saveCanvasCommand ?? (saveCanvasCommand = new RelayCommand(() =>
                {
                    gui.IsSaveToImage = true;
                    gui.OnPropertyChanged(nameof(Gui.Gui.IsSaveToImage));
                    gui.IsSaveToImage = false;
                    gui.OnPropertyChanged(nameof(Gui.Gui.IsSaveToImage));
                }, () => ActiveDocument != null));
            }
        }

        public RibbonSelectedOnionDiagramViewModel RibbonSelectedOnionDiagramViewModel =>
            gui?.ViewManager?.ActiveDocument?.ViewModel is OnionDiagramViewModel viewModel
                ? new RibbonSelectedOnionDiagramViewModel(viewModel.GetDiagram())
                : null;

        public bool IsProjectDataToolWindowActive
        {
            get => gui.ViewManager.ToolWindows.Any(i => i.ViewModel is ProjectExplorerViewModel);
            set {}
        }

        public ICommand ToggleToolWindowCommand => new ToggleProjectExplorerCommand(analysis, gui.ViewManager);

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    OnPropertyChanged(nameof(ActiveDocument));
                    OnPropertyChanged(nameof(RibbonSelectedOnionDiagramViewModel));
                    break;
            }
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Gui.Gui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
            }
        }
    }
}
