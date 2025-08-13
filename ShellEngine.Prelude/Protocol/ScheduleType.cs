using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngine.Prelude.Protocol
{
    /// <summary>
    /// When will schedules function;<br/>
    /// Rendering will function at the end of "Schedule", before "PostSchedule".
    /// </summary>
    public enum ScheduleType
    {
        /// <summary>
        /// "PreSchedule" functions at first;<br/>
        /// "PreSchedule" is always responsible for data synchronization and automatic updates.
        /// </summary>
        PreSchedule,

        /// <summary>
        /// "Schedule" functions after "PreSchedule" and before "PostSchedule";<br/>
        /// Rendering happens at the end of this process.
        /// </summary>
        Schedule,

        /// <summary>
        /// "PostSchedule" functions at last;<br/>
        /// Rendering results can be acquired in this process;<br/>
        /// "PostSchedule" is always responsible for callbacks and data post updates.
        /// </summary>
        PostSchedule
    }
}
