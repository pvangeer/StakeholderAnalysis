using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.Properties.TwoAxisDiagramProperties;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.TwoAxisDiagrams
{
    // TODO: Possibly merge with AttitudeImpactDiagramViewModel
    public class ForceFieldDiagramViewModel : ViewModelBase, ITwoAxisDiagramViewModel, ISelectable, IDiagramViewModel
    {
        private readonly ForceFieldDiagram diagram;
        private object selectedObject;

        public ForceFieldDiagramViewModel(ViewModelFactory factory, ForceFieldDiagram diagram) : base(factory)
        {
            this.diagram = diagram;

            if (diagram != null)
            {
                diagram.PropertyChanged += DiagramPropertyChanged;
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(
                    diagram.Stakeholders.Select(stakeholder =>
                        ViewModelFactory.CreateForceFieldDiagramStakeholderViewModel(diagram, stakeholder, this)));
            }
        }

        public bool CanSelect => true;

        public bool IsSelected { get; set; }

        public ICommand SelectItemCommand => null;

        public object GetSelectableObject()
        {
            return diagram;
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        public Brush BackgroundBrush => new LinearGradientBrush(diagram.BrushStartColor, diagram.BrushEndColor,
            new Point(0, 1), new Point(1, 0));

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

        public bool IsSelectedObject(object o)
        {
            return selectedObject == o;
        }

        public void SelectObject(object o)
        {
            selectedObject = o;
            foreach (var stakeholderViewModel in PositionedStakeholders.OfType<DiagramStakeholderViewModelBase>())
                stakeholderViewModel.OnPropertyChanged(nameof(DiagramStakeholderViewModelBase.IsSelectedStakeholder));
        }

        public IStakeholderDiagram GetDiagram()
        {
            return diagram;
        }

        public TwoAxisDiagramPropertiesViewModel GetPropertiesViewModel()
        {
            return ViewModelFactory.CreateTwoAxisDiagramPropertiesViewModel(diagram);
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

        public bool IsViewModelFor(ForceFieldDiagram otherDiagram)
        {
            return otherDiagram == diagram;
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<ForceFieldDiagramStakeholder>())
                    PositionedStakeholders.Add(
                        ViewModelFactory.CreateForceFieldDiagramStakeholderViewModel(diagram, item, this));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<ForceFieldDiagramStakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder.Stakeholder)));
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ForceFieldDiagram.BrushStartColor):
                case nameof(ForceFieldDiagram.BrushEndColor):
                    OnPropertyChanged(nameof(BackgroundBrush));
                    break;
                case nameof(ForceFieldDiagram.BackgroundTextLeftTop):
                    OnPropertyChanged(nameof(BackgroundTextLeftTop));
                    break;
                case nameof(ForceFieldDiagram.BackgroundTextRightTop):
                    OnPropertyChanged(nameof(BackgroundTextRightTop));
                    break;
                case nameof(ForceFieldDiagram.BackgroundTextLeftBottom):
                    OnPropertyChanged(nameof(BackgroundTextLeftBottom));
                    break;
                case nameof(ForceFieldDiagram.BackgroundTextRightBottom):
                    OnPropertyChanged(nameof(BackgroundTextRightBottom));
                    break;
                case nameof(ForceFieldDiagram.BackgroundFontFamily):
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
                case nameof(ForceFieldDiagram.YAxisMaxLabel):
                    OnPropertyChanged(nameof(YAxisMaxLabel));
                    break;
                case nameof(ForceFieldDiagram.YAxisMinLabel):
                    OnPropertyChanged(nameof(YAxisMinLabel));
                    break;
                case nameof(ForceFieldDiagram.XAxisMaxLabel):
                    OnPropertyChanged(nameof(XAxisMaxLabel));
                    break;
                case nameof(ForceFieldDiagram.XAxisMinLabel):
                    OnPropertyChanged(nameof(XAxisMinLabel));
                    break;
                case nameof(ForceFieldDiagram.AxisFontFamily):
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

        public override bool IsViewModelFor(object o)
        {
            return o == diagram;
        }
    }
}