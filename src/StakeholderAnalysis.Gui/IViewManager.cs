using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StakeholderAnalysis.Gui
{
    public interface IViewManager
    {
        ObservableCollection<IViewInfo> Views{ get; }

        IViewInfo ActiveDocument { get; set; }

        ObservableCollection<IViewInfo> ToolWindows { get; }

        void OpenView(IViewInfo viewInfo);

        void CloseView(IViewInfo viewInfo);

        void BringToFront(IViewInfo viewInfo);

        void OpenToolWindow(ToolWindowViewInfo viewInfo);

        void CloseToolWindow(ToolWindowViewInfo viewInfo);
    }
}
