using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization.ViewModels.DocumentViews.StakeholderTableView
{
    public class StakeholderDetailsViewModel : ViewModelBase
    {
        private readonly Stakeholder stakeholder;

        public StakeholderDetailsViewModel(ViewModelFactory factory, Stakeholder stakeholder) : base(factory)
        {
            this.stakeholder = stakeholder;
            stakeholder.PropertyChanged += StakeholderPropertyChanged;
        }

        public string Name
        {
            get => stakeholder.Name;
            set
            {
                stakeholder.Name = value;
                stakeholder.OnPropertyChanged();
            }
        }

        public string Notes
        {
            get => stakeholder.Notes;
            set
            {
                if (value == stakeholder.Notes)
                    return;
                stakeholder.Notes = value;
                stakeholder.OnPropertyChanged();
            }
        }

        public string TelephoneNumber
        {
            get => stakeholder.TelephoneNumber;
            set
            {
                if (value == stakeholder.TelephoneNumber)
                    return;
                stakeholder.TelephoneNumber = value;
                stakeholder.OnPropertyChanged();
            }
        }

        public ICommand CallStakeholderCommand => new CanAlwaysExecuteActionCommand
        {
            ExecuteAction = o => { Process.Start($"tel:{stakeholder.TelephoneNumber}"); }
        };

        public ICommand EmailStakeholderCommand => new CanAlwaysExecuteActionCommand
        {
            ExecuteAction = o => { Process.Start($"mailto:{stakeholder.Email}"); }
        };

        public StakeholderType Type
        {
            get => stakeholder.Type;
            set
            {
                if (Equals(value, stakeholder.Type))
                    return;
                stakeholder.Type = value;
                stakeholder.OnPropertyChanged();
            }
        }

        public string Email
        {
            get => stakeholder.Email;
            set
            {
                if (value == stakeholder.Email)
                    return;
                stakeholder.Email = value;
                stakeholder.OnPropertyChanged();
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return o == stakeholder;
        }

        private void StakeholderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Stakeholder.Name):
                    OnPropertyChanged(nameof(Name));
                    break;
            }
        }
    }
}