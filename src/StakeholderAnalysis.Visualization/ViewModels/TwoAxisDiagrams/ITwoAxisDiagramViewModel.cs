using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    interface ITwoAxisDiagramViewModel : ISelectionRegister
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
        ICommand GridClickedCommand { get; }
    }
}
