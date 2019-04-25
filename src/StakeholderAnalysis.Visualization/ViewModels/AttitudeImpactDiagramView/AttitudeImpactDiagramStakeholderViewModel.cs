using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.AttitudeImpactDiagramView
{
    public class AttitudeImpactDiagramStakeholderViewModel : StakeholderViewModel
    {
        public AttitudeImpactDiagramStakeholderViewModel(Stakeholder stakeholder) : base(stakeholder)
        {
            // TODO Pull position members up. See also comments in ForceFieldDiagramStakeholderViewModel
        }
    }
}
