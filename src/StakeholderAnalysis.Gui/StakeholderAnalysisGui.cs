using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui.Annotations;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Storage;

namespace StakeholderAnalysis.Gui
{
    public class StakeholderAnalysisGui : IMessageCollection
    {
        public readonly ObservableCollection<StakeholderConnectionGroupSelection> SelectedStakeholderConnectionGroups;
        private Analysis analysis;

        public StakeholderAnalysisGui() : this(new Analysis())
        {
        }

        private StakeholderAnalysisGui(Analysis analysis)
        {
            SelectedStakeholderConnectionGroups = analysis == null
                ? new ObservableCollection<StakeholderConnectionGroupSelection>()
                : new ObservableCollection<StakeholderConnectionGroupSelection>(analysis.OnionDiagrams.Select(d =>
                    new StakeholderConnectionGroupSelection(d, d.ConnectionGroups.FirstOrDefault())));

            this.analysis = analysis;
            if (analysis != null) analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;

            GuiProjectServices = new GuiProjectServices(this);

            IsProjectExplorerVisible = true;
            IsPropertiesVisible = true;

            ConfigureMessaging();
            Messages = new MessageList();
            IsMagnifierActive = false;
            ViewManager = new ViewManager();

            LogMessageAppender.Instance.MessageCollection = this;
        }

        public string ProjectFilePath { get; set; }

        public VersionInfo VersionInfo { get; set; }

        public Analysis Analysis
        {
            get => analysis;
            set
            {
                if (analysis != null) analysis.OnionDiagrams.CollectionChanged -= OnionDiagramsCollectionChanged;
                analysis = value;
                SelectedStakeholderConnectionGroups.Clear();
                if (analysis != null)
                {
                    analysis.OnionDiagrams.CollectionChanged += OnionDiagramsCollectionChanged;
                    foreach (var onionDiagram in analysis.OnionDiagrams)
                        SelectedStakeholderConnectionGroups.Add(new StakeholderConnectionGroupSelection(onionDiagram,
                            onionDiagram.ConnectionGroups.FirstOrDefault()));
                }
            }
        }

        public ViewManager ViewManager { get; }

        public StorageState BusyIndicator { get; set; }

        public bool IsProjectExplorerVisible { get; set; }

        public bool IsPropertiesVisible { get; set; }

        public bool IsMagnifierActive { get; set; }

        public bool IsSaveToImage { get; set; }

        public Func<bool> ShouldSaveOpenChanges { get; set; }

        public Func<bool> ShouldMigrateProject { get; set; }

        public GuiProjectServices GuiProjectServices { get; }

        public MessageList Messages { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private void ConfigureMessaging()
        {
            var rootLogger = ((Hierarchy)LogManager.GetRepository()).Root;

            if (!rootLogger.Appenders.Cast<IAppender>().Any(a => a is LogMessageAppender))
            {
                rootLogger.AddAppender(new LogMessageAppender());
                rootLogger.Repository.Configured = true;
            }
        }
    }
}