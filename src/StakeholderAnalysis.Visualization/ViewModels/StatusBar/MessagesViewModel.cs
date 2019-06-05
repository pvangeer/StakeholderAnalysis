using System.Collections.Specialized;
using System.Windows.Input;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Visualization.ViewModels.StatusBar
{
    public class MessageListViewModel : ViewModelBase
    {
        public MessageListViewModel(ViewModelFactory factory, MessageList messageList) : base(factory)
        {
            this.MessageList = messageList;
            if (messageList != null)
            {
                MessageList.CollectionChanged += MessageListCollectionChanged;
            }
        }

        private void MessageListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(RemoveAllMessagesCommand));
            OnPropertyChanged(nameof(MessageList));
        }

        public MessageList MessageList { get; }

        public ICommand RemoveAllMessagesCommand => CommandFactory.CreateRemoveAllMessagesCommand(MessageList);
    }
}