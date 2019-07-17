﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace StakeholderAnalysis.Data.ForceFieldDiagrams
{
    public class ForceFieldDiagram : NotifyPropertyChangedObservable, IRankedStakeholderDiagram<ForceFieldDiagramStakeholder>, ITwoAxisDiagram
    {
        public ForceFieldDiagram(string name)
        {
            Name = name;
            Stakeholders = new ObservableCollection<ForceFieldDiagramStakeholder>();
            BackgroundBrush = new LinearGradientBrush(Colors.PowderBlue, Colors.LightGreen, new Point(0, 1), new Point(1, 0));
            BackgroundTextLeftTop = "Consulteren";
            BackgroundTextLeftBottom = "Monitoren";
            BackgroundTextRightTop = "Betrekken";
            BackgroundTextRightBottom = "Informeren";
            YAxisMaxLabel = "Veel invloed";
            YAxisMinLabel = "Weinig invloed";
            XAxisMaxLabel = "Groot belang";
            XAxisMinLabel = "Klein belang";
        }

        public ForceFieldDiagram() : this("") { }

        public string Name { get; set; }

        public ObservableCollection<ForceFieldDiagramStakeholder> Stakeholders { get; }

        public Brush BackgroundBrush { get; set; }

        public string BackgroundTextLeftTop { get; set; }

        public string BackgroundTextRightTop { get; set; }

        public string BackgroundTextLeftBottom { get; set; }

        public string BackgroundTextRightBottom { get; set; }

        public string YAxisMaxLabel { get; set; }

        public string YAxisMinLabel { get; set; }

        public string XAxisMaxLabel { get; set; }

        public string XAxisMinLabel { get; set; }
    }
}
