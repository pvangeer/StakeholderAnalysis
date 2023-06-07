using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace StakeholderAnalysis.Data.OnionDiagrams
{
    public class OnionDiagram : NotifyPropertyChangedObservable, IStakeholderDiagram<OnionDiagramStakeholder>,
        ICloneable
    {
        public OnionDiagram(string name, ObservableCollection<OnionRing> onionRings = null,
            ObservableCollection<StakeholderConnection> connections = null,
            ObservableCollection<StakeholderConnectionGroup> connectionGroups = null)
        {
            Name = name;
            OnionRings = onionRings ?? new ObservableCollection<OnionRing>();
            Stakeholders = new ObservableCollection<OnionDiagramStakeholder>();
            Connections = connections ?? new ObservableCollection<StakeholderConnection>();
            ConnectionGroups = connectionGroups ?? new ObservableCollection<StakeholderConnectionGroup>();
            Asymmetry = 0.5;
            Orientation = 210.0;
        }

        public OnionDiagram() : this("")
        {
        }

        public double Asymmetry { get; set; }

        public double Orientation { get; set; }

        public ObservableCollection<OnionRing> OnionRings { get; }

        public ObservableCollection<StakeholderConnection> Connections { get; }

        public ObservableCollection<StakeholderConnectionGroup> ConnectionGroups { get; }

        public string Name { get; set; }

        public object Clone()
        {
            var clone = new OnionDiagram
            {
                Name = Name,
                Asymmetry = Asymmetry
            };

            foreach (var onionRing in OnionRings)
                clone.OnionRings.Add(new OnionRing(onionRing.Percentage)
                {
                    BackgroundColor = onionRing.BackgroundColor.Clone(),
                    StrokeColor = onionRing.StrokeColor.Clone(),
                    StrokeThickness = onionRing.StrokeThickness,
                    LineStyle = onionRing.LineStyle
                });

            foreach (var stakeholder in Stakeholders)
                clone.Stakeholders.Add(
                    new OnionDiagramStakeholder(stakeholder.Stakeholder, stakeholder.Left, stakeholder.Top)
                        { Rank = stakeholder.Rank });

            foreach (var connectionGroup in ConnectionGroups)
                clone.ConnectionGroups.Add(new StakeholderConnectionGroup(connectionGroup.Name,
                    connectionGroup.StrokeColor.Clone(), connectionGroup.StrokeThickness, connectionGroup.LineStyle,
                    connectionGroup.Visible));

            foreach (var connection in Connections)
            {
                var groupClone =
                    clone.ConnectionGroups.ElementAt(ConnectionGroups.IndexOf(connection.StakeholderConnectionGroup));
                var stakeholderFrom = clone.Stakeholders.ElementAt(Stakeholders.IndexOf(connection.ConnectFrom));
                var stakeholderTo = clone.Stakeholders.ElementAt(Stakeholders.IndexOf(connection.ConnectTo));
                clone.Connections.Add(new StakeholderConnection(groupClone, stakeholderFrom, stakeholderTo));
            }

            return clone;
        }

        public ObservableCollection<OnionDiagramStakeholder> Stakeholders { get; }
    }
}