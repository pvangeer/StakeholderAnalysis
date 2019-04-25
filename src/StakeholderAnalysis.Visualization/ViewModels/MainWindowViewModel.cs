using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Gui;
using StakeholderAnalysis.Visualization.Commands;
using StakeholderAnalysis.Visualization.Commands.FileHandling;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;
using StakeholderAnalysis.Visualization.ViewModels.ProjectExplorer;
using StakeholderAnalysis.Visualization.ViewModels.Ribbon;
using StakeholderAnalysis.Visualization.ViewModels.StakeholderTableView;
using StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedObservable
    {
        private readonly Analysis analysis;
        private readonly Gui.Gui gui;

        public MainWindowViewModel() : this(new Analysis(), new Gui.Gui()){ }

        public MainWindowViewModel(Analysis analysisInput, Gui.Gui guiInput)
        {
            analysis = analysisInput;
            gui = guiInput;

            foreach (var forceFieldDiagram in analysis.ForceFieldDiagrams)
            {
                gui.ViewManager.OpenView(new ViewInfo(forceFieldDiagram.Name, new ForceFieldDiagramViewModel(forceFieldDiagram), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/forces.png"));
            }
            foreach (var attitudeImpactDiagram in analysis.AttitudeImpactDiagrams)
            {
                gui.ViewManager.OpenView(new ViewInfo(attitudeImpactDiagram.Name, new AttitudeImpactDiagramViewModel(attitudeImpactDiagram), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/involvement.png"));
            }
            foreach (var onionDiagram in analysis.OnionDiagrams)
            {
                gui.ViewManager.OpenView(new ViewInfo(onionDiagram.Name, new OnionDiagramViewModel(onionDiagram), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/onion.png"));
            }
            gui.ViewManager.OpenView(new ViewInfo("Tabel", new StakeholderTableViewModel(analysis), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/table.png"));
            gui.ViewManager.OpenToolWindow(new ToolWindowViewInfo("Projectgegevens", new ProjectExplorerViewModel(analysis, gui.ViewManager), "pack://application:,,,/StakeholderAnalysis.Visualization;component/Resources/SaveImage.png"));
            MainContentPresenterViewModel = new MainContentPresenterViewModel(gui);
        }

        public MainContentPresenterViewModel MainContentPresenterViewModel { get; }
    
        public RibbonViewModel RibbonViewModel => new RibbonViewModel(analysis,gui);
    }
}