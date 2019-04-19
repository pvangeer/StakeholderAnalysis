﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StakeholderAnalysis.Data
{
    public class OnionRing : PropertyChangedElement
    {
        public OnionRing(string name = "", double percentage = 1.0)
        {
            Name = name;
            Percentage = percentage;
            BackgroundColor = Colors.Azure;
            StrokeColor = Colors.Gray;
            StrokeThickness = 1.0;
        }

        public string Name { get; }

        public double Percentage { get; set; }

        public Color BackgroundColor { get; set; }

        public Color StrokeColor { get; set; }

        public double StrokeThickness { get; set; }
    }
}
