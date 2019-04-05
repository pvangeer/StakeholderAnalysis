using System.ComponentModel;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Data.Annotations;

namespace StakeholderAnalysis.Data
{
    public class PropertyChangedElement : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}