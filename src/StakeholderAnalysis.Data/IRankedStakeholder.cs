using System.ComponentModel;

namespace StakeholderAnalysis.Data
{
    public interface IRankedStakeholder : INotifyPropertyChanged
    {
        int Rank { get; set; }

        void OnPropertyChanged(string propertyName);
    }
}