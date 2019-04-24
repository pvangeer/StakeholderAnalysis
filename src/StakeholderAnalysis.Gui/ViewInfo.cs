namespace StakeholderAnalysis.Gui
{
    public class ViewInfo
    {
        public ViewInfo(string title, object viewModel, string iconReference)
        {
            IconReference = iconReference;
            Title = title;
            ViewModel = viewModel;
        }

        public string Title { get; }

        public string IconReference { get; }

        public object ViewModel { get; }
    }
}