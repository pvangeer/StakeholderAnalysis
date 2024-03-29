﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerStakeholderOverviewTableViewModel : ViewModelBase, ITreeNodeViewModel
    {
        private readonly ViewManager viewManager;

        public ProjectExplorerStakeholderOverviewTableViewModel(ViewModelFactory factory, ViewManager viewManager) :
            base(factory)
        {
            this.viewManager = viewManager;
            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>();
            SelectItemCommand = CommandFactory.CreateSelectItemCommand(this);
        }

        public string DisplayName => "Stakeholders";

        public string IconSourceString =>
            "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Fluent/table_24_regular.ico";

        public bool CanRemove => false;

        public ICommand RemoveItemCommand => null;

        public bool CanAdd => false;

        public ICommand AddItemCommand => null;

        public bool CanOpen => true;

        public ICommand OpenViewCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(o =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v => v.ViewModel is StakeholderTableViewModel);
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo("Stakeholders", ViewModelFactory.CreateStakeholderTableViewModel(),
                    "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/Fluent/table_24_regular.ico");
                viewManager.OpenView(viewInfo);
            }

            viewManager.BringToFront(viewInfo);
        });

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand { get; }

        public object GetSelectableObject()
        {
            return "StakeholderTable";
        }

        public ObservableCollection<ContextMenuItemViewModel> ContextMenuItems { get; }

        public override bool IsViewModelFor(object o)
        {
            return o is string s && s == "StakeholderTable";
        }

        public bool IsExpandable => false;

        public bool IsExpanded { get; set; }

        public ICommand ToggleIsExpandedCommand => null;
    }
}