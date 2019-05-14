using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.OnionDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class ConnectionGroupPropertiesViewModel : NotifyPropertyChangedObservable, IExpandableContentViewModel
    {
        private readonly StakeholderConnectionGroup connectionGroup;
        private bool isExpanded;
        private OnionDiagram diagram;

        public ConnectionGroupPropertiesViewModel(StakeholderConnectionGroup connectionGroup, OnionDiagram selectedOnionDiagram)
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
        public ICommand ToggleIsExpandedCommand => new ToggleIsExpandedCommand(this);

        public ICommand RemoveConnectionGroupCommand => new RemoveConnectionGroupCommand(this);

        public void RemoveConnectionGroup()
        {
            diagram.ConnectionGroups.Remove(connectionGroup);
        }
    }
}
