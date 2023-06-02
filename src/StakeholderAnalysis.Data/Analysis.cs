using System.Collections.ObjectModel;
using System.Windows.Media;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Data
{
    public class Analysis : NotifyPropertyChangedObservable
    {
        public Analysis()
        {
            OnionDiagrams = new ObservableCollection<OnionDiagram>
            {
                new OnionDiagram("Nieuw ui-diagram")
                {
                    Asymmetry = 0.5,
                    Orientation = 210,
                    ConnectionGroups = { new StakeholderConnectionGroup("Nieuwe groep", Colors.Black) },
                    OnionRings =
                    {
                        new OnionRing(0.45)
                        {
                            StrokeThickness = 0,
                            BackgroundColor = (Color)ColorConverter.ConvertFromString("#080C80")
                        },
                        new OnionRing(0.75)
                        {
                            StrokeThickness = 0,
                            BackgroundColor = (Color)ColorConverter.ConvertFromString("#0D38E0")
                        },
                        new OnionRing() { BackgroundColor = (Color)ColorConverter.ConvertFromString("#0EBBF0") }
                    }
                }
            };
            ForceFieldDiagrams = new ObservableCollection<ForceFieldDiagram> { new ForceFieldDiagram("Nieuw krachtenvelddiagram") };
            AttitudeImpactDiagrams = new ObservableCollection<AttitudeImpactDiagram> { new AttitudeImpactDiagram("Nieuw houding-impact diagram") };
            StakeholderTypes = new ObservableCollection<StakeholderType> { new StakeholderType() };
            Stakeholders = new ObservableCollection<Stakeholder>();
        }

        public ObservableCollection<OnionDiagram> OnionDiagrams { get; }

        public ObservableCollection<ForceFieldDiagram> ForceFieldDiagrams { get; }

        public ObservableCollection<AttitudeImpactDiagram> AttitudeImpactDiagrams { get; }

        public ObservableCollection<Stakeholder> Stakeholders { get; }

        public ObservableCollection<StakeholderType> StakeholderTypes { get; }
    }
}