using StakeholderAnalysis.Data.ForceFieldDiagrams;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagram : NotifyPropertyChangedObservable,
        IStakeholderDiagram, ITwoAxisDiagram, ICloneable
    {
        public AttitudeImpactDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<PositionedStakeholder>();
            BrushStartColor = Colors.LightYellow;
            BrushEndColor = Colors.PaleVioletRed;
            BackgroundTextLeftTop = "Informeren";
            BackgroundTextLeftBottom = "Monitoren";
            BackgroundTextRightTop = "Betrekken";
            BackgroundTextRightBottom = "Overtuigen";
            BackgroundFontFamily = SystemFonts.CaptionFontFamily;
            BackgroundFontColor = Colors.DimGray;
            BackgroundFontSize = 64;
            BackgroundFontBold = true;
            BackgroundFontItalic = true;
            YAxisMaxLabel = "Positief";
            YAxisMinLabel = "Negatief";
            XAxisMaxLabel = "Hoge impact";
            XAxisMinLabel = "Lage impact";
            AxisFontFamily = SystemFonts.CaptionFontFamily;
            AxisFontColor = Colors.Black;
            AxisFontSize = 24;
            AxisFontBold = false;
            AxisFontItalic = false;
        }

        public AttitudeImpactDiagram() : this("")
        {
        }

        public object Clone()
        {
            var converter = new FontFamilyConverter();

            var diagram = new AttitudeImpactDiagram
            {
                AxisFontBold = AxisFontBold,
                AxisFontColor = AxisFontColor.Clone(),
                AxisFontFamily =
                    converter.ConvertFromInvariantString(converter.ConvertToInvariantString(AxisFontFamily)) as
                        FontFamily,
                AxisFontItalic = AxisFontItalic,
                AxisFontSize = AxisFontSize,
                BackgroundFontBold = AxisFontBold,
                BackgroundFontColor = BackgroundFontColor.Clone(),
                BackgroundFontFamily =
                    converter.ConvertFromInvariantString(converter.ConvertToInvariantString(BackgroundFontFamily)) as
                        FontFamily,
                BackgroundFontItalic = BackgroundFontItalic,
                BackgroundFontSize = BackgroundFontSize,
                BackgroundTextLeftBottom = BackgroundTextLeftBottom,
                BackgroundTextLeftTop = BackgroundTextLeftTop,
                BackgroundTextRightBottom = BackgroundTextRightBottom,
                BackgroundTextRightTop = BackgroundTextRightTop,
                BrushEndColor = BrushEndColor.Clone(),
                BrushStartColor = BrushStartColor.Clone(),
                Name = Name,
                XAxisMaxLabel = XAxisMaxLabel,
                XAxisMinLabel = XAxisMinLabel,
                YAxisMaxLabel = YAxisMaxLabel,
                YAxisMinLabel = YAxisMinLabel
            };
            foreach (var stakeholder in Stakeholders)
                diagram.Stakeholders.Add(
                    new PositionedStakeholder(stakeholder.Stakeholder, stakeholder.Top,
                            stakeholder.Left)
                        { Rank = stakeholder.Rank });

            return diagram;
        }

        public ObservableCollection<PositionedStakeholder> Stakeholders { get; }

        public string Name { get; set; }

        public Color BrushStartColor { get; set; }

        public Color BrushEndColor { get; set; }

        public string BackgroundTextLeftTop { get; set; }

        public string BackgroundTextRightTop { get; set; }

        public string BackgroundTextLeftBottom { get; set; }

        public string BackgroundTextRightBottom { get; set; }

        public FontFamily BackgroundFontFamily { get; set; }

        public Color BackgroundFontColor { get; set; }

        public bool BackgroundFontBold { get; set; }

        public bool BackgroundFontItalic { get; set; }

        public double BackgroundFontSize { get; set; }

        public string YAxisMaxLabel { get; set; }

        public string YAxisMinLabel { get; set; }

        public string XAxisMaxLabel { get; set; }

        public string XAxisMinLabel { get; set; }

        public FontFamily AxisFontFamily { get; set; }

        public Color AxisFontColor { get; set; }

        public bool AxisFontBold { get; set; }

        public bool AxisFontItalic { get; set; }

        public double AxisFontSize { get; set; }
    }
}