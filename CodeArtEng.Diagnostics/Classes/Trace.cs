using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

[assembly: CLSCompliant(true)]
namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// TraceLogger write() function callback
    /// </summary>
    public delegate void TraceLoggerWrite(string message);
    /// <summary>
    /// TraceLogger flush() function callback
    /// </summary>
    public delegate void TraceLoggerFlush();

    /// <summary>
    /// A simple implementation of <see cref="TraceListener"/>.
    /// </summary>
    /// <seealso cref="TraceListener"/>
    public class TraceLogger : TraceListener
    {
        TraceLoggerWrite OnWrite;
        TraceLoggerFlush OnFlush;
        private bool _EnableTracer;

        private void DummyWrite(string message) { }
        private void DummyFlush() { }

        /// <summary>
        /// Initializes a new instance of <see cref="TraceLogger"/>.
        /// </summary>
        /// <param name="writeCallback">Callback to TraceListener write function. Value can be null.</param>
        /// <param name="flushCallback">Callback to TraceListener flush function. Value can be null.</param>
        public TraceLogger(TraceLoggerWrite writeCallback, TraceLoggerFlush flushCallback)
        {
            Enabled = true;
            OnWrite = writeCallback ?? DummyWrite;
            OnFlush = flushCallback ?? DummyFlush;
        }
        /// <summary>
        /// Received message from Trace source.
        /// </summary>
        /// <param name="message">Message received.</param>
        public override void Write(string message) { OnWrite(message); }
        /// <summary>
        /// Receive message from Trace source followed by a line terminator.
        /// </summary>
        /// <param name="message">Message received.</param>
        public override void WriteLine(string message) { OnWrite(message + "\n"); }
        /// <summary>
        /// Flushes trace buffer.
        /// </summary>
        public override void Flush() { OnFlush(); }
        /// <summary>
        /// Enable / Disable TraceListener.
        /// </summary>
        /// <value>
        /// <para><c>True</c> = Monitor trace listener activites.</para>
        /// <para><c>False</c> = Suspend trace listener activities.</para>
        /// </value>
        public bool Enabled
        {
            get
            {
                return _EnableTracer;
            }
            set
            {
                if (_EnableTracer == value) return; //Avoid multiple set / clear

                _EnableTracer = value;
                if (_EnableTracer == true)
                    Trace.Listeners.Add(this);
                else
                    Trace.Listeners.Remove(this);
            }
        }

    }
}
