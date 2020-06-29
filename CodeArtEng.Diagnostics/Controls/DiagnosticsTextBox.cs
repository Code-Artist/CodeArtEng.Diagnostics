using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace CodeArtEng.Diagnostics.Controls
{
    /// <summary>
    /// Textbox with TraceListener
    /// </summary>
    public class DiagnosticsTextBox : TextBox
    {
        private static readonly object LockObject = new object();
        string MessageBuffer;

        private ContextMenuStrip contextMenuStrip1;
        private IContainer components;
        private ToolStripMenuItem toolStripEnabled;
        private ToolStripMenuItem toolStripClear;
        private Timer refreshTimer;
        private ToolStripMenuItem toolStripSaveToFile;
        private TraceLogger Tracer;
        private ToolStripMenuItem toolStripCopyAll;
        private ToolStripMenuItem toolStripCopySelected;
        private TraceFileWriter OutputFileWriter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                refreshTimer.Enabled = false;
                Tracer.Enabled = false;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTextBox"/> class.
        /// </summary>
        public DiagnosticsTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //Double Buffer

            //Default control property
            base.Multiline = true;
            ScrollBars = System.Windows.Forms.ScrollBars.Both;
            DisplayBufferSize = 0;

            base.ReadOnly = true;
            Width = Height = 100;
            MessageBuffer = "";

            OutputFileWriter = new TraceFileWriter();

            //Setup listener
            Tracer = new TraceLogger(Tracer_OnWriteMessage, Tracer_OnFlush, Tracer_OnMessageReceived);
            ConfigureFileWritter();
        }
        private static bool IsInDesignMode(IComponent component)
        {
            bool ret = false;
            if (null != component)
            {
                ISite site = component.Site;
                if (null != site)
                {
                    ret = site.DesignMode;
                }
                else if (component is System.Windows.Forms.Control)
                {
                    IComponent parent = ((System.Windows.Forms.Control)component).Parent;
                    ret = IsInDesignMode(parent);
                }
            }
            return ret;
        }

        #region [ Hide Base Class Property ]
        [Browsable(false)]
        private new bool AcceptsReturn { get; set; }

        [Browsable(false)]
        private new bool AcceptsTab { get; set; }

        [Browsable(false)]
        private new bool ReadOnly { get; set; }

        [Browsable(false)]
        private new bool Multiline { get; set; } = true;
        #endregion

        /// <summary>
        /// Enable / Disable trace listener to capture message from trace source.
        /// </summary>
        /// <value></value>
        [Category("Trace Listener")]
        [DisplayName("ListenerEnabled")]
        [Description("Enable / Disable trace listener to capture message from trace source.")]
        [DefaultValue(true)]
        public bool ListenerEnabled
        {
            get { return Tracer.Enabled; }
            set
            {
                Tracer.Enabled = value;
                ConfigureFileWritter();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control should response to Flush() command from trace source.
        /// </summary>
        /// <value>Ignore Flush() command if disabled, else clear all text in control.</value>
        [Category("Trace Listener")]
        [DisplayName("AutoFlushEnabled")]
        [Description("Indicate whether control should response to Flush() command from trace source.")]
        [DefaultValue(true)]
        public bool FlushEnabled { get; set; }

        /// <summary>
        /// Enable / Disable printing time stamp in front of each message.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("ShowTimeStamp")]
        [Description("Append time stamp in front of each log message.")]
        [DefaultValue(false)]
        public bool ShowTimeStamp
        {
            get { return Tracer.ShowTimeStamp; }
            set
            {
                Tracer.ShowTimeStamp = value;
                OutputFileWriter.ShowTimeStamp = value;
            }
        }

        /// <summary>
        /// Define time stamp string format when <see cref="TimeStampStyle"/> set as <see cref="TraceTimeStampStyle.DateTimeString"/> 
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("TimeStampFormat")]
        [Description("Define time stamp string format when TimeStampStyle set as DateTimeString")]
        public string TimeStampFormat
        {
            get { return Tracer.TimeStampFormat; }
            set
            {
                Tracer.TimeStampFormat = value;
                OutputFileWriter.TimeStampFormat = value;
            }
        }

        /// <summary>
        /// Define time stamp style.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("TimeStampStyle")]
        [Description("Define time stamp style")]
        [DefaultValue(TraceTimeStampStyle.DateTimeString)]
        public TraceTimeStampStyle TimeStampStyle
        {
            get { return Tracer.TimeStampStyle; }
            set
            {
                Tracer.TimeStampStyle = value;
                OutputFileWriter.TimeStampStyle = value;
            }
        }

        /// <summary>
        /// Define number of lines to be keep in text box. Set to 0 to keep all lines.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("DisplayBufferSize")]
        [Description("Define how many lines of logs to keep in text box. Set to 0 to keep all lines.")]
        [DefaultValue(0)]
        public int DisplayBufferSize { get; set; }

        /// <summary>
        /// Define how often the control shall be updated. Value in ms.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("Refresh Interval (ms).")]
        [Description("Define how often the control shall be updated")]
        [DefaultValue(10)]
        public int RefreshInterval { get => refreshTimer.Interval; set => refreshTimer.Interval = value; }

        private void Tracer_OnWriteMessage(string message)
        {
            if (IsInDesignMode(this)) return;
            lock (LockObject)
            {
                MessageBuffer += message;
            }
        }
        private void Tracer_OnFlush()
        {
            if (FlushEnabled) this.Clear();
        }

        private void Tracer_OnMessageReceived(ref string message)
        {
            //Message filter implementation. 
            if(MessageReceived != null)
            {
                TextEventArgs eArg = new TextEventArgs() { Message = message };
                MessageReceived.Invoke(this, eArg);
                message = eArg.Message;
            }
        }

        /// <summary>
        /// Occur when message is received by Trace Listener.
        /// </summary>
        [DisplayName("MessageReceived")]
        public event EventHandler<TextEventArgs> MessageReceived;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEnabled,
            this.toolStripClear,
            this.toolStripSaveToFile,
            this.toolStripCopySelected,
            this.toolStripCopyAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripEnabled
            // 
            this.toolStripEnabled.Name = "toolStripEnabled";
            this.toolStripEnabled.Size = new System.Drawing.Size(173, 22);
            this.toolStripEnabled.Text = "Enabled";
            // 
            // toolStripClear
            // 
            this.toolStripClear.Name = "toolStripClear";
            this.toolStripClear.Size = new System.Drawing.Size(173, 22);
            this.toolStripClear.Text = "Clear";
            // 
            // toolStripSaveToFile
            // 
            this.toolStripSaveToFile.Name = "toolStripSaveToFile";
            this.toolStripSaveToFile.Size = new System.Drawing.Size(173, 22);
            this.toolStripSaveToFile.Text = "Save to File...";
            // 
            // toolStripCopySelected
            // 
            this.toolStripCopySelected.Name = "toolStripCopySelected";
            this.toolStripCopySelected.Size = new System.Drawing.Size(173, 22);
            this.toolStripCopySelected.Text = "Copy Selected Text";
            // 
            // toolStripCopyAll
            // 
            this.toolStripCopyAll.Name = "toolStripCopyAll";
            this.toolStripCopyAll.Size = new System.Drawing.Size(173, 22);
            this.toolStripCopyAll.Text = "Copy All";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 50;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // DiagnosticsTextBox
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.WordWrap = false;
            this.VisibleChanged += new System.EventHandler(this.DiagnosticsTextBox_VisibleChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private void DiagnosticsTextBox_VisibleChanged(object sender, EventArgs e)
        {
            ListenerEnabled = !IsInDesignMode(this);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripEnabled)
            {
                ListenerEnabled = !ListenerEnabled;
            }
            else if (e.ClickedItem == toolStripClear)
            {
                this.Clear();
            }
            else if (e.ClickedItem == toolStripSaveToFile)
            {
                contextMenuStrip1.Hide(); //Hide context menu.
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(dlg.FileName, this.Text);
                    }
                }
            }
            else if (e.ClickedItem == toolStripCopyAll)
            {
                Clipboard.SetText(Text);
            }
            else if (e.ClickedItem == toolStripCopySelected)
            {
                Clipboard.SetText(SelectedText);
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            toolStripEnabled.Checked = ListenerEnabled;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            lock (LockObject)
            {
                //Transfer from Message Buffer to Diagnostics Text Box without locking main thread.
                if (MessageBuffer.Length == 0) return;
                this.AppendText(MessageBuffer);
                MessageBuffer = "";

                if (DisplayBufferSize <= 0) return;

                if (Lines.Length > DisplayBufferSize)
                {
                    string[] data = new string[DisplayBufferSize];
                    Array.Copy(Lines, Lines.Length - DisplayBufferSize, data, 0, DisplayBufferSize);
                    Lines = data;
                }
                SelectionStart = Text.Length;
                ScrollToCaret();
            }

        }

        /// <summary>
        /// Output trace to file. Set to a valid path to enable this option.
        /// Disabled automatically when failed to write output.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("OutputFile")]
        [Description("Configure output file for traces. Set WriteToFile property to true to enable trace output to file.")]
        [DefaultValue("")]
        public string OutputFile
        {
            get { return OutputFileWriter.OutputFile; }
            set { OutputFileWriter.OutputFile = value; }
        }

        /// <summary>
        /// Configure secondary output path for trace output as backup when failed to write to OutputFile. Recommend to use local path which is guaranteed to be available.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("OutputFileBackup")]
        [Description("Configure secondary output path for trace output as backup when failed to write to OutputFile. Recommend to use local path which is guaranteed to be available.")]
        [DefaultValue("")]
        public string OutputFileBackup
        {
            get { return OutputFileWriter.BackupOutputFile; }
            set { OutputFileWriter.BackupOutputFile = value; }
        }

        private bool _WriteToFile = false;
        /// <summary>
        /// Set this flag to true to enable writing log to OutputFile.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("WriteToFile")]
        [Description("Enable / Disable trace output to file.")]
        [DefaultValue(false)]
        public bool WriteToFile
        {
            get { return _WriteToFile; }
            set
            {
                _WriteToFile = value;
                ConfigureFileWritter();
            }
        }

        /// <summary>
        /// Return the current text from the TextBox. 
        /// </summary>
        [Browsable(false)]
        public new string Text { get { return base.Text; } private set { base.Text = value; } }

        private void ConfigureFileWritter()
        {
            OutputFileWriter.ListenerEnabled = (ListenerEnabled && _WriteToFile);
        }
    }

}
