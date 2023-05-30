using System.Collections.Specialized;
using System.Windows.Input;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Visualization.ViewModels.StatusBar
{
    public class MessageListViewModel : ViewModelBase
    {
        public MessageListViewModel(ViewModelFactory factory, MessageList messageList) : base(factory)
        {
            MessageList = messageList;
            if (messageList != null) MessageList.CollectionChanged += MessageListCollectionChanged;
        }

        public MessageList MessageList { get; }

        public ICommand RemoveAllMessagesCommand => CommandFactory.CreateRemoveAllMessagesCommand(MessageList);

        private void MessageListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(RemoveAllMessagesCommand));
            OnPropertyChanged(nameof(MessageList));
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}