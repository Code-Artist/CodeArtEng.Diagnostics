using System.IO;
using System.Threading;

namespace CodeArtEng.Diagnostics
{
    //ToDo: Recovery Loop, from Normal to Backup

    /// <summary>
    /// <see cref="TraceFileWritter"/> Operation Mode.
    /// </summary>
    public enum TraceFileWritterMode
    {
        /// <summary>
        /// Disabled trace writting to file
        /// </summary>
        Disabled,
        /// <summary>
        /// Recording traces to <see cref="TraceFileWritter.OutputFile"/>
        /// </summary>
        Normal,
        /// <summary>
        /// Recording traces to <see cref="TraceFileWritter.BackupOutputFile"/>.
        /// <see cref="TraceFileWritter.OutputFile"/> not accessible or offline.
        /// </summary>
        Backup
    }

    /// <summary>
    /// Output DEBUG and TRACE log to file.
    /// </summary>
    public class TraceFileWritter
    {
        private readonly object WriterLock = new object();
        private TraceLogger Tracer;
        private Thread RecoveryThread;

        /// <summary>
        /// Operation Mode.
        /// </summary>
        public TraceFileWritterMode OperationMode { get; private set; }

        private string _OutputDir;
        private string _OutputFile;
        /// <summary>
        /// Target output file. When writter failed to write to this file for whatever reason,
        /// it will try to Write Traces to <see cref="BackupOutputFile"/> if defined. Else, trace capture will stop immediately.
        /// </summary>
        /// <remarks>Changing the value for this property caused <see cref="ListenerEnabled"/> set to FALSE.</remarks>
        public string OutputFile
        {
            get { return _OutputFile; }
            set
            {
                lock (WriterLock)
                {
                    if (string.IsNullOrEmpty(value)) { _OutputDir = _OutputFile = string.Empty; }
                    else
                    {
                        _OutputFile = value;
                        _OutputDir = Path.GetDirectoryName(_OutputFile);
                    }
                }
            }
        }

        private string _BackupOutputDir;
        private string _BackupOutputFile;
        /// <summary>
        /// Backup output target. Use only local path for this property. Trace will be output to this file when unable to write to <see cref="OutputFile"/>.
        /// Writter will merge content to OutputFile and resume trace recording with OutputFile once it is available again.
        /// Trace capture stop if failed to write to this file.
        /// </summary>
        /// <remarks>Changing the value for this property caused <see cref="ListenerEnabled"/> set to FALSE.</remarks>
        public string BackupOutputFile
        {
            get { return _BackupOutputFile; }
            set
            {
                lock (WriterLock)
                {
                    if (string.IsNullOrEmpty(value)) { _BackupOutputDir = _BackupOutputFile = string.Empty; }
                    else
                    {
                        _BackupOutputFile = value;
                        _BackupOutputDir = Path.GetDirectoryName(_BackupOutputFile);
                    }
                }
            }
        }

        /// <summary>
        /// Define how often the recovery loop should try to check if OutputFile is accesible once again.
        /// </summary>
        public int RetryInterval_ms { get; set; } = 1000;

        /// <summary>
        /// Enable / Disable Log Capture
        /// </summary>
        public bool ListenerEnabled
        {
            get { return Tracer.Enabled; }
            set
            {
                lock (WriterLock)
                {
                    if (Tracer.Enabled == value) return;

                    Tracer.Enabled = value;
                    if (Tracer.Enabled)
                        OperationMode = TraceFileWritterMode.Normal;
                    else
                        OperationMode = TraceFileWritterMode.Disabled;
                }
            }
        }

        /// <summary>
        /// Enable / Disable printing time stamp in front of each message.
        /// </summary>
        public bool ShowTimeStamp { get { return Tracer.ShowTimeStamp; } set { Tracer.ShowTimeStamp = value; } }

        /// <summary>
        /// Enable / Disable TraceLogger.
        /// </summary>
        public TraceFileWritter()
        {
            Tracer = new TraceLogger(Tracer_OnWriteMessage, Tracer_OnFlush);
            Tracer.Enabled = false;
        }

        private void Tracer_OnFlush()
        {
            //Do Nothing
        }

        private void Tracer_OnWriteMessage(string message)
        {
            lock (WriterLock)
            {
                switch (OperationMode)
                {
                    case TraceFileWritterMode.Disabled:
                        break;

                    case TraceFileWritterMode.Normal:
                        TryWriteMessageToOutputFile(message);
                        break;

                    case TraceFileWritterMode.Backup:
                        TryWriteMessageToBackupFile(message);
                        break;
                }

            }
        }

        private bool TryWriteMessageToOutputFile(string message)
        {
            lock (WriterLock)
            {
                try
                {
                    if(string.IsNullOrEmpty(_OutputFile))
                    {
                        OperationMode = TraceFileWritterMode.Disabled;
                        Tracer.Enabled = false;
                        return false;
                    }

                    if (!string.IsNullOrEmpty(_OutputDir))
                        if (!Directory.Exists(_OutputDir)) Directory.CreateDirectory(_OutputDir);
                    File.AppendAllText(_OutputFile, message);
                    return true;
                }
                catch
                {
                    OperationMode = TraceFileWritterMode.Disabled;
                    if (string.IsNullOrEmpty(_BackupOutputFile)) return false;
                    if (SetupBackupLogFile() == false) return false;
                    if (TryWriteMessageToBackupFile(message) == false) return false;

                    //Start Recovery Thread
                    OperationMode = TraceFileWritterMode.Backup;
                    RecoveryThread = new Thread(Recovery);
                    RecoveryThread.Start();
                    return true;
                }
            }
        }

        private void Recovery()
        {
            while (true)
            {
                Thread.Sleep(RetryInterval_ms);
                lock (WriterLock)
                {
                    try
                    {
                        FileStream fileStream = File.OpenWrite(_OutputFile);
                        fileStream.Close();

                        string filebuffer = File.ReadAllText(_BackupOutputFile);
                        File.AppendAllText(_OutputFile, filebuffer);
                        OperationMode = TraceFileWritterMode.Normal;
                        return;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

        }

        private bool SetupBackupLogFile()
        {
            try
            {
                if (File.Exists(_BackupOutputFile)) File.Delete(_BackupOutputFile);
                return true;
            }
            catch { return false; }
        }

        private bool TryWriteMessageToBackupFile(string message)
        {
            lock (WriterLock)
            {
                try
                {
                    if (!string.IsNullOrEmpty(_BackupOutputDir))
                        if (!Directory.Exists(_BackupOutputDir)) Directory.CreateDirectory(_BackupOutputDir);
                    File.AppendAllText(_BackupOutputFile, message);
                    return true;
                }
                catch
                {
                    OperationMode = TraceFileWritterMode.Disabled;
                    return false;
                }
            }
        }
    }
}
