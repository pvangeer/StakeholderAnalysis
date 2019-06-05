using System;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Messaging;

namespace StakeholderAnalysis.Visualization.Commands.StatusBar
{
    public class RemoveAllMessagesCommand : ICommand
    {
        private readonly MessageList messageList;

        public RemoveAllMessagesCommand(MessageList messageList)
        {
            this.messageList = messageList;
        }

        public bool CanExecute(object parameter)
        {
            return messageList.Any();
        }

        public void Execute(object parameter)
        {
            messageList.Clear();
        }

        public event EventHandler CanExecuteChanged;
    }
}