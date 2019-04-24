using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Gui
{
    public interface IViewInfo
    {
        string Title { get; }

        object ViewModel { get; }
    }
}
