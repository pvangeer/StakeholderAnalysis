using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Gui
{
    public interface IGui
    {
        bool IsMagnifierActive { get; set; }

        ViewManager ViewManager { get; set; }
    }
}
