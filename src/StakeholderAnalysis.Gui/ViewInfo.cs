using System.Windows.Input;

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

        // TODO: Update title in case of property change
        public string Title { get; }

        public string IconReference { get; }

        public object ViewModel { get; }

        public ICommand CloseViewCommand { get; set; }
    }
}