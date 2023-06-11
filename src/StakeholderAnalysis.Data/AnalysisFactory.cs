using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Data
{
    public static class AnalysisFactory
    {
        public static Analysis CreateEmptyAnalysis()
        {
            return new Analysis();
        }


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
                AttitudeImpactDiagrams = { CreateAttitudeImpactDiagram("Nieuw houding-impact diagram") },
                StakeholderTypes = { new StakeholderType() }
            };
        }

        public static TwoAxisDiagram CreateAttitudeImpactDiagram(string name)
        {
            return new TwoAxisDiagram(name)
            {
                BrushStartColor = Colors.LightYellow,
                BrushEndColor = Colors.PaleVioletRed,
                BackgroundTextLeftTop = "Informeren",
                BackgroundTextLeftBottom = "Monitoren",
                BackgroundTextRightTop = "Betrekken",
                BackgroundTextRightBottom = "Overtuigen",
                BackgroundFontFamily = SystemFonts.CaptionFontFamily,
                BackgroundFontColor = Colors.DimGray,
                BackgroundFontSize = 64,
                BackgroundFontBold = true,
                BackgroundFontItalic = true,
                YAxisMaxLabel = "Positief",
                YAxisMinLabel = "Negatief",
                XAxisMaxLabel = "Hoge impact",
                XAxisMinLabel = "Lage impact",
                AxisFontFamily = SystemFonts.CaptionFontFamily,
                AxisFontColor = Colors.Black,
                AxisFontSize = 24,
                AxisFontBold = false,
                AxisFontItalic = false
            };
        }
    }
}