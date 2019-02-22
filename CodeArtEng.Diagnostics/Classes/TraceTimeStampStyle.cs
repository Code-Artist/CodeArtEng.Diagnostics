using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// Define format of time stamp in <see cref="TraceLogger"/> and <see cref="TraceFileWriter"/>
    /// </summary>
    public enum TraceTimeStampStyle
    {
        /// <summary>
        /// Date time string as defined in TimeStampFormat property
        /// </summary>
        DateTimeString,
        /// <summary>
        /// Date time as tick count in large integer
        /// </summary>
        TickCount
    }
}
