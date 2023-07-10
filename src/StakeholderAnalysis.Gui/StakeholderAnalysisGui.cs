using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui.Properties;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Storage;

namespace StakeholderAnalysis.Gui
{
    public class StakeholderAnalysisGui : IMessageCollection
    {
        private Analysis analysis;

        public StakeholderAnalysisGui() : this(AnalysisFactory.CreateStandardNewAnalysis())
        {
        }

        private StakeholderAnalysisGui(Analysis analysis)
        {
            IsMagnifierActive = false;
            IsProjectExplorerVisible = true;
            IsPropertiesVisible = true;

            GuiProjectServices = new GuiProjectServices(this);
            SelectionManager = new SelectionManager();
            ViewManager = new ViewManager(SelectionManager);
            SelectedStakeholderGroupRegister = new SelectedStakeholderGroupRegister();

            Analysis = analysis;

            ConfigureMessaging();
            Messages = new MessageList();
            LogMessageAppender.Instance.MessageCollection = this;
        }

        public SelectedStakeholderGroupRegister SelectedStakeholderGroupRegister { get; }

        public string ProjectFilePath { get; set; }

        public VersionInfo VersionInfo { get; set; }

        public Analysis Analysis
        {
            get => analysis;
            set
            {
                analysis = value;
                SelectedStakeholderGroupRegister.Analysis = analysis;
            }
        }

        public SelectionManager SelectionManager { get; set; }

        public ViewManager ViewManager { get; }

        public StorageState BusyIndicator { get; set; }

        public bool IsProjectExplorerVisible { get; set; }

        public bool IsPropertiesVisible { get; set; }

        public bool IsMagnifierActive { get; set; }

        public bool IsSaveToImage { get; set; }

        public Func<ShouldProceedState> ShouldSaveOpenChanges { get; set; }

        public Func<bool> ShouldMigrateProject { get; set; }

        public GuiProjectServices GuiProjectServices { get; }

        public MessageList Messages { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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