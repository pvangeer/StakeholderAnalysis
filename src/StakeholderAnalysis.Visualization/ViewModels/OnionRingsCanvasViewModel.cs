﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class OnionRingsCanvasViewModel : NotifyPropertyChangedObservable
    {
        private readonly OnionDiagram onionDiagram;

        public OnionRingsCanvasViewModel() : this(null){ }

        public OnionRingsCanvasViewModel(OnionDiagram diagram)
        {
            this.onionDiagram = diagram;
            if (diagram != null)
            {
                onionDiagram.OnionRings.CollectionChanged += RingsCollectionChanged;
                onionDiagram.PropertyChanged += OnionDiagramPropertyChanged;
            }
            OnionRings = new ObservableCollection<OnionRingViewModel>(onionDiagram.OnionRings.Select(r => new OnionRingViewModel(r)));
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
                foreach (var item in e.NewItems.OfType<OnionRing>())
                    OnionRings.Insert(
                        OnionRings.IndexOf(OnionRings.FirstOrDefault(r => r.Percentage >= item.Percentage)),
                        new OnionRingViewModel(item));

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var onionRing in e.OldItems.OfType<OnionRing>())
                    OnionRings.Remove(OnionRings.FirstOrDefault(r => r.Ring == onionRing));
        }
    }
}
