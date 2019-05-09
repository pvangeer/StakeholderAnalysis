using System.Windows.Input;

namespace StakeholderAnalysis.Gui
{
    public class ViewInfo
    {
        public ViewInfo(string title, object viewModel, string iconReference, bool isDocumentView)
        {
            IsDocumentView = isDocumentView;
            IconReference = iconReference;
            Title = title;
            ViewModel = viewModel;
        }

        public string Title { get; }

        public string IconReference { get; }

        public object ViewModel { get; }

        public CloseViewCommand CloseViewCommand { get; set; }

        public bool IsDocumentView { get; }
    }
}