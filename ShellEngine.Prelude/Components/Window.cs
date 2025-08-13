using ShellEngine.Prelude.Components;
using ShellEngine.Prelude.Protocol;

namespace ShellEngine.Prelude.Components
{
    public class Window
    {
        #region Fields
        private Size _size;
        private Vector _currentPosition;
        #endregion

        #region Constructor
        public Window(Size size, PositionType positionType = PositionType.None, Vector initialPosition = null)
        {
            _size = size;
            _currentPosition = initialPosition;
        }
        #endregion

        #region
        public static Window StartAWindow(Size size, PositionType positionType = PositionType.None, Vector initialPosition = null)
        {
            return new Window(size, positionType, initialPosition).Show();
        }

        public Window Show()
        {
            return this;
        }
        #endregion
    }
}
