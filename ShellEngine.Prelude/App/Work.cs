using ShellEngine.Prelude.Protocol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.App
{
    public class Work
    {
        #region Fields
        private uint _fps = 60;
        private WorkState _state = WorkState.Wait;
        private Action _action;
        private Stopwatch _stopwatch = new();

        // flags
        private bool _terminated = false;
        #endregion

        #region Events
        public event EventHandler WorkReadyToStart;
        public event EventHandler WorkStarted;
        public event EventHandler WorkToTerminate;
        public event EventHandler WorkTerminated;
        #endregion

        #region Properties
        private TimeSpan _waitTime => TimeSpan.FromSeconds(1 / (double)_fps);
        #endregion

        #region Constructor
        public Work(Action action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public void Start()
        {
            WorkReadyToStart?.Invoke(this, new());
            _state = WorkState.Working;
            WorkStarted?.Invoke(this, new());
            while (true)
            {
                OnWorkStarted();
                if (_terminated)
                {
                    WorkToTerminate.Invoke(this, new());
                    break;
                }
                try
                {
                    _action.Invoke();
                }
                catch
                {
                    _terminated = true;
                }
                OnWorkDone();
            }
            _state = WorkState.Terminated;
        }

        public void Terminate()
        {
            _terminated = true;
        }

        public Work SetFps(uint newFps)
        {
            _fps = newFps;
            return this;
        }

        private void OnWorkStarted()
        {
            _stopwatch?.Restart();
        }

        private void OnWorkDone()
        {
            if (_stopwatch.Elapsed < _waitTime)
            {
                var waitTime = _waitTime - _stopwatch.Elapsed;
                WaitFor(waitTime);
            }
        }

        private void WaitFor(TimeSpan timeSpan)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (true)
            {
                if (stopWatch.Elapsed >= timeSpan)
                {
                    break;
                }
            }
            stopWatch = null;
        }
        #endregion
    }
}
