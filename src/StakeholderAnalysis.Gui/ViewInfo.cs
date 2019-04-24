namespace StakeholderAnalysis.Gui
{
    public class ViewInfo
    {
        public ViewInfo(string title, object viewModel)
        {
            Title = title;
            ViewModel = viewModel;
        }

        public string Title { get; }

        public object ViewModel { get; }
    }
}