using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui.Annotations;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Gui
{
    public class StakeholderAnalysisGui : IMessageCollection
    {
        public StakeholderAnalysisGui() : this(new Analysis()) { }

        public StakeholderAnalysisGui(Analysis analysis)
        {
            Analysis = analysis;
            ConfigureMessaging();
            Messages = new MessageList();
            IsMagnifierActive = false;
            ViewManager = new ViewManager();

            LogMessageAppender.Instance.MessageCollection = this;
        }

        public string ProjectFilePath { get; set; }

        public Analysis Analysis { get; set; }

        public ViewManager ViewManager { get; set; }

        public MessageList Messages { get; }

        public StorageState BusyIndicator { get; set; }

        public bool IsMagnifierActive { get; set; }

        public bool IsSaveToImage { get; set; }

        public Func<bool> ShouldSaveOpenChanges { get; set; }

        private void ConfigureMessaging()
        {
            Logger rootLogger = ((Hierarchy)LogManager.GetRepository()).Root;

            if (!rootLogger.Appenders.Cast<IAppender>().Any(a => a is LogMessageAppender))
            {
                rootLogger.AddAppender(new LogMessageAppender());
                rootLogger.Repository.Configured = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
