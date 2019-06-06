﻿using System;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.Commands.Ribbon;
using StakeholderAnalysis.Visualization.Commands.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class CommandFactory
    {
        private Analysis Analysis => gui?.Analysis;
        private readonly StakeholderAnalysisGui gui;
        private readonly ViewModelFactory viewModelFactory;

        public CommandFactory(StakeholderAnalysisGui gui, ViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            this.gui = gui;
        }

        public ICommand CreateRemoveStakeholderTypeCommand(StakeholderType stakeholderType)
        {
            return new RemoveStakeholderTypeCommand(stakeholderType, Analysis);
        }

        public ICommand CreateCloseApplicationCommand()
        {
            return new CloseApplicationCommand(gui, gui?.GuiProjectServices);
        }

        public ICommand CreateNewProjectCommand()
        {
            return new NewProjectCommand(gui?.GuiProjectServices);
        }

        public ICommand CreateOpenFileCommand()
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
            return new AddStakeholdersToDiagramCommand(gui?.ViewManager, Analysis);
        }

        public RelayCommand CreateSaveImageCommand()
        {
            return new RelayCommand(() =>
            {
                if (gui != null)
                {
                    gui.IsSaveToImage = true;
                    gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
                    gui.IsSaveToImage = false;
                    gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsSaveToImage));
                }
            }, 
                () => gui?.ViewManager.ActiveDocument != null);
        }

        public ICommand CreateToggleToolWindowCommand()
        {
            return new ToggleToolWindowCommand(viewModelFactory, gui?.ViewManager);
        }

        public ICommand CreateAddOnionRingCommand(OnionDiagram onionDiagram)
        {
            return new AddOnionRingCommand(onionDiagram);
        }

        public ICommand CreateCanAlwaysExecuteActionCommand(Action<object> action)
        {
            return new CanAlwaysExecuteActionCommand
            {
                ExecuteAction = action
            };
        }

        public ICommand CreateRemoveAllMessagesCommand(MessageList messageList)
        {
            return new RemoveAllMessagesCommand(messageList);
        }

        public ICommand CreateAddConnectionGroupCommand(OnionDiagram selectedOnionDiagram)
        {
            return new AddConnectionGroupCommand(selectedOnionDiagram);
        }

        public ICommand CreateToggleIsExpandedCommand(IExpandableContentViewModel expandableContentViewModel)
        {
            return new ToggleIsExpandedCommand(expandableContentViewModel);
        }

        public ICommand CreateAddNewStakeholderTypeCommand(Analysis analysis)
        {
            return new AddNewStakeholderTypeCommand(analysis);
        }

        public ICommand CreateClearSelectionCommand(ISelectionRegister selectionRegister)
        {
            return new ClearSelectionCommand(selectionRegister);
        }

        public ICommand CreateRemoveSelectedStakeholderFromDiagramCommand(IRemoveStakeholderViewModel removeStakeholderViewModel)
        {
            return new RemoveSelectedStakeholderFromDiagramCommand(removeStakeholderViewModel);
        }

        public ICommand CreateMoveToBottomCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
        {
            return new MoveStakeholderToBottomCommand(diagram, onionDiagramStakeholder);
        }

        public ICommand CreateMoveUpCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
        {
            return new MoveStakeholderUpCommand(diagram, onionDiagramStakeholder);
        }

        public ICommand CreateMoveDownCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
        {
            return new MoveStakeholderDownCommand(diagram, onionDiagramStakeholder);
        }

        public ICommand CreateMoveToTopCommand(OnionDiagram diagram, OnionDiagramStakeholder onionDiagramStakeholder)
        {
            return new MoveStakeholderToTopCommand(diagram, onionDiagramStakeholder);
        }
    }
}
