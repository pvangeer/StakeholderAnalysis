namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewInfo
    {
        public StakeholderViewInfo(StakeholderViewType type, MainWindowViewModel viewModel)
        {
            Type = type;
            ViewModel = viewModel;
        }

        public StakeholderViewType Type { get; }

        public MainWindowViewModel ViewModel { get; }
    }
}