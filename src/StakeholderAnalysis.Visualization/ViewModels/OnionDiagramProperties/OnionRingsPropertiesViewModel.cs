using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.OnionDiagramProperties
{
    public class OnionRingsPropertiesViewModel : NotifyPropertyChangedObservable, IExpandableContentViewModel
    {
        private readonly ViewManager viewManager;
        private bool isExpanded;

        public OnionRingsPropertiesViewModel(ViewManager viewManager)
        {
            this.viewManager = viewManager;
            viewManager.PropertyChanged += ViewManagerPropertyChanged;
            SetActiveOnionDiagram();
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    SetActiveOnionDiagram();
                    break;
            }
        }

        public string DisplayName => "Ringen";

        private OnionDiagram SelectedOnionDiagram { get; set; }

        public ObservableCollection<OnionRingPropertiesViewModel> OnionRings { get; private set; }

        public ICommand ToggleIsExpandedCommand => new ToggleIsExpandedCommand(this);

        public ICommand AddNewRingCommand => new AddOnionRingCommand(SelectedOnionDiagram);

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        private void SetActiveOnionDiagram()
        {
            var activeOnionDiagram = (viewManager?.ActiveDocument?.ViewModel as OnionDiagramViewModel)?.GetDiagram();
            if (SelectedOnionDiagram != activeOnionDiagram)
            {
                if (SelectedOnionDiagram != null)
                {
                    SelectedOnionDiagram.OnionRings.CollectionChanged += OnionRingsCollectionChanged;
                }

                SelectedOnionDiagram = activeOnionDiagram;
                OnionRings = new ObservableCollection<OnionRingPropertiesViewModel>(
                    SelectedOnionDiagram?.OnionRings.Select(r => new OnionRingPropertiesViewModel(r, SelectedOnionDiagram)) ??
                    new List<OnionRingPropertiesViewModel>());

                if (SelectedOnionDiagram != null)
                {
                    SelectedOnionDiagram.OnionRings.CollectionChanged += OnionRingsCollectionChanged;
                }

                OnPropertyChanged(nameof(OnionRings));
            }
        }

        private void OnionRingsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var onionRing in e.NewItems.OfType<OnionRing>())
                    {
                        OnionRings.Add(new OnionRingPropertiesViewModel(onionRing, SelectedOnionDiagram));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var ringViewModel in e.OldItems.OfType<OnionRing>())
                    {
                        var viewModel = OnionRings.FirstOrDefault(vm => vm.IsViewModelFor(ringViewModel));
                        if (viewModel != null)
                        {
                            OnionRings.Remove(viewModel);
                        }
                    }
                    break;
            }
        }
    }
}
