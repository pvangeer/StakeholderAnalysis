using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class ConnectionGroupViewModel : NotifyPropertyChangedObservable
    {
        public ConnectionGroupViewModel(ConnectionGroup connectionGroup)
        {
            ConnectionGroup = connectionGroup;
            connectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
        }

        public ConnectionGroupViewModel() : this(new ConnectionGroup("test", Colors.DodgerBlue))
        {
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