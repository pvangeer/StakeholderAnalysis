using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Visualization.ViewModels
{
    public class StakeholderViewInfo
    {
        public StakeholderViewInfo(string name, StakeholderViewType type, MainWindowViewModel viewModel)
        {
            Type = type;
            ViewModel = viewModel;
            Name = name;
        }

        public StakeholderViewType Type { get; }

        public string Name { get; }

        public MainWindowViewModel ViewModel { get; }
    }
}
