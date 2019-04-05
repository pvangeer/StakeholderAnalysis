using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Data
{
    public class StakeholderGroup : Stakeholder
    {
        public StakeholderGroup(string name, double leftPercentage, double topPercentage) : base(name, leftPercentage, topPercentage, StakeholderType.Stakeholdergroep)
        {
        }
    }
}
