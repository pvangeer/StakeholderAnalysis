using System.ComponentModel;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui.Annotations;

namespace StakeholderAnalysis.Gui
{
    public class StakeholderConnectionGroupSelection : INotifyPropertyChanged
    {
        // TODO: Merge this with the selectionManager?
        public StakeholderConnectionGroupSelection(OnionDiagram diagram, StakeholderConnectionGroup connectionGroup)
        {
            OnionDiagram = diagram;
            StakeholderConnectionGroup = connectionGroup;
        }

        public OnionDiagram OnionDiagram { get; }

        public StakeholderConnectionGroup StakeholderConnectionGroup { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}