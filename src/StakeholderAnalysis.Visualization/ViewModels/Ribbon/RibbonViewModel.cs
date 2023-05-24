using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using StakeholderAnalysis.Data.OnionDiagrams;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;
        private RelayCommand saveCanvasCommand;
        private StakeholderConnectionGroupSelection stakeholderConnectionGroupSelection;

        public RibbonViewModel(ViewModelFactory factory, StakeholderAnalysisGui guiInput) : base(factory)
        {
            gui = guiInput;
            if (gui != null)
            {
                gui.ShouldSaveOpenChanges = ShouldSaveOpenChanges;
                gui.ShouldMigrateProject = ShouldMigrateProject;
                gui.PropertyChanged += GuiPropertyChanged;
                gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
                gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
                SetCurrentSelectedDiagramAndGroups();
            }

            ViewManagerViewModel = ViewModelFactory.CreateViewManagerViewModel();
        }

        public ViewManagerViewModel ViewManagerViewModel { get; }

        public ICommand OpenCommand => CommandFactory.CreateOpenFileCommand();

        public ICommand SaveCommand => CommandFactory.CreateSaveFileCommand();

        public ICommand SaveAsCommand => CommandFactory.CreateSaveFileAsCommand();

        public ICommand NewCommand => CommandFactory.CreateNewProjectCommand();

        public ICommand CloseApplication => CommandFactory.CreateCloseApplicationCommand();

        public ICommand AddStakeholdersCommand => CommandFactory.CreateAddStakeholdersCommand();

        public bool IsProjectExplorerVisible
        {
            get => gui.IsProjectExplorerVisible;
            set
            {
                gui.IsProjectExplorerVisible = value;
                gui.OnPropertyChanged();
            }
        }

        public bool IsPropertiesVisible
        {
            get => gui.IsPropertiesVisible;
            set
            {
                gui.IsPropertiesVisible = value;
                gui.OnPropertyChanged();
            }
        }

        public bool IsMagnifierActive
        {
            get => gui.IsMagnifierActive;
            set
            {
                gui.IsMagnifierActive = value;
                gui.OnPropertyChanged();
            }
        }

        public ICommand SaveImageCommand =>
            saveCanvasCommand ?? (saveCanvasCommand = CommandFactory.CreateSaveImageCommand());

        public ICommand AddOnionRingCommand =>
            CommandFactory.CreateAddOnionRingCommand(stakeholderConnectionGroupSelection?.OnionDiagram);

        public double Asymmetry
        {
            get => stakeholderConnectionGroupSelection?.OnionDiagram?.Asymmetry ?? 0.0;
            set
            {
                if (stakeholderConnectionGroupSelection?.OnionDiagram != null)
                {
                    stakeholderConnectionGroupSelection.OnionDiagram.Asymmetry = value;
                    stakeholderConnectionGroupSelection.OnionDiagram.OnPropertyChanged();
                }
            }
        }

        public double Orientation
        {
            get => stakeholderConnectionGroupSelection?.OnionDiagram?.Orientation ?? 180.0;
            set
            {
                if (stakeholderConnectionGroupSelection?.OnionDiagram != null)
                {
                    stakeholderConnectionGroupSelection.OnionDiagram.Orientation = value;
                    stakeholderConnectionGroupSelection.OnionDiagram.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<StakeholderConnectionGroup> StakeholderConnectionGroups =>
            stakeholderConnectionGroupSelection?.OnionDiagram?.ConnectionGroups;

        public StakeholderConnectionGroup SelectedStakeholderConnectionGroup
        {
            get => stakeholderConnectionGroupSelection?.StakeholderConnectionGroup;
            set
            {
                if (stakeholderConnectionGroupSelection == null || value == null) return;

                stakeholderConnectionGroupSelection.StakeholderConnectionGroup = value;
                stakeholderConnectionGroupSelection.OnPropertyChanged(nameof(StakeholderConnectionGroupSelection
                    .StakeholderConnectionGroup));
            }
        }

        private void SetCurrentSelectedDiagramAndGroups()
        {
            var selectedOnionDiagram = gui?.ViewManager?.ActiveDocument?.ViewModel is OnionDiagramViewModel viewModel
                ? viewModel.GetDiagram()
                : null;

            if (stakeholderConnectionGroupSelection != null)
            {
                stakeholderConnectionGroupSelection.PropertyChanged -=
                    StakeholderConnectionGroupSelectionPropertyChanged;
                stakeholderConnectionGroupSelection.OnionDiagram.PropertyChanged += OnionDiagramPropertyChanged;
            }

            stakeholderConnectionGroupSelection = null;
            OnPropertyChanged(nameof(StakeholderConnectionGroups));
            OnPropertyChanged(nameof(SelectedStakeholderConnectionGroup));

            stakeholderConnectionGroupSelection =
                gui?.SelectedStakeholderConnectionGroups.FirstOrDefault(g => g.OnionDiagram == selectedOnionDiagram);

            if (stakeholderConnectionGroupSelection != null)
            {
                stakeholderConnectionGroupSelection.PropertyChanged +=
                    StakeholderConnectionGroupSelectionPropertyChanged;
                stakeholderConnectionGroupSelection.OnionDiagram.PropertyChanged += OnionDiagramPropertyChanged;
            }

            OnPropertyChanged(nameof(Asymmetry));
            OnPropertyChanged(nameof(Orientation));
            OnPropertyChanged(nameof(AddOnionRingCommand));
            OnPropertyChanged(nameof(StakeholderConnectionGroups));
            OnPropertyChanged(nameof(SelectedStakeholderConnectionGroup));
        }

        private void OnionDiagramPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OnionDiagram.Asymmetry))
                OnPropertyChanged(nameof(Asymmetry));
            if (e.PropertyName == nameof(OnionDiagram.Orientation))
                OnPropertyChanged(nameof(Orientation));
        }

        private void StakeholderConnectionGroupSelectionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StakeholderConnectionGroupSelection.StakeholderConnectionGroup))
                OnPropertyChanged(nameof(SelectedStakeholderConnectionGroup));
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(StakeholderAnalysisGui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
                case nameof(StakeholderAnalysisGui.Analysis):
                    SetCurrentSelectedDiagramAndGroups();
                    OnPropertyChanged(nameof(AddStakeholdersCommand));
                    break;
                case nameof(StakeholderAnalysisGui.IsProjectExplorerVisible):
                    OnPropertyChanged(nameof(IsProjectExplorerVisible));
                    break;
                case nameof(StakeholderAnalysisGui.IsPropertiesVisible):
                    OnPropertyChanged(nameof(IsPropertiesVisible));
                    break;
            }
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(gui.ViewManager.ActiveDocument):
                    SetCurrentSelectedDiagramAndGroups();
                    break;
            }
        }

        private bool ShouldSaveOpenChanges()
        {
            var messageBoxText = "U heeft aanpassingen aan uw project nog niet opgeslagen. Wilt u dat alsnog doen?";
            var caption = "Aanpassingen opslaan";
            var messageBoxResult =
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            return messageBoxResult == MessageBoxResult.Yes;
        }

        private bool ShouldMigrateProject()
        {
            var messageBoxText =
                "U wilt een verouderd bestand openen. Wilt u dit bestand migreren naar het nieuwe format om het te kunnen openen?";
            var caption = "Bestand migreren naar nieuwste versie";
            var messageBoxResult =
                MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            return messageBoxResult == MessageBoxResult.Yes;
        }
    }
}