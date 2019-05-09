using System.ComponentModel;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainContentPresenterViewModel : NotifyPropertyChangedObservable
    {
        private bool isSaveToImage;
        private RelayCommand saveCanvasCommand;
        private readonly Gui.Gui gui;

        public MainContentPresenterViewModel(Gui.Gui gui)
        {
            this.gui = gui;
            if (gui != null)
            {
                gui.PropertyChanged += GuiPropertyChanged;
            }
        }

        private void GuiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Gui.Gui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
                case nameof(Gui.Gui.IsSaveToImage):
                    OnPropertyChanged(nameof(IsSaveToImage));
                    break;
            }
        }

        // TODO: this property should be wrapped by a viewmodel
        public ViewManager ViewManager => gui.ViewManager;

        public bool IsSaveToImage
        {
            get => gui.IsSaveToImage;
            set
            {
                gui.IsSaveToImage = value;
                gui.OnPropertyChanged(nameof(gui.IsSaveToImage));
            }
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
    }
}
