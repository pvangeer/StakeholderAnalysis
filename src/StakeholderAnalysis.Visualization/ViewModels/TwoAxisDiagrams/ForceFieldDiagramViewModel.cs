using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.ForceFieldDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public class ForceFieldDiagramViewModel : ITwoAxisDiagramViewModel
    {
        public ForceFieldDiagramViewModel(ForceFieldDiagram diagram)
        {
            if (diagram != null)
            {
                diagram.Stakeholders.CollectionChanged += StakeholdersCollectionChanged;
                PositionedStakeholders = new ObservableCollection<IPositionedStakeholderViewModel>(diagram.Stakeholders.Select(stakeholder => new ForceFieldDiagramStakeholderViewModel(stakeholder)));
            }
        }

        public ObservableCollection<IPositionedStakeholderViewModel> PositionedStakeholders { get; }

        private void StakeholdersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems.OfType<Stakeholder>())
                    PositionedStakeholders.Add(new ForceFieldDiagramStakeholderViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var stakeholder in e.OldItems.OfType<Stakeholder>())
                    PositionedStakeholders.Remove(PositionedStakeholders.FirstOrDefault(viewModel =>
                        viewModel.IsViewModelFor(stakeholder)));
        }

        public Brush BackgroundBrush => new LinearGradientBrush(Colors.CornflowerBlue, Colors.LimeGreen, new Point(0,1), new Point(1,0));

        public string BackgroundTextLeftTop => "Tevreden houden";

        public string BackgroundTextRightTop => "Betrekken";

        public string BackgroundTextLeftBottom => "Monitoren";

        public string BackgroundTextRightBottom => "Informeren";
        
        public string YAxisMaxLabel { get; }
        public string YAxisMinLabel { get; }
        public string XAxisMaxLabel { get; }
        public string XAxisMinLabel { get; }
    }
}