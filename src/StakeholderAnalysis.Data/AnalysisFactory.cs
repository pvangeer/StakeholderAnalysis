using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data.Diagrams;
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
                OnionDiagrams = { CreateOnionDiagram("Nieuw ui-diagram") },
                ForceFieldDiagrams = { CreateForceFieldDiagram("Nieuw krachtenvelddiagram") },
                AttitudeImpactDiagrams = { CreateAttitudeImpactDiagram("Nieuw houding-impact diagram") },
                StakeholderTypes = { new StakeholderType() }
            };
        }

        public static OnionDiagram CreateOnionDiagram(string name)
        {
            return new OnionDiagram(name)
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
            };
        }

        public static TwoAxisDiagram CreateForceFieldDiagram(string name)
        {
            return new TwoAxisDiagram(name)
            {
                BrushStartColor = Colors.PowderBlue,
                BrushEndColor = Colors.LightGreen,
                BackgroundTextLeftTop = "Consulteren",
                BackgroundTextLeftBottom = "Monitoren",
                BackgroundTextRightTop = "Betrekken",
                BackgroundTextRightBottom = "Informeren",
                BackgroundFontFamily = SystemFonts.CaptionFontFamily,
                BackgroundFontColor = Colors.DimGray,
                BackgroundFontSize = 64,
                BackgroundFontBold = true,
                BackgroundFontItalic = true,
                YAxisMaxLabel = "Veel invloed",
                YAxisMinLabel = "Weinig invloed",
                XAxisMaxLabel = "Groot belang",
                XAxisMinLabel = "Klein belang",
                AxisFontFamily = SystemFonts.CaptionFontFamily,
                AxisFontColor = Colors.Black,
                AxisFontSize = 24,
                AxisFontBold = false,
                AxisFontItalic = false
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