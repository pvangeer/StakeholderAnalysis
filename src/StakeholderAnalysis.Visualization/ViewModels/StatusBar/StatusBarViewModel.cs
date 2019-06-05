using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Visualization.ViewModels.StatusBar
{
    public class StatusBarViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;
        private MessageListViewModel messageListViewModel;

        public StatusBarViewModel(ViewModelFactory viewModelFactory, StakeholderAnalysisGui gui) : base(viewModelFactory)
        {
            this.gui = gui;

            if (gui != null)
            {
                gui.PropertyChanged += GuiPropertyChanged;
                gui.Messages.CollectionChanged += GuiMessagesCollectionChanged;
            }
        }

        public bool ShowMessages { get; set; }

        public ICommand ShowMessageListCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            ShowMessages = MessagesViewModel.MessageList.Count != 0;
            OnPropertyChanged(nameof(ShowMessages));
        });

        public MessageListViewModel MessagesViewModel =>
            messageListViewModel ?? (messageListViewModel = ViewModelFactory.CreateMessageListViewModel());

        public ICommand RemoveLastMessageCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            PriorityMessage = null;
            OnPropertyChanged(nameof(PriorityMessage));
        });

        public LogMessage PriorityMessage { get; set; }

        private void GuiMessagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var newItem = e.NewItems.OfType<LogMessage>().First();
                if (newItem.HasPriority)
                {
                    PriorityMessage = newItem;
                    OnPropertyChanged(nameof(PriorityMessage));
                }
                OnPropertyChanged(nameof(MessagesViewModel));
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var items = e.OldItems.OfType<LogMessage>();
                foreach (var logMessage in items)
                {
                    if (PriorityMessage == logMessage)
                    {
                        PriorityMessage = null;
                        OnPropertyChanged(nameof(PriorityMessage));
                    }
                }

                if (!MessagesViewModel.MessageList.Any())
                {
                    ShowMessages = false;
                    OnPropertyChanged(nameof(ShowMessages));
                }
                OnPropertyChanged(nameof(MessagesViewModel));
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                PriorityMessage = null;
                OnPropertyChanged(nameof(PriorityMessage));
                OnPropertyChanged(nameof(MessagesViewModel));
                ShowMessages = false;
                OnPropertyChanged(nameof(ShowMessages));
            }
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
               case nameof(StakeholderAnalysisGui.Messages):
                    messageListViewModel = null;
                    OnPropertyChanged(nameof(MessagesViewModel));
                    break;
            }
        }
    }
}
