using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Gui;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainContentPresenterViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;

        public MainContentPresenterViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            this.gui = gui;
            if (gui != null)
            {
                gui.PropertyChanged += GuiPropertyChanged;
            }
            ViewManager = ViewModelFactory.CreateViewManagerViewModel(gui?.ViewManager);
        }

        public ViewManagerViewModel ViewManager { get; }

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
    }
}
