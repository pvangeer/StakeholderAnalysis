using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ConnectionGroupViewModel : PropertyChangedElement
    {
        public ConnectionGroupViewModel(ConnectionGroup connectionGroup)
        {
            this.ConnectionGroup = connectionGroup;
            connectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
        }

        public ConnectionGroupViewModel() : this(new ConnectionGroup("test",Colors.DodgerBlue))
        {
        }

        private void ConnectionGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ConnectionGroup.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(ConnectionGroup.Visible):
                    OnPropertyChanged(nameof(IsGroupSelected));
                    break;
            }
        }

        public ConnectionGroup ConnectionGroup { get; }

        public string Name
        {
            get => ConnectionGroup.Name;
            set => ConnectionGroup.Name = value;
        }

        public bool IsGroupSelected
        {
            get => ConnectionGroup.Visible;
            set
            {
                ConnectionGroup.Visible = value;
                ConnectionGroup.OnPropertyChanged(nameof(ConnectionGroup.Visible));
            }
        }

        public Color Color
        {
            get => ConnectionGroup.Color;
            set
            {
                ConnectionGroup.Color = value;
                ConnectionGroup.OnPropertyChanged(nameof(ConnectionGroup.Color));
            }
        }

        public ICommand ChangeColorCommand => new ChangeColorCommand(ConnectionGroup);
    }

    public class ChangeColorCommand : ICommand
    {
        public ChangeColorCommand(ConnectionGroup connectionGroup)
        {
            throw new NotImplementedException();
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
