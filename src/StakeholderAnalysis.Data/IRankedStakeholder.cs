using System.ComponentModel;

namespace StakeholderAnalysis.Data
{
    public interface IRankedStakeholder : INotifyPropertyChanged
    {
        int Rank { get; set; }

        Stakeholder Stakeholder { get; }

        void OnPropertyChanged(string propertyName);
    }
}