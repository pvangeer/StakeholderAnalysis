using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Visualization
{
    public interface IDropHandler
    {
        void Moved(double xRelativeNew, double yRelativeNew);
    }
}
