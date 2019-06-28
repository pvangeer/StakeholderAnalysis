using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Data
{
    public interface INameProperty : INotifyPropertyChangedImplementation
    {
        string Name { get; set; }
    }
}
