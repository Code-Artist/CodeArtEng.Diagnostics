using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Globalization;

[assembly: CLSCompliant(true)]
namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// TraceLogger write() / writeline() function callback to write message to target output.
    /// </summary>
    /// <seealso cref="TraceLoggerMessageReceived"/>
    public delegate void TraceLoggerWrite(string message);
    /// <summary>
    /// TraceLogger flush() function callback
    /// </summary>
    public delegate void TraceLoggerFlush();
    /// <summary>
    /// TraceLogger write() / writeline() function callback when message is received.
    /// </summary>
    /// <param name="message"></param>
    /// <seealso cref="TraceLoggerWrite"/>
    public delegate void TraceLoggerMessageReceived(ref string message);

    /// <summary>
    /// A simple implementation of <see cref="TraceListener"/> for text based logging
    /// </summary>
    /// <seealso cref="TraceListener"/>
    public class TraceLogger : TraceListener
    {
        TraceLoggerWrite OnWrite;
        TraceLoggerFlush OnFlush;
        TraceLoggerMessageReceived OnMessageReceived;
        private bool _EnableTracer;

        private const string NewLineDelimiter = "\r\n";
        private bool IsNewLine;

        private void DummyWrite(string message) { }
        private void DummyFlush() { }
        private void DummyMsgReceived(ref string message) { }

        /// <summary>
        /// Initializes a new instance of <see cref="TraceLogger"/>.
        /// </summary>
        /// <param name="writeCallback">Callback to TraceListener write() / writeline() function. Value can be null.</param>
        /// <param name="flushCallback">Callback to TraceListener flush() function. Value can be null.</param>
        /// <param name="messageReceivedCallback">Callback to TraceListener write() / writeline() function when message is received, can be used for message filtering. Value can be null.</param>
        public TraceLogger(TraceLoggerWrite writeCallback, TraceLoggerFlush flushCallback, 
            TraceLoggerMessageReceived messageReceivedCallback = null)
        {
            Enabled = true;
            ShowTimeStamp = false;
            OnWrite = writeCallback ?? DummyWrite;
            OnFlush = flushCallback ?? DummyFlush;
            OnMessageReceived = messageReceivedCallback ?? DummyMsgReceived;
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
            OnMessageReceived(ref message);
            if (string.IsNullOrEmpty(message)) return;

            message = ParseMessage(message);
            OnWrite(message);
            IsNewLine = false;
        }

        /// <summary>
        /// Receive message from Trace source followed by a line terminator.
        /// </summary>
        /// <param name="message">Message received.</param>
        public override void WriteLine(string message)
        {
            OnMessageReceived(ref message);
            if (string.IsNullOrEmpty(message)) return;

            message = ParseMessage(message);
            OnWrite(message + NewLineDelimiter);
            IsNewLine = true;
        }

        //ToDo: Add property, unified CRLF
        private string ParseMessage(string message)
        {
            string dateTimeStr = ShowTimeStamp ? AppendDateTime() : string.Empty;
            string result = IsNewLine ? dateTimeStr : string.Empty;

            if (message.Contains("\r") || message.Contains("\n"))
            {
                //Unified CR, CRLF, LFCR, LF
                message = message.Replace("\n", "\r");
                message = message.Replace("\r\r", "\r");

                string newLineFiller = new string(' ', dateTimeStr.Length);
                string[] multiLineMessage = message.Split('\r');
                result += multiLineMessage[0].Trim() + NewLineDelimiter;
                foreach (string msg in multiLineMessage.Skip(1))
                    result += newLineFiller + msg.Trim() + NewLineDelimiter;

                result = result.TrimEnd();
            }
            else result += message;
            return result;
        }

        private string AppendDateTime()
        {
            switch (TimeStampStyle)
            {
                case TraceTimeStampStyle.DateTimeString: return "[" + DateTime.Now.ToString(TimeStampFormat) + "] ";
                case TraceTimeStampStyle.TickCount: return "[" + DateTime.Now.Ticks.ToString() + "] ";
            }
            return "-";
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
        /// Define date time display format when <see cref="TimeStampStyle"/> set as <see cref="TraceTimeStampStyle.DateTimeString"/>  . Use default format if undefined.
        /// Time stamp is append in front of message when <see cref="ShowTimeStamp"/> is enabled.
        /// </summary>
        public string TimeStampFormat { get; set; }

        /// <summary>
        /// Define time stamp style.
        /// </summary>
        /// <seealso cref="TimeStampFormat"/>
        public TraceTimeStampStyle TimeStampStyle { get; set; } = TraceTimeStampStyle.DateTimeString;

    }
}
