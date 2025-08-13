using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Protocol
{
    public enum WorkState
    {
        Wait,
        Working,
        Terminated,
        Error
    }
}
