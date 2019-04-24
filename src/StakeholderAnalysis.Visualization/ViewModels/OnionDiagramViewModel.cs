using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class OnionDiagramViewModel: NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram diagram;
        private readonly Analysis analysis;
        private readonly ViewManager viewManager;

        public OnionDiagramViewModel(Analysis analysis, OnionDiagram onionDiagram, ViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.analysis = analysis;
            diagram = onionDiagram;
            if (diagram != null)
            {
                diagram.PropertyChanged += DiagramPropertyChanged;
            }
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
            }
        }

        public string Name
        {
            get => diagram.Name;
            set
            {
                diagram.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public OnionRingsCanvasViewModel OnionRingsCanvasViewModel => new OnionRingsCanvasViewModel(diagram);

        public OnionConnectionsPresenterViewModel OnionConnectionsPresenterViewModel => new OnionConnectionsPresenterViewModel(diagram);

        public OnionDiagramStakeholdersViewModel OnionDiagramStakeholdersViewModel => new OnionDiagramStakeholdersViewModel(diagram);

        public ICommand RemoveOnionDiagramCommand => new RemoveOnionDiagramCommand(analysis, diagram);

        public ICommand OpenOnionDiagramCommand => new OpenOnionDiagramCommand(this);

        public void RegisterConnectionGroupsCollectionChanged(NotifyCollectionChangedEventHandler handler)
        {
            if (diagram != null)
            {
                diagram.ConnectionGroups.CollectionChanged += handler;
            }
        }

        public void UnRegisterConnectionGroupsCollectionChanged(NotifyCollectionChangedEventHandler handler)
        {
            if (diagram != null)
            {
                diagram.ConnectionGroups.CollectionChanged -= handler;
            }
        }

        public ObservableCollection<ConnectionGroupViewModel> GetConnectionGroupsViewModels()
        {
            return new ObservableCollection<ConnectionGroupViewModel>(diagram.ConnectionGroups.Select(g => new ConnectionGroupViewModel(g)));
        }

        public bool IsViewModelFor(OnionDiagram onionDiagram)
        {
            return onionDiagram == this.diagram;
        }

        public void OpenDiagramInDocumentView()
        {
            var viewInfo = new ViewInfo(diagram.Name, new OnionDiagramViewModel(analysis,diagram,viewManager), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png");
            viewManager.OpenView(viewInfo);
            viewManager.BringToFront(viewInfo);
        }
    }
}
