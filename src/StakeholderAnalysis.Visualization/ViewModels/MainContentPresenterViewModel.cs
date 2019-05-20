using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainContentPresenterViewModel : NotifyPropertyChangedObservable
    {
        private bool isSaveToImage;
        private RelayCommand saveCanvasCommand;
        private readonly StakeholderAnalysisGui gui;

        public MainContentPresenterViewModel(StakeholderAnalysisGui gui)
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
                case nameof(StakeholderAnalysisGui.IsMagnifierActive):
                    OnPropertyChanged(nameof(IsMagnifierActive));
                    break;
                case nameof(StakeholderAnalysisGui.IsSaveToImage):
                    OnPropertyChanged(nameof(IsSaveToImage));
                    break;
                case nameof(StakeholderAnalysisGui.Analysis):
                    foreach (var viewInfo in gui.ViewManager.Views.ToList())
                    {
                        gui.ViewManager.CloseView(viewInfo);
                    }
                    break;
            }
        }

        public ViewManagerViewModel ViewManager => new ViewManagerViewModel(gui.ViewManager);

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
                gui.OnPropertyChanged(nameof(StakeholderAnalysisGui.IsMagnifierActive));
            }
        }
    }
}
