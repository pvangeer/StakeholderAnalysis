using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Data
{
    public interface IVisibilityProperty: INotifyPropertyChangedImplementation
    {
        bool Visible { get; set; }
    }
}
