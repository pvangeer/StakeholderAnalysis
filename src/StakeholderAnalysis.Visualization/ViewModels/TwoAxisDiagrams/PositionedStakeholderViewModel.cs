using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.TwoAxisDiagrams
{
    public interface IPositionedStakeholderViewModel
    {
        bool IsViewModelFor(Stakeholder stakeholder);

        double RelativePositionLeft { get; set; }

        double RelativePositionTop { get; set; }
    }
}
