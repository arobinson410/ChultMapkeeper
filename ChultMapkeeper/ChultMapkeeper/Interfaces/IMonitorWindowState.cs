using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChultMapkeeper.Interfaces
{
    public interface IMonitorWindowState
    {
        void OnWindowStateChanged(object sender, EventArgs e);
    }
}
