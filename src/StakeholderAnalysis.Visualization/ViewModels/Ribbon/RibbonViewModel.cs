﻿using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

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

            ViewManagerViewModel = new ViewManagerViewModel(gui.ViewManager);
            gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
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
                }, () => gui.ViewManager.ActiveDocument != null));
            }
        }

        public RibbonSelectedOnionDiagramViewModel RibbonSelectedOnionDiagramViewModel =>
            gui?.ViewManager?.ActiveDocument?.ViewModel is OnionDiagramViewModel viewModel
                ? new RibbonSelectedOnionDiagramViewModel(viewModel.GetDiagram())
                : null;

        public ICommand ShowToolWindowCommand => new ShowProjectExplorerCommand(analysis, gui.ViewManager);

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Gui.Gui.IsMagnifierActive):
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
