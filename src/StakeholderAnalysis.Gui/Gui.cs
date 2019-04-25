using System.ComponentModel;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Gui.Annotations;

namespace StakeholderAnalysis.Gui
{
    public class Gui : INotifyPropertyChanged
    {
        public Gui()
        {
            IsMagnifierActive = false;
            ViewManager = new ViewManager();
        }

        public ViewManager ViewManager { get; set; }

        public bool IsMagnifierActive { get; set; }
        public bool IsSaveToImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
