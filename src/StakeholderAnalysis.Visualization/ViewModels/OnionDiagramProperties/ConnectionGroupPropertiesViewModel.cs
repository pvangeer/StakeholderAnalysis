using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class ConnectionGroupPropertiesViewModel : ViewModelBase, IExpandableContentViewModel
    {
        private readonly StakeholderConnectionGroup connectionGroup;
        private bool isExpanded;
        private readonly OnionDiagram diagram;

        public ConnectionGroupPropertiesViewModel(ViewModelFactory factory, StakeholderConnectionGroup connectionGroup, OnionDiagram selectedOnionDiagram) : base(factory)
        {
            this.diagram = selectedOnionDiagram;
            this.connectionGroup = connectionGroup;
            connectionGroup.PropertyChanged += ConnectionGroupPropertyChanged;
        }

        private void ConnectionGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderConnectionGroup.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(StakeholderConnectionGroup.Visible):
                    OnPropertyChanged(nameof(IsVisible));
                    break;
                case nameof(StakeholderConnectionGroup.StrokeThickness):
                    OnPropertyChanged(nameof(StrokeThickness));
                    break;
                case nameof(StakeholderConnectionGroup.Color):
                    OnPropertyChanged(nameof(StrokeColor));
                    break;
            }
        }

        public bool IsViewModelFor(StakeholderConnectionGroup otherConnectionGroup)
        {
            return otherConnectionGroup == connectionGroup;
        }

        public string DisplayName => connectionGroup.Name;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public string Name
        {
            get => connectionGroup.Name;
            set
            {
                connectionGroup.Name = value;
                connectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Name));
            }
        }

        public Color StrokeColor
        {
            get => connectionGroup.Color;
            set
            {
                connectionGroup.Color = value;
                connectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Color));
            }
        }

        public bool IsVisible
        {
            get => connectionGroup.Visible;
            set
            {
                connectionGroup.Visible = value;
                connectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.Visible));
            }
        }

        public double StrokeThickness
        {
            get => connectionGroup.StrokeThickness;
            set
            {
                connectionGroup.StrokeThickness = value;
                connectionGroup.OnPropertyChanged(nameof(StakeholderConnectionGroup.StrokeThickness));
            }
        }

        public ICommand ToggleIsExpandedCommand => CommandFactory.CreateToggleIsExpandedCommand(this);

        public ICommand RemoveConnectionGroupCommand => CommandFactory.CreateCanAlwaysExecuteActionCommand(p =>
        {
            diagram.ConnectionGroups.Remove(connectionGroup);
        });
    }
}
