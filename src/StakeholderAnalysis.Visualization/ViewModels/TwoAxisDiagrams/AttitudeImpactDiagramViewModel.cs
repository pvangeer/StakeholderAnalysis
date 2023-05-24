using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramViewModel : ViewModelBase, ITwoAxisDiagramViewModel
    {
        private readonly AttitudeImpactDiagram diagram;
        private object selectedObject;

        public AttitudeImpactDiagramViewModel(ViewModelFactory factory, AttitudeImpactDiagram attitudeImpactDiagram) :
            base(factory)
        {
            diagram = attitudeImpactDiagram;
            if (attitudeImpactDiagram != null)
            {
                diagram.PropertyChanged += DiagramPropertyChanged;
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(
                    attitudeImpactDiagram.Stakeholders.Select(stakeholder =>
                        ViewModelFactory.CreateAttitudeImpactDiagramStakeholderViewModel(diagram, stakeholder, this)));
            }
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        public Brush BackgroundBrush => new LinearGradientBrush(diagram.BrushStartColor, diagram.BrushEndColor,
            new Point(0, 0), new Point(1, 1));

        public string BackgroundTextLeftTop => diagram.BackgroundTextLeftTop;

        public string BackgroundTextRightTop => diagram.BackgroundTextRightTop;

        public string BackgroundTextLeftBottom => diagram.BackgroundTextLeftBottom;

        public string BackgroundTextRightBottom => diagram.BackgroundTextRightBottom;

        public FontFamily BackgroundFontFamily => diagram.BackgroundFontFamily;

        public Brush BackgroundFontColor => new SolidColorBrush(diagram.BackgroundFontColor);

        public FontWeight BackgroundFontWeight => diagram.BackgroundFontBold ? FontWeights.Bold : FontWeights.Normal;

        public FontStyle BackgroundFontStyle => diagram.BackgroundFontItalic ? FontStyles.Italic : FontStyles.Normal;

        public double BackgroundFontSize => diagram.BackgroundFontSize;

        public string YAxisMaxLabel => diagram.YAxisMaxLabel;

        public string YAxisMinLabel => diagram.YAxisMinLabel;

        public string XAxisMaxLabel => diagram.XAxisMaxLabel;

        public string XAxisMinLabel => diagram.XAxisMinLabel;

        public FontFamily AxisFontFamily => diagram.AxisFontFamily;

        public Brush AxisFontColor => new SolidColorBrush(diagram.AxisFontColor);

        public FontWeight AxisFontWeight => diagram.AxisFontBold ? FontWeights.Bold : FontWeights.Normal;

        public FontStyle AxisFontStyle => diagram.AxisFontItalic ? FontStyles.Italic : FontStyles.Normal;

        public double AxisFontSize => diagram.AxisFontSize;

        public ICommand GridClickedCommand => CommandFactory.CreateClearSelectionCommand(this);

        public bool IsSelected(object o)
        {
            return selectedObject == o;
        }

        public void Select(object o)
        {
            selectedObject = o;
            foreach (var stakeholder in PositionedStakeholders.OfType<StakeholderViewModel>())
                stakeholder.OnPropertyChanged(nameof(StakeholderViewModel.IsSelectedStakeholder));
        }

        public ITwoAxisDiagram GetDiagram()
        {
            return diagram;
        }

        public TwoAxisDiagramPropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateTwoAxisDiagramPropertiesViewModel();
        }

        public string DisplayName
        {
            get => diagram?.Name;
            set
            {
                if (diagram != null)
                {
                    diagram.Name = value;
                    diagram.OnPropertyChanged(nameof(ITwoAxisDiagram.Name));
                }
            }
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<AttitudeImpactDiagramStakeholder>())
                    PositionedStakeholders.Add(
                        ViewModelFactory.CreateAttitudeImpactDiagramStakeholderViewModel(diagram, item, this));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<AttitudeImpactDiagramStakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder.Stakeholder)));
        }

        public bool IsViewModelFor(AttitudeImpactDiagram otherDiagram)
        {
            return otherDiagram == diagram;
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ITwoAxisDiagram.BrushStartColor):
                case nameof(ITwoAxisDiagram.BrushEndColor):
                    OnPropertyChanged(nameof(BackgroundBrush));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundTextLeftTop):
                    OnPropertyChanged(nameof(BackgroundTextLeftTop));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundTextRightTop):
                    OnPropertyChanged(nameof(BackgroundTextRightTop));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundTextLeftBottom):
                    OnPropertyChanged(nameof(BackgroundTextLeftBottom));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundTextRightBottom):
                    OnPropertyChanged(nameof(BackgroundTextRightBottom));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundFontFamily):
                    OnPropertyChanged(nameof(BackgroundFontFamily));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundFontColor):
                    OnPropertyChanged(nameof(BackgroundFontColor));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundFontBold):
                    OnPropertyChanged(nameof(BackgroundFontWeight));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundFontItalic):
                    OnPropertyChanged(nameof(BackgroundFontStyle));
                    break;
                case nameof(ITwoAxisDiagram.BackgroundFontSize):
                    OnPropertyChanged(nameof(BackgroundFontSize));
                    break;
                case nameof(ITwoAxisDiagram.YAxisMaxLabel):
                    OnPropertyChanged(nameof(YAxisMaxLabel));
                    break;
                case nameof(ITwoAxisDiagram.YAxisMinLabel):
                    OnPropertyChanged(nameof(YAxisMinLabel));
                    break;
                case nameof(ITwoAxisDiagram.XAxisMaxLabel):
                    OnPropertyChanged(nameof(XAxisMaxLabel));
                    break;
                case nameof(ITwoAxisDiagram.XAxisMinLabel):
                    OnPropertyChanged(nameof(XAxisMinLabel));
                    break;
                case nameof(ITwoAxisDiagram.AxisFontFamily):
                    OnPropertyChanged(nameof(AxisFontFamily));
                    break;
                case nameof(ITwoAxisDiagram.AxisFontColor):
                    OnPropertyChanged(nameof(AxisFontColor));
                    break;
                case nameof(ITwoAxisDiagram.AxisFontBold):
                    OnPropertyChanged(nameof(AxisFontWeight));
                    break;
                case nameof(ITwoAxisDiagram.AxisFontItalic):
                    OnPropertyChanged(nameof(AxisFontStyle));
                    break;
                case nameof(ITwoAxisDiagram.AxisFontSize):
                    OnPropertyChanged(nameof(AxisFontSize));
                    break;
                case nameof(ITwoAxisDiagram.Name):
                    OnPropertyChanged(nameof(DisplayName));
                    break;
            }
        }
    }
}