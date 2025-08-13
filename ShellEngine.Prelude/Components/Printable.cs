using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Components
{
    public abstract class Printable : IComponent
    {
        public List<Point> Pixels = new();
    }
}
