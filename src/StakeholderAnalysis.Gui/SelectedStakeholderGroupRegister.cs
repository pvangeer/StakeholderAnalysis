using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Gui
{
    public class SelectedStakeholderGroupRegister
    {
        public readonly ObservableCollection<StakeholderConnectionGroupSelection> SelectedStakeholderConnectionGroups;
        private Analysis analysis;

        public SelectedStakeholderGroupRegister()
        {
            SelectedStakeholderConnectionGroups = new ObservableCollection<StakeholderConnectionGroupSelection>();
        }

        public Analysis Analysis
        {
            get => analysis;
            set
            {
                if (analysis != null)
                    analysis.OnionDiagrams.CollectionChanged -= OnionDiagramsCollectionChanged;

                analysis = value;
                SelectedStakeholderConnectionGroups.Clear();

                if (analysis != null)
                {
                    analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;
                    foreach (var onionDiagram in analysis.OnionDiagrams)
                        SelectedStakeholderConnectionGroups.Add(
                            new StakeholderConnectionGroupSelection(onionDiagram, onionDiagram.ConnectionGroups.FirstOrDefault()));
                }
            }
        }

        private void OnionDiagramsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var onionDiagram in e.NewItems.OfType<OnionDiagram>())
                    {
                        var selection =
                            SelectedStakeholderConnectionGroups.FirstOrDefault(g => g.OnionDiagram == onionDiagram);
                        if (selection == null)
                        {
                            SelectedStakeholderConnectionGroups.Add(
                                new StakeholderConnectionGroupSelection(onionDiagram,
                                    onionDiagram.ConnectionGroups.FirstOrDefault()));
                            onionDiagram.ConnectionGroups.CollectionChanged +=
                                OnionDiagramConnectionGroupsCollectionChanged;
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var onionDiagram in e.OldItems.OfType<OnionDiagram>())
                    {
                        var selection =
                            SelectedStakeholderConnectionGroups.FirstOrDefault(g => g.OnionDiagram == onionDiagram);
                        if (selection != null)
                        {
                            onionDiagram.ConnectionGroups.CollectionChanged -=
                                OnionDiagramConnectionGroupsCollectionChanged;
                            SelectedStakeholderConnectionGroups.Remove(selection);
                        }
                    }

                    break;
            }
        }

        private void OnionDiagramConnectionGroupsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var diagram = Analysis.OnionDiagrams.FirstOrDefault(d =>
                d.ConnectionGroups == sender as ObservableCollection<StakeholderConnectionGroup>);
            if (diagram == null) return;
            var selection = SelectedStakeholderConnectionGroups.FirstOrDefault(s => s.OnionDiagram == diagram);
            if (selection == null) return;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (selection.StakeholderConnectionGroup == null)
                    {
                        selection.StakeholderConnectionGroup = diagram.ConnectionGroups.FirstOrDefault();
                        selection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection
                            .StakeholderConnectionGroup));
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var stakeholderConnectionGroup in e.OldItems.OfType<StakeholderConnectionGroup>())
                        if (selection.StakeholderConnectionGroup == stakeholderConnectionGroup)
                        {
                            selection.StakeholderConnectionGroup = diagram.ConnectionGroups.FirstOrDefault();
                            selection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection
                                .StakeholderConnectionGroup));
                        }

                    break;
            }
        }
    }
}