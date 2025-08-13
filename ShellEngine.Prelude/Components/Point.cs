using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Components
{
    public class Point : IComponent
    {
        #region Constructors
        public Point()
        {

        }

        public Point(uint x, uint y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Properties
        public uint X { get; set; }
        public uint Y { get; set; }
        #endregion

        #region Methods
        public Point Clone()
        {
            return new Point(X, Y);
        }
        #endregion
    }
}
