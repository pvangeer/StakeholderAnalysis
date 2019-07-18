using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    // TODO: Possibly merge with AttitudeImpactDiagramViewModel
    public class ForceFieldDiagramViewModel : ViewModelBase, ITwoAxisDiagramViewModel
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
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => ViewModelFactory.CreateForceFieldDiagramStakeholderViewModel(diagram, stakeholder, this)));
            }
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }


        // TODO: Retrieve and store this property on the datamodel and make it editable

        public Brush BackgroundBrush => new LinearGradientBrush(diagram.BrushStartColor, diagram.BrushEndColor, new Point(0, 1), new Point(1, 0));

        public string BackgroundTextLeftTop => diagram.BackgroundTextLeftTop;

        public string BackgroundTextRightTop => diagram.BackgroundTextRightTop;

        public string BackgroundTextLeftBottom => diagram.BackgroundTextLeftBottom;

        public string BackgroundTextRightBottom => diagram.BackgroundTextRightBottom;

        public string YAxisMaxLabel => diagram.YAxisMaxLabel;

        public string YAxisMinLabel => diagram.YAxisMinLabel;

        public string XAxisMaxLabel => diagram.XAxisMaxLabel;

        public string XAxisMinLabel => diagram.XAxisMinLabel;

        public ICommand GridClickedCommand => CommandFactory.CreateClearSelectionCommand(this);

        public bool IsViewModelFor(ForceFieldDiagram otherDiagram)
        {
            return otherDiagram == diagram;
        }

        public bool IsSelected(object o)
        {
            return selectedObject == o;
        }

        public void Select(object o)
        {
            selectedObject = o;
            foreach (var stakeholderViewModel in PositionedStakeholders.OfType<StakeholderViewModel>())
            {
                stakeholderViewModel.OnPropertyChanged(nameof(StakeholderViewModel.IsSelectedStakeholder));
            }
        }

        public ITwoAxisDiagram GetDiagram()
        {
            return diagram;
        }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<ForceFieldDiagramStakeholder>())
                    PositionedStakeholders.Add(ViewModelFactory.CreateForceFieldDiagramStakeholderViewModel(diagram, item, this));

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
            }
        }
    }
}