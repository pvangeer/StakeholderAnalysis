using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Behaviors;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    internal interface ITwoAxisDiagramViewModel : ISelectionRegister, INameableViewModel
    {
        Brush BackgroundBrush { get; }

        string BackgroundTextLeftTop { get; }

        string BackgroundTextRightTop { get; }

        string BackgroundTextLeftBottom { get; }

        string BackgroundTextRightBottom { get; }

        FontFamily BackgroundFontFamily { get; }

        Brush BackgroundFontColor { get; }

        FontWeight BackgroundFontWeight { get; }

        FontStyle BackgroundFontStyle { get; }

        double BackgroundFontSize { get; }

        ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        string YAxisMaxLabel { get; }

        string YAxisMinLabel { get; }

        string XAxisMaxLabel { get; }

        string XAxisMinLabel { get; }

        ICommand GridClickedCommand { get; }

        FontFamily AxisFontFamily { get; }

        Brush AxisFontColor { get; }

        FontWeight AxisFontWeight { get; }

        FontStyle AxisFontStyle { get; }

        double AxisFontSize { get; }

        ITwoAxisDiagram GetDiagram();

        TwoAxisDiagramPropertiesViewModel GetPropertiesViewModel();
    }
}