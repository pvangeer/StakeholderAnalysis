using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StakeholderAnalysis.Messaging
{
    public interface IMessageCollection : INotifyPropertyChanged
    {
        MessageList Messages { get; }

        void OnPropertyChanged(string propertyName = null);
    }

    public class MessageList : ObservableCollection<LogMessage> { }
}