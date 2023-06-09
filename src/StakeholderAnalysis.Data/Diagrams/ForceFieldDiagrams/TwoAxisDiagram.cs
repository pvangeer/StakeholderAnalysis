using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams
{
    public class TwoAxisDiagram : NotifyPropertyChangedObservable, ITwoAxisDiagram, ICloneable
    {
        private double axisFontSize;
        private double backgroundFontSize;
        
        public TwoAxisDiagram()
        {
            Stakeholders = new ObservableCollection<PositionedStakeholder>();
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

        public double BackgroundFontSize
        {
            get => backgroundFontSize;
            set
            {
                if (double.IsNaN(value)) throw new ArgumentException("Value should not be NaN");
                backgroundFontSize = value;
            }
        }

        public string YAxisMaxLabel { get; set; }
        
        public string YAxisMinLabel { get; set; }
        
        public string XAxisMaxLabel { get; set; }
        
        public string XAxisMinLabel { get; set; }
        
        public FontFamily AxisFontFamily { get; set; }
        
        public Color AxisFontColor { get; set; }
        
        public bool AxisFontBold { get; set; }
        
        public bool AxisFontItalic { get; set; }

        public double AxisFontSize
        {
            get => axisFontSize;
            set
            {
                if (double.IsNaN(value)) throw new ArgumentException("Value should not be NaN");
                axisFontSize = value;
            }
        }

        public object Clone()
        {
            var converter = new FontFamilyConverter();

            var diagram = new ForceFieldDiagram
            {
                AxisFontBold = AxisFontBold,
                AxisFontColor = ColorExtensions.Clone(AxisFontColor),
                AxisFontFamily =
                    converter.ConvertFromInvariantString(converter.ConvertToInvariantString(AxisFontFamily)) as
                        FontFamily,
                AxisFontItalic = AxisFontItalic,
                AxisFontSize = AxisFontSize,
                BackgroundFontBold = AxisFontBold,
                BackgroundFontColor = ColorExtensions.Clone(BackgroundFontColor),
                BackgroundFontFamily =
                    converter.ConvertFromInvariantString(converter.ConvertToInvariantString(BackgroundFontFamily)) as
                        FontFamily,
                BackgroundFontItalic = BackgroundFontItalic,
                BackgroundFontSize = BackgroundFontSize,
                BackgroundTextLeftBottom = BackgroundTextLeftBottom,
                BackgroundTextLeftTop = BackgroundTextLeftTop,
                BackgroundTextRightBottom = BackgroundTextRightBottom,
                BackgroundTextRightTop = BackgroundTextRightTop,
                BrushEndColor = ColorExtensions.Clone(BrushEndColor),
                BrushStartColor = ColorExtensions.Clone(BrushStartColor),
                Name = Name,
                XAxisMaxLabel = XAxisMaxLabel,
                XAxisMinLabel = XAxisMinLabel,
                YAxisMaxLabel = YAxisMaxLabel,
                YAxisMinLabel = YAxisMinLabel
            };
            foreach (var stakeholder in Stakeholders)
                diagram.Stakeholders.Add(
                    new PositionedStakeholder(stakeholder.Stakeholder, stakeholder.Left,
                        stakeholder.Top) { Rank = stakeholder.Rank });

            return diagram;
        }
    }
}