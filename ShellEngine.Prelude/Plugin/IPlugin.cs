using ShellEngine.Prelude.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Plugin
{
    public interface IPlugin
    {
        public void LoadPlugin(App.App app);
    }
}
