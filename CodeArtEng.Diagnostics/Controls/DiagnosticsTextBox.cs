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
        private string MessageBuffer;

        private ContextMenuStrip contextMenu;
        private IContainer components;
        private ToolStripMenuItem menuItemEnabled;
        private ToolStripMenuItem menuItemClear;
        private ToolStripMenuItem menuItemSaveToFile;
        private ToolStripMenuItem menuItemCopyAll;
        private ToolStripMenuItem menuItemCopySelected;
        private Timer refreshTimer;
        private TraceLogger Tracer;
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
            ConfigureFileWriter();
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

        #region [ Theme ]

        /// <summary>
        /// Fore Color changed event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            if (!UpdatingTheme) ThemeValue = TextBoxTheme.UserDefined;
            base.OnForeColorChanged(e);
        }

        /// <summary>
        /// Back Color changed event handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!UpdatingTheme) ThemeValue = TextBoxTheme.UserDefined;
            base.OnBackColorChanged(e);
        }

        /// <summary>
        /// Font changed event handler.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            if (!UpdatingTheme) ThemeValue = TextBoxTheme.UserDefined;
            base.OnFontChanged(e);
        }

        private bool UpdatingTheme = false;
        private TextBoxTheme ThemeValue = TextBoxTheme.Windows;
        /// <summary>
        /// Get or set Diagnostic TextBox Theme. <see cref="TextBoxTheme"/>
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(TextBoxTheme), "Windows")]
        [Description("Get or set Diagnostic TextBox Theme.")]
        public TextBoxTheme Theme
        {
            get => ThemeValue;
            set
            {
                UpdatingTheme = true;
                try { ThemeValue = this.SetTheme(value); }
                finally { UpdatingTheme = false; }
            }
        }

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
                ConfigureFileWriter();
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
            if (MessageReceived != null)
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
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEnabled,
            this.menuItemClear,
            this.menuItemSaveToFile,
            this.menuItemCopySelected,
            this.menuItemCopyAll});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(174, 114);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripEnabled
            // 
            this.menuItemEnabled.Name = "toolStripEnabled";
            this.menuItemEnabled.Size = new System.Drawing.Size(173, 22);
            this.menuItemEnabled.Text = "Enabled";
            // 
            // toolStripClear
            // 
            this.menuItemClear.Name = "toolStripClear";
            this.menuItemClear.Size = new System.Drawing.Size(173, 22);
            this.menuItemClear.Text = "Clear";
            // 
            // toolStripSaveToFile
            // 
            this.menuItemSaveToFile.Name = "toolStripSaveToFile";
            this.menuItemSaveToFile.Size = new System.Drawing.Size(173, 22);
            this.menuItemSaveToFile.Text = "Save to File...";
            // 
            // toolStripCopySelected
            // 
            this.menuItemCopySelected.Name = "toolStripCopySelected";
            this.menuItemCopySelected.Size = new System.Drawing.Size(173, 22);
            this.menuItemCopySelected.Text = "Copy Selected Text";
            // 
            // toolStripCopyAll
            // 
            this.menuItemCopyAll.Name = "toolStripCopyAll";
            this.menuItemCopyAll.Size = new System.Drawing.Size(173, 22);
            this.menuItemCopyAll.Text = "Copy All";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 50;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // DiagnosticsTextBox
            // 
            this.ContextMenuStrip = this.contextMenu;
            this.WordWrap = false;
            this.VisibleChanged += new System.EventHandler(this.DiagnosticsTextBox_VisibleChanged);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private void DiagnosticsTextBox_VisibleChanged(object sender, EventArgs e)
        {
            ListenerEnabled = !IsInDesignMode(this);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == menuItemEnabled)
            {
                ListenerEnabled = !ListenerEnabled;
            }
            else if (e.ClickedItem == menuItemClear)
            {
                this.Clear();
            }
            else if (e.ClickedItem == menuItemSaveToFile)
            {
                contextMenu.Hide(); //Hide context menu.
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(dlg.FileName, this.Text);
                    }
                }
            }
            else if (e.ClickedItem == menuItemCopyAll)
            {
                Clipboard.SetText(Text);
            }
            else if (e.ClickedItem == menuItemCopySelected)
            {
                Clipboard.SetText(SelectedText);
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            menuItemEnabled.Checked = ListenerEnabled;
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
                ConfigureFileWriter();
            }
        }

        /// <summary>
        /// Return the current text from the TextBox. 
        /// </summary>
        [Browsable(false)]
        public new string Text { get { return base.Text; } private set { base.Text = value; } }

        private void ConfigureFileWriter()
        {
            OutputFileWriter.ListenerEnabled = (ListenerEnabled && _WriteToFile);
        }
    }

}
