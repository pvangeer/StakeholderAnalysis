﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class ForceFieldDiagram : NotifyPropertyChangedObservable, IRankedStakeholderDiagram<ForceFieldDiagramStakeholder>, ITwoAxisDiagram
    {
        private double axisFontSize;
        private double backgroundFontSize;

        // TODO: Merge with AttitudeImpact diagram (only difference is the stakeholder type and even that can be the same).
        public ForceFieldDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<ForceFieldDiagramStakeholder>();
            BrushStartColor = Colors.PowderBlue;
            BrushEndColor = Colors.LightGreen;
            BackgroundTextLeftTop = "Consulteren";
            BackgroundTextLeftBottom = "Monitoren";
            BackgroundTextRightTop = "Betrekken";
            BackgroundTextRightBottom = "Informeren";
            BackgroundFontFamily = SystemFonts.CaptionFontFamily;
            BackgroundFontColor = Colors.DimGray;
            BackgroundFontSize = 64;
            BackgroundFontBold = true;
            BackgroundFontItalic = true;
            YAxisMaxLabel = "Veel invloed";
            YAxisMinLabel = "Weinig invloed";
            XAxisMaxLabel = "Groot belang";
            XAxisMinLabel = "Klein belang";
            AxisFontFamily = SystemFonts.CaptionFontFamily;
            AxisFontColor = Colors.Black;
            AxisFontSize = 24;
            AxisFontBold = false;
            AxisFontItalic = false;
        }

        public ForceFieldDiagram() : this("") { }

        public string Name { get; set; }

        public ObservableCollection<ForceFieldDiagramStakeholder> Stakeholders { get; }

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
                if (double.IsNaN(value))
                {
                    throw new ArgumentException("Value should not be NaN");
                }
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
                if (double.IsNaN(value))
                {
                    throw new ArgumentException("Value should not be NaN");
                }
                axisFontSize = value;
            }
        }
    }
}
