using System.ComponentModel;

namespace StakeholderAnalysis.Data
{
    public interface IRankedStakeholder : INotifyPropertyChanged
    {
        int Rank { get; set; }

        double Left { get; set; }

        double Top { get; set; }

        Stakeholder Stakeholder { get; }

        void OnPropertyChanged(string propertyName);
    }
}