using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StakeholderAnalysis.Gui
{
    public class SelectionManager : INotifyPropertyChanged
    {
        public ISelectable Selection { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Select(ISelectable objectToSelect)
        {
            if (Selection != null)
            {
                Selection.IsSelected = false;
                Selection.OnPropertyChanged(nameof(ISelectable.IsSelected));
            }

            Selection = objectToSelect;

            if (Selection != null)
            {
                Selection.IsSelected = true;
                Selection.OnPropertyChanged(nameof(ISelectable.IsSelected));
            }

            OnPropertyChanged(nameof(Selection));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}