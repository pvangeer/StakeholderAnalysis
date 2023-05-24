using System.ComponentModel;
using System.Windows.Input;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Gui
{
    public class ViewInfo : NotifyPropertyChangedObservable
    {
        private readonly string bufferedTitle;
        private readonly INameableViewModel nameableViewModel;

        public ViewInfo(string title, object viewModel, string iconReference)
        {
            IconReference = iconReference;
            bufferedTitle = title;
            ViewModel = viewModel;
            nameableViewModel = viewModel as INameableViewModel;
            if (nameableViewModel != null) nameableViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public string Title
        {
            get
            {
                if (nameableViewModel != null) return nameableViewModel.DisplayName;
                return bufferedTitle;
            }
            set
            {
                if (nameableViewModel != null) nameableViewModel.DisplayName = value;
            }
        }

        public string IconReference { get; }

        public object ViewModel { get; }

        public ICommand CloseViewCommand { get; set; }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(INameableViewModel.DisplayName):
                    OnPropertyChanged(nameof(Title));
                    break;
            }
        }
    }
}