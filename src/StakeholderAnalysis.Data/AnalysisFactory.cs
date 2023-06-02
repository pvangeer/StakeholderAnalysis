using System.Collections.ObjectModel;
using System.Windows.Media;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Data
{
    public static class AnalysisFactory
    {
        public static Analysis CreateEmptyAnalysis() => new Analysis();


        public static Analysis CreateStandardNewAnalysis()
        {
            return new Analysis
            {
                OnionDiagrams =
                {
                    new OnionDiagram("Nieuw ui-diagram")
                    {
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
                            new OnionRing { BackgroundColor = (Color)ColorConverter.ConvertFromString("#0EBBF0") }
                        },
                        ConnectionGroups =
                        {
                            new StakeholderConnectionGroup("Nieuwe groep", Colors.Black)
                        }
                    }
                },
                ForceFieldDiagrams = { new ForceFieldDiagram("Nieuw krachtenvelddiagram") },
                AttitudeImpactDiagrams = { new AttitudeImpactDiagram("Nieuw houding-impact diagram") },
                StakeholderTypes = { new StakeholderType() }
            };
        }
    }
}