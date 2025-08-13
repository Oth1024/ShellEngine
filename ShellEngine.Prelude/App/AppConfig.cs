using ShellEngine.Prelude.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.App
{
    public class AppConfig
    {
        public AppConfig(uint fps)
        {
            FPS = fps;
        }

        public uint FPS { get; set; }
    }
}
