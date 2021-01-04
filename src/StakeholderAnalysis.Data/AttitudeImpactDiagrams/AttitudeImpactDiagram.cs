using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace StakeholderAnalysis.Data.AttitudeImpactDiagrams
{
    public class AttitudeImpactDiagram : NotifyPropertyChangedObservable, IRankedStakeholderDiagram<AttitudeImpactDiagramStakeholder>, ITwoAxisDiagram, ICloneable
    {
        public AttitudeImpactDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<AttitudeImpactDiagramStakeholder>();
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

        public AttitudeImpactDiagram() : this("") { }

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

        public ObservableCollection<AttitudeImpactDiagramStakeholder> Stakeholders { get; }

        public Color AxisFontColor { get; set; }

        public bool AxisFontBold { get; set; }

        public bool AxisFontItalic { get; set; }

        public double AxisFontSize { get; set; }

        public object Clone()
        {
                var converter = new FontFamilyConverter();

                var diagram = new AttitudeImpactDiagram
                {
                    AxisFontBold = this.AxisFontBold,
                    AxisFontColor = this.AxisFontColor.Clone(),
                    AxisFontFamily = converter.ConvertFromInvariantString(converter.ConvertToInvariantString(this.AxisFontFamily)) as FontFamily,
                    AxisFontItalic = this.AxisFontItalic,
                    AxisFontSize = this.AxisFontSize,
                    BackgroundFontBold = this.AxisFontBold,
                    BackgroundFontColor = this.BackgroundFontColor.Clone(),
                    BackgroundFontFamily = converter.ConvertFromInvariantString(converter.ConvertToInvariantString(this.BackgroundFontFamily)) as FontFamily,
                    BackgroundFontItalic = this.BackgroundFontItalic,
                    BackgroundFontSize = this.BackgroundFontSize,
                    BackgroundTextLeftBottom = this.BackgroundTextLeftBottom,
                    BackgroundTextLeftTop = this.BackgroundTextLeftTop,
                    BackgroundTextRightBottom = this.BackgroundTextRightBottom,
                    BackgroundTextRightTop = this.BackgroundTextRightTop,
                    BrushEndColor = this.BrushEndColor.Clone(),
                    BrushStartColor = this.BrushStartColor.Clone(),
                    Name = this.Name,
                    XAxisMaxLabel = this.XAxisMaxLabel,
                    XAxisMinLabel = this.XAxisMinLabel,
                    YAxisMaxLabel = this.YAxisMaxLabel,
                    YAxisMinLabel = this.YAxisMinLabel
                };
                foreach (var stakeholder in Stakeholders)
                {
                    diagram.Stakeholders.Add(
                        new AttitudeImpactDiagramStakeholder(stakeholder.Stakeholder, stakeholder.Attitude,
                                stakeholder.Impact)
                            { Rank = stakeholder.Rank });
                }

                return diagram;
            }
    }
}
