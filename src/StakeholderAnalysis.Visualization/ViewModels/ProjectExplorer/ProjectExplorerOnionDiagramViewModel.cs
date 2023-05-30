using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.Properties.OnionDiagramProperties;
using StakeholderAnalysis.Visualization.ViewModels.TreeView;

namespace StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer
{
    public class ProjectExplorerOnionDiagramViewModel : ProjectExplorerItemViewModelBase
    {
        private readonly Analysis analysis;
        private readonly OnionDiagram diagram;
        private readonly ViewManager viewManager;

        public ProjectExplorerOnionDiagramViewModel(ViewModelFactory factory, Analysis analysis,
            OnionDiagram onionDiagram, ViewManager viewManager) : base(factory)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = onionDiagram;

            ContextMenuItems = new ObservableCollection<ContextMenuItemViewModel>
            {
                ViewModelFactory.CreateDuplicateMenuItemViewModel(diagram,
                    CommandFactory.CreateCanAlwaysExecuteActionCommand(
                        p => { analysis.OnionDiagrams.Add(diagram.Clone() as OnionDiagram); }))
            };
            

            if (diagram != null) diagram.PropertyChanged += DiagramPropertyChanged;
        }

        public override string DisplayName => diagram.Name;

        public override string IconSourceString => "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png";

        public override ICommand OpenViewCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(v =>
                v.ViewModel is OnionDiagramViewModel diagramViewModel && diagramViewModel.IsViewModelFor(diagram));
            if (viewInfo == null)
            {
                var onionDiagramViewModel = ViewModelFactory.CreateOnionDiagramViewModel(diagram);
                viewInfo = new ViewInfo(diagram.Name, onionDiagramViewModel, IconSourceString);
                viewManager.OpenView(viewInfo);
            }

            viewManager.BringToFront(viewInfo);
        });

        public override ICommand RemoveItemCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            var viewInfo = viewManager.Views.FirstOrDefault(vi =>
                vi.ViewModel is OnionDiagramViewModel diagramViewModel1 && diagramViewModel1.IsViewModelFor(diagram));
            if (viewInfo != null) viewManager.CloseView(viewInfo);
            analysis.OnionDiagrams.Remove(diagram);
        });

        public override bool IsViewModelFor(object otherObject)
        {
            return otherObject as OnionDiagram == diagram;
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }

        public OnionDiagramPropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateOnionDiagramPropertiesViewModel(diagram);
        }
    }
}