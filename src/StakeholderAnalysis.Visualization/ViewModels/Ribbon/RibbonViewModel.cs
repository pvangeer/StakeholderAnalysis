using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.Commands.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;
        private RelayCommand saveCanvasCommand;
        private StakeholderConnectionGroupSelection stakeholderConnectionGroupSelection;

        public RibbonViewModel(ViewModelFactory factory, StakeholderAnalysisGui guiInput) : base(factory)
        {
            gui = guiInput;
            GuiProjectServices = new GuiProjectServices(gui);
            if (gui != null)
            {
                gui.ShouldSaveOpenChanges = ShouldSaveOpenChanges;
                gui.PropertyChanged += GuiPropertyChanged;
                gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
                gui.ViewManager.ToolWindows.CollectionChanged += ToolWindowsCollectionChanged;
                gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
                SetCurrentSelectedDiagramAndGroups();
            }

            ViewManagerViewModel = new ViewManagerViewModel(gui?.ViewManager);
        }

        private void SetCurrentSelectedDiagramAndGroups()
        {
            var selectedOnionDiagram = gui?.ViewManager?.ActiveDocument?.ViewModel is OnionDiagramViewModel viewModel
                ? viewModel.GetDiagram()
                : null;

            if (stakeholderConnectionGroupSelection != null)
            {
                stakeholderConnectionGroupSelection.PropertyChanged -= StakeholderConnectionGroupSelectionPropertyChanged;
            }

            stakeholderConnectionGroupSelection = null;
            OnPropertyChanged(nameof(StakeholderConnectionGroups));
            OnPropertyChanged(nameof(SelectedStakeholderConnectionGroup));
            
            stakeholderConnectionGroupSelection =
                gui?.SelectedStakeholderConnectionGroups.FirstOrDefault(g => g.OnionDiagram == selectedOnionDiagram);

            if (stakeholderConnectionGroupSelection != null)
            {
                stakeholderConnectionGroupSelection.PropertyChanged += StakeholderConnectionGroupSelectionPropertyChanged;
            }

            OnPropertyChanged(nameof(RibbonSelectedOnionDiagramViewModel));
            OnPropertyChanged(nameof(StakeholderConnectionGroups));
            OnPropertyChanged(nameof(SelectedStakeholderConnectionGroup));
            OnPropertyChanged(nameof(ToggleToolWindowCommand));
        }

        private void StakeholderConnectionGroupSelectionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StakeholderConnectionGroupSelection.StakeholderConnectionGroup))
            {
                OnPropertyChanged(nameof(SelectedStakeholderConnectionGroup));
            }
        }

        public ViewManagerViewModel ViewManagerViewModel { get; }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand(gui, GuiProjectServices);

        public ICommand AddStakeholdersCommand => new AddStakeholdersToDiagramCommand(gui.ViewManager, gui.Analysis);

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
            stakeholderConnectionGroupSelection?.OnionDiagram != null ? new RibbonSelectedOnionDiagramViewModel(stakeholderConnectionGroupSelection.OnionDiagram, gui.Analysis) : null;

        public ICommand ToggleToolWindowCommand => new ToggleToolWindowCommand(ViewModelFactory, gui, gui.ViewManager);

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
                case nameof(StakeholderAnalysisGui.Analysis):
                    SetCurrentSelectedDiagramAndGroups();
                    break;
            }
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(gui.ViewManager.ActiveDocument):
                    SetCurrentSelectedDiagramAndGroups();
                    break;
            }
        }

        public GuiProjectServices GuiProjectServices { get; }

        public ObservableCollection<StakeholderConnectionGroup> StakeholderConnectionGroups => stakeholderConnectionGroupSelection?.OnionDiagram?.ConnectionGroups;

        public StakeholderConnectionGroup SelectedStakeholderConnectionGroup
        {
            get => stakeholderConnectionGroupSelection?.StakeholderConnectionGroup;
            set 
            {
                if (stakeholderConnectionGroupSelection == null || value == null)
                {
                    return;
                }

                stakeholderConnectionGroupSelection.StakeholderConnectionGroup = value;
                stakeholderConnectionGroupSelection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection.StakeholderConnectionGroup));
            }
        }

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
