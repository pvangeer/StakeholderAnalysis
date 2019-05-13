using System.Collections.Specialized;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Messaging;
using StakeholderAnalysis.Visualization.Commands.StatusBar;

namespace StakeholderAnalysis.Visualization.ViewModels.StatusBar
{
    public class MessageListViewModel : NotifyPropertyChangedObservable
    {
        public MessageListViewModel(MessageList messageList)
        {
            this.MessageList = messageList;
            MessageList.CollectionChanged += MessageListCollectionChanged;
        }

        private void MessageListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(RemoveAllMessagesCommand));
            OnPropertyChanged(nameof(MessageList));
        }

        public MessageList MessageList { get; }

        public ICommand RemoveAllMessagesCommand => new RemoveAllMessagesCommand(this);

        public void ClearAllMessages()
        {
            MessageList.Clear();
        }

    }
}