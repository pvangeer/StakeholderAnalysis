using System.ComponentModel;
using System.Linq;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainContentPresenterViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;

        public MainContentPresenterViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            this.gui = gui;
            if (gui != null) gui.PropertyChanged += GuiPropertyChanged;
            ViewManager = ViewModelFactory.CreateViewManagerViewModel();
            ProjectExplorerViewModel = ViewModelFactory.CreateProjectExplorerViewModel();
        }

        public ViewManagerViewModel ViewManager { get; }

        public ProjectExplorerViewModel ProjectExplorerViewModel { get; }
        
        public bool IsSaveToImage
        {
            get => gui.IsSaveToImage;
            set
            {
                gui.IsSaveToImage = value;
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
                    foreach (var viewInfo in gui.ViewManager.Views.ToList()) gui.ViewManager.CloseView(viewInfo);
                    break;
            }
        }
    }
}