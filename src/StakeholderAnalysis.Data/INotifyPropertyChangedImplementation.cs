using System.ComponentModel;

namespace StakeholderAnalysis.Data
{
    public interface INotifyPropertyChangedImplementation: INotifyPropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }
}