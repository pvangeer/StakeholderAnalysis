using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using Xceed.Wpf.Toolkit;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainContentPresenterViewModel : ViewModelBase
    {
        private readonly StakeholderAnalysisGui gui;

        public MainContentPresenterViewModel(ViewModelFactory factory, StakeholderAnalysisGui gui) : base(factory)
        {
            this.gui = gui;
            if (gui != null)
                gui.PropertyChanged += GuiPropertyChanged;
            ViewManager = ViewModelFactory.CreateViewManagerViewModel();
            SelectionManager = gui?.SelectionManager;
            ProjectExplorerViewModel = ViewModelFactory.CreateProjectExplorerViewModel();
            StandardColorPallet = new ObservableCollection<ColorItem>
            {
                new ColorItem((Color)ColorConverter.ConvertFromString("#000000"), "Zwart"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#FFFFFF"), "Wit"),

                new ColorItem((Color)ColorConverter.ConvertFromString("#080C80"), "Deltares - Donkerblauw"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#0D38E0"), "Deltares - Blauw"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#0EBBF0"), "Deltares - Lichtblauw"),

                new ColorItem((Color)ColorConverter.ConvertFromString("#00B389"), "Deltares - Donkergroen"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#00CC96"), "Deltares - Groen"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#00E6A1"), "Deltares - Lichtgroen"),

                new ColorItem((Color)ColorConverter.ConvertFromString("#F2F2F2"), "Deltares - Lichtgrijs"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#E6E6E6"), "Deltares - Middengrijs"),

                new ColorItem((Color)ColorConverter.ConvertFromString("#FFD814"), "Deltares - Geel"),
                new ColorItem((Color)ColorConverter.ConvertFromString("#FF960D"), "Deltares - Academy")
            };
        }

        public ViewManagerViewModel ViewManager { get; }

        public SelectionManager SelectionManager { get; }

        public ViewModelFactory PropertiesViewModelFactory => ViewModelFactory;

        public ProjectExplorerViewModel ProjectExplorerViewModel { get; private set; }

        public ObservableCollection<ColorItem> StandardColorPallet { get; }

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
                case nameof(StakeholderAnalysisGui.IsProjectExplorerVisible):
                    OnPropertyChanged(nameof(IsProjectExplorerVisible));
                    break;
                case nameof(StakeholderAnalysisGui.IsPropertiesVisible):
                    OnPropertyChanged(nameof(IsPropertiesVisible));
                    break;
                case nameof(StakeholderAnalysisGui.Analysis):
                    gui.SelectionManager.Select(null);
                    foreach (var viewInfo in gui.ViewManager.Views.ToList())
                        gui.ViewManager.CloseView(viewInfo);
                    ProjectExplorerViewModel = ViewModelFactory.CreateProjectExplorerViewModel();
                    OnPropertyChanged(nameof(ProjectExplorerViewModel));
                    break;
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return false;
        }
    }
}