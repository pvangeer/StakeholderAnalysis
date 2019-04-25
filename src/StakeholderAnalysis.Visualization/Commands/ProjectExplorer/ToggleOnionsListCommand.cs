﻿using System;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.Commands.ProjectExplorer
{
    public class ToggleOnionsListCommand : ICommand
    {
        private readonly ProjectExplorerOnionDiagramsViewModel projectExplorerOnionDiagramsViewModel;

        public ToggleOnionsListCommand(ProjectExplorerOnionDiagramsViewModel projectExplorerOnionDiagramsViewModel)
        {
            this.projectExplorerOnionDiagramsViewModel = projectExplorerOnionDiagramsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            projectExplorerOnionDiagramsViewModel.IsExpanded =
                !projectExplorerOnionDiagramsViewModel.IsExpanded;
        }

        public event EventHandler CanExecuteChanged;
    }
}
