﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
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
            if (gui != null)
            {
                gui.ShouldSaveOpenChanges = ShouldSaveOpenChanges;
                gui.PropertyChanged += GuiPropertyChanged;
                gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
                gui.ViewManager.ToolWindows.CollectionChanged += ToolWindowsCollectionChanged;
                gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
                SetCurrentSelectedDiagramAndGroups();
            }

            ViewManagerViewModel = ViewModelFactory.CreateViewManagerViewModel();
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

            OnPropertyChanged(nameof(Asymmetry));
            OnPropertyChanged(nameof(AddOnionRingCommand));
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

        public ICommand OpenCommand => CommandFactory.CreateOpenFileCommand();

        public ICommand SaveCommand => CommandFactory.CreateSaveFileCommand();

        public ICommand SaveAsCommand => CommandFactory.CreateSaveFileAsCommand();

        public ICommand NewCommand => CommandFactory.CreateNewProjectCommand();

        public ICommand CloseApplication => CommandFactory.CreateCloseApplicationCommand();

        public ICommand AddStakeholdersCommand => CommandFactory.CreateAddStakeholdersCommand();

        public bool IsMagnifierActive
        {
            get => gui.IsMagnifierActive;
            set
            {
                gui.IsMagnifierActive = value;
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsMagnifierActive));
            }
        }

        public ICommand SaveImageCommand => saveCanvasCommand ?? (saveCanvasCommand = CommandFactory.CreateSaveImageCommand());

        public ICommand AddOnionRingCommand => CommandFactory.CreateAddOnionRingCommand(stakeholderConnectionGroupSelection?.OnionDiagram);

        public double Asymmetry
        {
            get => stakeholderConnectionGroupSelection?.OnionDiagram?.Asymmetry ?? 0.0;
            set
            {
                if (stakeholderConnectionGroupSelection?.OnionDiagram != null)
                {
                    stakeholderConnectionGroupSelection.OnionDiagram.Asymmetry = value;
                    stakeholderConnectionGroupSelection.OnionDiagram.OnPropertyChanged(nameof(OnionDiagram.Asymmetry));
                }
            }
        }


        public ICommand ToggleToolWindowCommand => CommandFactory.CreateToggleToolWindowCommand();

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
                    OnPropertyChanged(nameof(AddStakeholdersCommand));
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
            MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            return messageBoxResult == MessageBoxResult.Yes;
        }
    }
}
