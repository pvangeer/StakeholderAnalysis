using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerStakeholderOverviewTableViewModel : NotifyPropertyChangedObservable 
    {
        private readonly ViewManager viewManager;
        private readonly Analysis analysis;

        public ProjectExplorerStakeholderOverviewTableViewModel(Analysis analysis, ViewManager viewManager)
        {
            this.analysis = analysis;
            this.viewManager = viewManager;
        }

        public ICommand OpenStakeholderTableViewCommand => new OpenStakeholderTableViewCommand(this);

        public void OpenDiagramInDocumentView()
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v => v.ViewModel is StakeholderTableViewModel);
            if (viewInfo == null)
            {
                viewInfo = new ViewInfo("Tabel", new StakeholderTableViewModel(analysis), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/table.png", true);
                viewManager.OpenView(viewInfo);
            }
            viewManager.BringToFront(viewInfo);
        }
    }

    public class OpenStakeholderTableViewCommand : ICommand
    {
        private readonly ProjectExplorerStakeholderOverviewTableViewModel viewModel;

        public OpenStakeholderTableViewCommand(ProjectExplorerStakeholderOverviewTableViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.OpenDiagramInDocumentView();
        }

        public event EventHandler CanExecuteChanged;
    }
}
