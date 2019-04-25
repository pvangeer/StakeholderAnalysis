using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Visualization.DataTemplates.TwoAxisDiagrams;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramView
{
    public class AttitudeImpactDiagramViewModel : ITwoAxisDiagramViewModel
    {
        public AttitudeImpactDiagramViewModel(AttitudeImpactDiagram attitudeImpactDiagram)
        {
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
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    PositionedStakeholders.Add(new AttitudeImpactDiagramStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
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
    }
}