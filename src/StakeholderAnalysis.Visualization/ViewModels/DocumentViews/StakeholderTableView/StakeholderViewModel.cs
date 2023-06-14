using System.ComponentModel;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView
{
    public class StakeholderViewModel : ViewModelBase
    {
        private bool isSelected;
        private string notes;

        /// <summary>
        ///     Parameterless constructor needed for DataGrid to allow adding new stakeholders.
        /// </summary>
        public StakeholderViewModel() : this(null, new Stakeholder())
        {
        }

        public StakeholderViewModel(ViewModelFactory factory, Stakeholder stakeholder) : base(factory)
        {
            Stakeholder = stakeholder;

            if (Stakeholder != null) Stakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        public Stakeholder Stakeholder { get; }

        public string Name
        {
            get => Stakeholder.Name;
            set
            {
                Stakeholder.Name = value;
                Stakeholder.OnPropertyChanged();
            }
        }

        public StakeholderType Type
        {
            get => Stakeholder.Type;
            set
            {
                if (Stakeholder != null)
                {
                    Stakeholder.Type = value;
                    Stakeholder.OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (value == isSelected)
                    return;
                isSelected = value;
                OnPropertyChanged();
            }
        }


        public override bool IsViewModelFor(object o)
        {
            return o as Stakeholder == Stakeholder;
        }

        protected virtual void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
                case nameof(Stakeholder.Type):
                    OnPropertyChanged(nameof(Type));
                    break;
            }
        }
    }
}