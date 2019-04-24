﻿using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization
{
    public class PresentationLayoutTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ToolWindowViewInfo)
            {
                return AnchorableLayoutTemplate;
            }

            if (item is ViewInfo)
            {
                return DocumentLayoutTemplate;
            }

            return base.SelectTemplate(item, container);
        }

        public DataTemplate DocumentLayoutTemplate { get; set; }

        public DataTemplate AnchorableLayoutTemplate { get; set; }
    }
}
