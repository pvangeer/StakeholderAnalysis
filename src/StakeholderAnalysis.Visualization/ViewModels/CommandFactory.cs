using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.Commands.Ribbon;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class CommandFactory
    {
        private readonly Analysis analysis;
        private readonly StakeholderAnalysisGui gui;
        private readonly ViewModelFactory viewModelFactory;

        public CommandFactory(StakeholderAnalysisGui gui, Analysis analysis, ViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            this.analysis = analysis;
            this.gui = gui;
        }

        public ICommand CreateRemoveStakeholderTypeCommand(StakeholderType stakeholderType)
        {
            return new RemoveStakeholderTypeCommand(stakeholderType, analysis);
        }

        public ICommand CreateCloseApplicationCommand()
        {
            return new CloseApplicationCommand(gui, gui?.GuiProjectServices);
        }

        public ICommand CreateNewProjectCommand()
        {
            return new NewProjectCommand(gui?.GuiProjectServices);
        }

        public ICommand CeateOpenFileCommand()
        {
            return new OpenFileCommand(gui?.GuiProjectServices);
        }

        public ICommand CreateSaveFileCommand()
        {
            return new SaveFileCommand(gui?.GuiProjectServices);
        }

        public ICommand CreateSaveFileAsCommand()
        {
            return new SaveFileAsCommand(gui?.GuiProjectServices);
        }

        public ICommand CreateAddStakeholdersCommand()
        {
            return new AddStakeholdersToDiagramCommand(gui.ViewManager, gui.Analysis);
        }

        public RelayCommand CreateSaveImageCommand()
        {
            return new RelayCommand(() =>
            {
                gui.IsSaveToImage = true;
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
                gui.IsSaveToImage = false;
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
            }, 
                () => gui.ViewManager.ActiveDocument != null);
        }

        public ICommand CreateToggleToolWindowCommand()
        {
            return new ToggleToolWindowCommand(viewModelFactory, gui, gui.ViewManager);
        }

        public ICommand CreateAddOnionRingCommand(OnionDiagram onionDiagram)
        {
            return new AddOnionRingCommand(onionDiagram);
        }
    }
}
