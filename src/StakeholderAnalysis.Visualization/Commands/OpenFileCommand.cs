using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StakeholderAnalysis.Visualization.ViewModels;

namespace StakeholderAnalysis.Visualization.Commands
{
    public class FileHandleingCommandBase : ICommand
    {
        private MainWindowViewModel mainWindowViewModel;

        public FileHandleingCommandBase(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return false;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class OpenFileCommand : FileHandleingCommandBase
    {
        public OpenFileCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
        }
    }

    public class SaveFileCommand : FileHandleingCommandBase
    {
        public SaveFileCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
        }
    }


    public class SaveFileAsCommand : FileHandleingCommandBase
    {
        public SaveFileAsCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
        }
    }

    public class NewProjectCommand : FileHandleingCommandBase
    {
        public NewProjectCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {
        }
    }

}
