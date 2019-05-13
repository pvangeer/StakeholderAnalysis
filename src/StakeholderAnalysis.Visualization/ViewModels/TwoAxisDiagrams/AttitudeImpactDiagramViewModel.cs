using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class AttitudeImpactDiagramViewModel : ITwoAxisDiagramViewModel
    {
        private AttitudeImpactDiagram diagram;

        public AttitudeImpactDiagramViewModel(AttitudeImpactDiagram attitudeImpactDiagram)
        {
            diagram = attitudeImpactDiagram;
            if (attitudeImpactDiagram != null)
            {
                attitudeImpactDiagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(attitudeImpactDiagram.Stakeholders.Select(stakeholder => new AttitudeImpactDiagramStakeholderViewModel(stakeholder)));
            }
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<AttitudeImpactDiagramStakeholder>())
                    PositionedStakeholders.Add(new AttitudeImpactDiagramStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<AttitudeImpactDiagramStakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder.Stakeholder)));
        }

        public Brush BackgroundBrush => new LinearGradientBrush(Colors.LightYellow, Colors.PaleVioletRed, new Point(0,0), new Point(1,1));

        public string BackgroundTextLeftTop => "Informeren";

        public string BackgroundTextRightTop => "Betrekken";

        public string BackgroundTextLeftBottom => "Monitoren";

        public string BackgroundTextRightBottom => "Overtuigen";

        public string YAxisMaxLabel => "Positief";

        public string YAxisMinLabel => "Negatief";

        public string XAxisMaxLabel => "Hoge impact";

        public string XAxisMinLabel => "Lage impact";

        public bool IsViewModelFor(AttitudeImpactDiagram otherDiagram)
        {
            return otherDiagram == diagram;
        }
    }
}