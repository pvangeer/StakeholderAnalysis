using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramViewModel : ViewModelBase, ITwoAxisDiagramViewModel
    {
        private AttitudeImpactDiagram diagram;
        private object selectedObject;

        public AttitudeImpactDiagramViewModel(ViewModelFactory factory, AttitudeImpactDiagram attitudeImpactDiagram) : base(factory)
        {
            diagram = attitudeImpactDiagram;
            if (attitudeImpactDiagram != null)
            {
                diagram.PropertyChanged += DiagramPropertyChanged;
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(attitudeImpactDiagram.Stakeholders.Select(stakeholder => ViewModelFactory.CreateAttitudeImpactDiagramStakeholderViewModel(diagram, stakeholder, this)));
            }
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<AttitudeImpactDiagramStakeholder>())
                    PositionedStakeholders.Add(ViewModelFactory.CreateAttitudeImpactDiagramStakeholderViewModel(diagram, item, this));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<AttitudeImpactDiagramStakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder.Stakeholder)));
        }

        public Brush BackgroundBrush => new LinearGradientBrush(diagram.BrushStartColor, diagram.BrushEndColor, new Point(0,0), new Point(1,1));

        public string BackgroundTextLeftTop => diagram.BackgroundTextLeftTop;

        public string BackgroundTextRightTop => diagram.BackgroundTextRightTop;

        public string BackgroundTextLeftBottom => diagram.BackgroundTextLeftBottom;

        public string BackgroundTextRightBottom => diagram.BackgroundTextRightBottom;

        public string YAxisMaxLabel => diagram.YAxisMaxLabel;

        public string YAxisMinLabel => diagram.YAxisMinLabel;

        public string XAxisMaxLabel => diagram.XAxisMaxLabel;

        public string XAxisMinLabel => diagram.XAxisMinLabel;

        public ICommand GridClickedCommand => CommandFactory.CreateClearSelectionCommand(this);

        public bool IsViewModelFor(AttitudeImpactDiagram otherDiagram)
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
            foreach (var stakeholder in PositionedStakeholders.OfType<StakeholderViewModel>())
            {
                stakeholder.OnPropertyChanged(nameof(StakeholderViewModel.IsSelectedStakeholder));
            }
        }

        public ITwoAxisDiagram GetDiagram()
        {
            return diagram;
        }

        private void DiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(AttitudeImpactDiagram.BrushStartColor):
                case nameof(AttitudeImpactDiagram.BrushEndColor):
                    OnPropertyChanged(nameof(BackgroundBrush));
                    break;
                case nameof(AttitudeImpactDiagram.BackgroundTextLeftTop):
                    OnPropertyChanged(nameof(BackgroundTextLeftTop));
                    break;
                case nameof(AttitudeImpactDiagram.BackgroundTextRightTop):
                    OnPropertyChanged(nameof(BackgroundTextRightTop));
                    break;
                case nameof(AttitudeImpactDiagram.BackgroundTextLeftBottom):
                    OnPropertyChanged(nameof(BackgroundTextLeftBottom));
                    break;
                case nameof(AttitudeImpactDiagram.BackgroundTextRightBottom):
                    OnPropertyChanged(nameof(BackgroundTextRightBottom));
                    break;
                case nameof(AttitudeImpactDiagram.YAxisMaxLabel):
                    OnPropertyChanged(nameof(YAxisMaxLabel));
                    break;
                case nameof(AttitudeImpactDiagram.YAxisMinLabel):
                    OnPropertyChanged(nameof(YAxisMinLabel));
                    break;
                case nameof(AttitudeImpactDiagram.XAxisMaxLabel):
                    OnPropertyChanged(nameof(XAxisMaxLabel));
                    break;
                case nameof(AttitudeImpactDiagram.XAxisMinLabel):
                    OnPropertyChanged(nameof(XAxisMinLabel));
                    break;
            }
        }
    }
}