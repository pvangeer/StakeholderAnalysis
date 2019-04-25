using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;

namespace StakeholderAnalysis.Visualization.ViewModels.Ribbon
{
    public class RibbonViewModel : NotifyPropertyChangedObservable
    {
        private Gui.Gui gui;
        private Analysis analysis; // In future needed to save project data
        private RelayCommand saveCanvasCommand;

        public RibbonViewModel(Analysis analysisInput, Gui.Gui guiInput)
        {
            gui = guiInput;
            analysis = analysisInput;
            gui.ViewManager.PropertyChanged += ViewManagerPropertyChanged;
            gui.PropertyChanged += GuiPropertyChanged;
        }

        public ICommand OpenCommand => new OpenFileCommand(this);

        public ICommand SaveCommand => new SaveFileCommand(this);

        public ICommand SaveAsCommand => new SaveFileAsCommand(this);

        public ICommand NewCommand => new NewProjectCommand(this);

        public ICommand CloseApplication => new CloseApplicationCommand();

        public RibbonStakeholderConnectionGroupsViewModel RibbonStakeholderConnectionGroupsViewModel => new RibbonStakeholderConnectionGroupsViewModel(gui.ViewManager);

        public ViewInfo ActiveDocument
        {
            get => gui.ViewManager.ActiveDocument;
            set => gui.ViewManager.ActiveDocument = value;
        }

        public bool IsMagnifierActive
        {
            get => gui.IsMagnifierActive;
            set
            {
                gui.IsMagnifierActive = value;
                gui.OnPropertyChanged(nameof(Gui.Gui.IsMagnifierActive));
            }
        }

        public ICommand SaveImageCommand
        {
            get
            {
                return saveCanvasCommand ?? (saveCanvasCommand = new RelayCommand(() =>
                {
                    gui.IsSaveToImage = true;
                    gui.OnPropertyChanged(nameof(Gui.Gui.IsSaveToImage));
                    gui.IsSaveToImage = false;
                    gui.OnPropertyChanged(nameof(Gui.Gui.IsSaveToImage));
                }));
            }
        }

        private void ViewManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewManager.ActiveDocument):
                    OnPropertyChanged(nameof(ActiveDocument));
                    break;
            }
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Gui.Gui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
            }
        }
    }
}
