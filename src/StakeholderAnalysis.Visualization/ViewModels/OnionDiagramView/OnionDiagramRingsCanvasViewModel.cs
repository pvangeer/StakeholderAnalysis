using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView
{
    public class OnionDiagramRingsCanvasViewModel : ViewModelBase
    {
        private readonly OnionDiagram onionDiagram;

        public OnionDiagramRingsCanvasViewModel(ViewModelFactory factory, OnionDiagram diagram) : base(factory)
        {
            this.onionDiagram = diagram;
            if (diagram != null)
            {
                onionDiagram.OnionRings.CollectionChanged += RingsCollectionChanged;
                onionDiagram.PropertyChanged += OnionDiagramPropertyChanged;
            }
            OnionRings = new ObservableCollection<OnionRingViewModel>(onionDiagram.OnionRings.Select(r => ViewModelFactory.CreateOnionRingViewModel(r)));
        }

        private void OnionDiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OnionDiagram.Asymmetry):
                    OnPropertyChanged(nameof(Asymmetry));
                    break;
            }
        }

        public ObservableCollection<OnionRingViewModel> OnionRings { get; }

        public double Asymmetry
        {
            get => onionDiagram?.Asymmetry ?? double.NaN;
            set => onionDiagram.Asymmetry = value;
        }

        private void RingsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var onionRing in e.NewItems.OfType<OnionRing>())
                {
                    OnionRings.Add(ViewModelFactory.CreateOnionRingViewModel(onionRing));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var onionRing in e.OldItems.OfType<OnionRing>())
                    OnionRings.Remove(OnionRings.FirstOrDefault(r => r.Ring == onionRing));
        }
    }
}
