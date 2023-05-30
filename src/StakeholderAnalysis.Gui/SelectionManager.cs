using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StakeholderAnalysis.Gui
{
    public class SelectionManager : INotifyPropertyChanged
    {
        private ISelectable CurrentSelectedSelectable { get; set; }

        public object Selection => CurrentSelectedSelectable?.GetSelectableObject();

        public event PropertyChangedEventHandler PropertyChanged;

        public void Select(ISelectable selectable)
        {
            if (CurrentSelectedSelectable != null)
            {
                CurrentSelectedSelectable.IsSelected = false;
                CurrentSelectedSelectable.OnPropertyChanged(nameof(ISelectable.IsSelected));
            }

            CurrentSelectedSelectable = selectable;

            if (CurrentSelectedSelectable != null)
            {
                CurrentSelectedSelectable.IsSelected = true;
                CurrentSelectedSelectable.OnPropertyChanged(nameof(ISelectable.IsSelected));
            }

            OnPropertyChanged(nameof(Selection));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}