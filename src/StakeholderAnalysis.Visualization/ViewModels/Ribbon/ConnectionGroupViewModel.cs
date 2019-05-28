using System.ComponentModel;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class ConnectionGroupViewModel : NotifyPropertyChangedObservable
    {
        public ConnectionGroupViewModel(StakeholderConnectionGroup stakeholderConnectionGroup)
        {
            StakeholderConnectionGroup = stakeholderConnectionGroup;
            stakeholderConnectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
        }

        public ConnectionGroupViewModel() : this(new StakeholderConnectionGroup("test", Colors.DodgerBlue))
        {
        }

        public StakeholderConnectionGroup StakeholderConnectionGroup { get; }

        public string Name
        {
            get => StakeholderConnectionGroup.Name;
            set => StakeholderConnectionGroup.Name = value;
        }

        public bool IsGroupSelected
        {
            get => StakeholderConnectionGroup.Visible;
            set
            {
                StakeholderConnectionGroup.Visible = value;
                StakeholderConnectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Visible));
            }
        }

        public Color Color
        {
            get => StakeholderConnectionGroup.Color;
            set
            {
                StakeholderConnectionGroup.Color = value;
                StakeholderConnectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Color));
            }
        }

        private void ConnectionGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderConnectionGroup.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(StakeholderConnectionGroup.Visible):
                    OnPropertyChanged(nameof(IsGroupSelected));
                    break;
            }
        }
    }
}