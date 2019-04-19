using System.ComponentModel;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Data.Properties;

namespace StakeholderAnalysis.Data
{
    public class PropertyChangedElement : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}