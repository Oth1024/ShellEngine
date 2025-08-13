using ShellEngine.Prelude.Components;
using ShellEngine.Prelude.Protocol;

namespace ShellEngine.Prelude.Plugin
{
    public class SimpleWindowPlugin : IPlugin
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public SimpleWindowPlugin(Size windowSize, PositionType windowInitialLocation = PositionType.OverCenter, Point location = null)
        {

        }

        /// <summary>
        /// Start a simple shell window with given size and lcoation;
        /// </summary>
        /// <param name="windowSize">Size of shell window;</param>
        /// <param name="windowInitialLocation">Window location when startup; This will not take effect if the "location" pamram not equals to "null";</param>
        /// <param name="location">Window location when startup.</param>
        /// <returns></returns>
        public static SimpleWindowPlugin StartASimpleWindow(Size windowSize, PositionType windowInitialLocation = PositionType.OverCenter, Point location = null)
        {
            return new SimpleWindowPlugin(windowSize, windowInitialLocation, location);
        }
        #endregion

        #region IPlugin Implementation
        public void LoadPlugin(App.App app)
        {

        }
        #endregion

        #region Methods

        #endregion
    }
}
