using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;

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

        public const string NewLineDelimiter = "\r\n";
        public bool IsNewLine;

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
            ShowTimeStamp = false;
            OnWrite = writeCallback ?? DummyWrite;
            OnFlush = flushCallback ?? DummyFlush;
            IsNewLine = true;

            DateTimeFormatInfo timeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            TimeStampFormat = timeFormat.ShortDatePattern + " " + timeFormat.LongTimePattern;
            TimeStampFormat = TimeStampFormat.Replace("ss", "ss.fff"); //Include miliseconds
        }
        /// <summary>
        /// Received message from Trace source.
        /// </summary>CRLF
        /// <param name="message">Message received.</param>
        public override void Write(string message)
        {
            if (ShowTimeStamp && IsNewLine) message = DateTime.Now.ToString() + ": " + message;
            OnWrite(message);
            IsNewLine = false;
        }
        /// <summary>
        /// Receive message from Trace source followed by a line terminator.
        /// </summary>
        /// <param name="message">Message received.</param>
        public override void WriteLine(string message)
        {
            if (ShowTimeStamp && IsNewLine) message = "[" + DateTime.Now.ToString(TimeStampFormat) + "] " + message;
            OnWrite(message + NewLineDelimiter);
            IsNewLine = true;
        }
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

        /// <summary>
        /// Enable / Disable time stamp in log.
        /// </summary>
        public bool ShowTimeStamp { get; set; }

        /// <summary>
        /// Define date time display format. Use default format if undefined.
        /// Time stamp is append in front of message when <see cref="ShowTimeStamp"/> is enabled.
        /// </summary>
        public string TimeStampFormat { get; set; }

    }
}
