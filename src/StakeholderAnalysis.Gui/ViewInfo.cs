namespace StakeholderAnalysis.Gui
{
    public class ViewInfo : IViewInfo
    {
        public ViewInfo(string title, object viewModel)
        {
            Title = title;
            ViewModel = viewModel;
        }

        public string Title { get; }

        public object ViewModel { get; }
    }

    public class ToolWindowViewInfo : ViewInfo {
        public ToolWindowViewInfo(string title, object viewModel) : base(title, viewModel)
        {
        }
    }
}