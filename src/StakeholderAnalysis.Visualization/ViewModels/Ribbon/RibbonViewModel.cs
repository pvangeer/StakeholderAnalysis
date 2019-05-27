using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonViewModel : NotifyPropertyChangedObservable
    {
        private readonly StakeholderAnalysisGui gui;
        private RelayCommand saveCanvasCommand;
        private readonly Action invalidateVisualAction;

        public RibbonViewModel(StakeholderAnalysisGui guiInput, Action invalidateVisualAction)
        {
            this.invalidateVisualAction = invalidateVisualAction;
            gui = guiInput;
            GuiProjectSercices = new GuiProjectServices(gui);
            RibbonStakeholderConnectionGroupsViewModel = new RibbonStakeholderConnectionGroupsViewModel(gui.ViewManager);
            if (gui != null)
            {
                gui.ShouldSaveOpenChanges = ShouldSaveOpenChanges;
                gui.PropertyChanged += GuiPropertyChanged;
            }

            ViewManagerViewModel = new ViewManagerViewModel(gui.ViewManager);
            gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
            gui.ViewManager.ToolWindows.CollectionChanged += ToolWindowsCollectionChanged;
            gui.PropertyChanged += GuiPropertyChanged;
        }

        public ViewManagerViewModel ViewManagerViewModel { get; }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand(gui, GuiProjectSercices);

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel { get; }

        public bool HasGui => gui != null;

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
                ? new RibbonSelectedOnionDiagramViewModel(viewModel.GetDiagram(), gui.Analysis)
                : null;

        public ICommand ToggleToolWindowCommand => new ToggleToolWindowCommand(gui, gui.ViewManager);

        public ObservableCollection<ViewInfo> ToolWindows
        {
            get => ViewManagerViewModel.ToolWindows;
            set { }
        }

        private void ToolWindowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ToolWindows));
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
                case nameof(StakeholderAnalysisGui.BusyIndicator):
                    invalidateVisualAction?.Invoke();
                    break;
                case nameof(StakeholderAnalysisGui.Analysis):
                    OnPropertyChanged(nameof(RibbonSelectedOnionDiagramViewModel));
                    OnPropertyChanged(nameof(ToggleToolWindowCommand));
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

        public GuiProjectServices GuiProjectSercices { get; }

        private bool ShouldSaveOpenChanges()
        {
            string messageBoxText = "U heeft aanpassingen aan uw project nog niet opgeslagen. Wilt u dat alsnog doen?";
            string caption = "Aanpassingen opslaan";

            MessageBoxButton messageBoxType = MessageBoxButton.YesNo;
            MessageBoxImage messageBoxImage = MessageBoxImage.Question;

            MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, caption, messageBoxType, messageBoxImage);

            return messageBoxResult == MessageBoxResult.Yes;
        }
    }
}
