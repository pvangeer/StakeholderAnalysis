using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.OnionDiagramView
{
    public class OnionDiagramRingsCanvasViewModel : ViewModelBase
    {
        private readonly OnionDiagram onionDiagram;

        public OnionDiagramRingsCanvasViewModel(ViewModelFactory factory, OnionDiagram diagram) : base(factory)
        {
            onionDiagram = diagram;
            if (diagram != null)
            {
                onionDiagram.OnionRings.CollectionChanged += RingsCollectionChanged;
                onionDiagram.PropertyChanged += OnionDiagramPropertyChanged;
            }

            OnionRings =
                new ObservableCollection<OnionRingViewModel>(
                    onionDiagram.OnionRings.Select(r => ViewModelFactory.CreateOnionRingViewModel(r)));
        }

        public ObservableCollection<OnionRingViewModel> OnionRings { get; }

        public double Asymmetry
        {
            get => onionDiagram?.Asymmetry ?? double.NaN;
            set => onionDiagram.Asymmetry = value;
        }

        public double Orientation
        {
            get => onionDiagram?.Orientation ?? double.NaN;
            set => onionDiagram.Orientation = value;
        }

        private void OnionDiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Asymmetry):
                    OnPropertyChanged(nameof(Asymmetry));
                    break;
                case nameof(OnionDiagram.Orientation):
                    OnPropertyChanged(nameof(Orientation));
                    break;
            }
        }

        private void RingsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var onionRing in e.NewItems.OfType<OnionRing>())
                    OnionRings.Add(ViewModelFactory.CreateOnionRingViewModel(onionRing));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var onionRing in e.OldItems.OfType<OnionRing>())
                    OnionRings.Remove(OnionRings.FirstOrDefault(r => r.Ring == onionRing));
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}