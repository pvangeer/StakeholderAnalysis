using System;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.Commands.BackStage;
using StakeholderAnalysis.Visualization.Commands.Diagrams;
using StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.Commands.ProjectExplorer;
using StakeholderAnalysis.Visualization.Commands.Ribbon;
using StakeholderAnalysis.Visualization.Commands.StatusBar;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class CommandFactory
    {
        private readonly StakeholderAnalysisGui gui;

        public CommandFactory(StakeholderAnalysisGui gui)
        {
            this.gui = gui;
        }

        private Analysis Analysis => gui?.Analysis;

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

        public ICommand CreateToggleIsExpandedCommand(IExpandable expandableContentViewModel)
        {
            return new ToggleIsExpandedCommand(expandableContentViewModel);
        }

        public ICommand CreateClearSelectionCommand(ISelectionRegister selectionRegister)
        {
            return new ClearSelectionCommand(selectionRegister);
        }

        public ICommand CreateRemoveSelectedStakeholderFromDiagramCommand(
            IRemoveStakeholderViewModel removeStakeholderViewModel)
        {
            return new RemoveSelectedStakeholderFromDiagramCommand(removeStakeholderViewModel);
        }

        public ICommand CreateMoveToBottomCommand<TStakeholder>(IRankedStakeholderDiagram<TStakeholder> diagram,
            IRankedStakeholder stakeholder) where TStakeholder : class, IRankedStakeholder
        {
            return new MoveStakeholderToBottomCommand<TStakeholder>(diagram, stakeholder);
        }

        public ICommand CreateMoveUpCommand<TStakeholder>(IRankedStakeholderDiagram<TStakeholder> diagram,
            IRankedStakeholder stakeholder) where TStakeholder : class, IRankedStakeholder
        {
            return new MoveStakeholderUpCommand<TStakeholder>(diagram, stakeholder);
        }

        public ICommand CreateMoveDownCommand<TStakeholder>(IRankedStakeholderDiagram<TStakeholder> diagram,
            IRankedStakeholder stakeholder) where TStakeholder : class, IRankedStakeholder
        {
            return new MoveStakeholderDownCommand<TStakeholder>(diagram, stakeholder);
        }

        public ICommand CreateMoveToTopCommand<TStakeholder>(IRankedStakeholderDiagram<TStakeholder> diagram,
            IRankedStakeholder onionDiagramStakeholder) where TStakeholder : class, IRankedStakeholder
        {
            return new MoveStakeholderToTopCommand<TStakeholder>(diagram, onionDiagramStakeholder);
        }

        public ICommand CreateSelectItemCommand(ISelectable selectable)
        {
            return new SelectItemCommand(gui.SelectionManager, selectable);
        }
    }
}