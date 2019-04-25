using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.DataTemplates.TwoAxisDiagrams
{
    interface ITwoAxisDiagramViewModel
    {
        Brush BackgroundBrush { get; }

        string BackgroundTextLeftTop { get; }

        string BackgroundTextRightTop { get; }

        string BackgroundTextLeftBottom { get; }

        string BackgroundTextRightBottom { get; }

        ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        string YAxisMaxLabel { get; }

        string YAxisMinLabel { get; }

        string XAxisMaxLabel { get; }

        string XAxisMinLabel { get; }
    }
}
