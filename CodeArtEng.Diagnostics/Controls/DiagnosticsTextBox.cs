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

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTextBox"/> class.
        /// </summary>
        public DiagnosticsTextBox()
        {
            InitializeComponent();

            //Default control property
            Multiline = true;
            ScrollBars = System.Windows.Forms.ScrollBars.Both;

            ReadOnly = true;
            Width = Height = 100;
            MessageBuffer = "";

            //Setup listener
            Tracer = new TraceLogger(Tracer_OnWriteMessage, Tracer_OnFlush);
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

        /// <summary>
        /// Enable / Disable trace listener to capture message from trace source.
        /// </summary>
        /// <value></value>
        [Category("Trace Listener")]
        [DisplayName("ListenerEnabled")]
        [Description("Enable / Disable trace listener to capture message from trace source.")]
        public bool ListenerEnabled { get { return Tracer.Enabled; } set { Tracer.Enabled = value; } }

        /// <summary>
        /// Gets or sets a value indicating whether control should response to Flush() command from trace source.
        /// </summary>
        /// <value>Ignore Flush() command if disabled, else clear all text in control.</value>
        [Category("Trace Listener")]
        [DisplayName("AutoFlushEnabled")]
        [Description("Indicate whether control should response to Flush() command from trace source.")]
        public bool FlushEnabled { get; set; }

        /// <summary>
        /// Enable / Disable printing time stamp in front of each message.
        /// </summary>
        [Category("Trace Listener")]
        [DisplayName("ShowTimeStamp")]
        [Description("Append time stamp in front of each log message.")]
        [DefaultValue(false)]
        public bool ShowTimeStamp { get { return Tracer.ShowTimeStamp; } set { Tracer.ShowTimeStamp = value; } }

        private void Tracer_OnWriteMessage(string message)
        {
            //if (ListenerEnabled) HandleWriteEvent(message);
            if (ListenerEnabled)
            {
                lock (LockObject)
                {
                    MessageBuffer += message;
                    if (!string.IsNullOrEmpty(_OutputFile)) System.IO.File.AppendAllText(_OutputFile, message);
                }
            }
        }
        private void Tracer_OnFlush()
        {
            if (FlushEnabled) this.Clear();
        }

        private delegate void WriteDelegate(string message);
        private void HandleWriteEvent(string message)
        {
            if (InvokeRequired)
            {
                WriteDelegate delFunction = new WriteDelegate(HandleWriteEvent);
                Invoke(delFunction, new object[] { message });
                return;
            }

            //this.AppendText(message);
            this.Text += message;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripClear = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEnabled,
            this.toolStripClear,
            this.toolStripSaveToFile});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripEnabled
            // 
            this.toolStripEnabled.Name = "toolStripEnabled";
            this.toolStripEnabled.Size = new System.Drawing.Size(142, 22);
            this.toolStripEnabled.Text = "Enabled";
            // 
            // toolStripClear
            // 
            this.toolStripClear.Name = "toolStripClear";
            this.toolStripClear.Size = new System.Drawing.Size(142, 22);
            this.toolStripClear.Text = "Clear";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 10;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // toolStripSaveToFile
            // 
            this.toolStripSaveToFile.Name = "toolStripSaveToFile";
            this.toolStripSaveToFile.Size = new System.Drawing.Size(142, 22);
            this.toolStripSaveToFile.Text = "Save to File...";
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
            }

        }

        private string _OutputFile;
        public string OutputFile
        {
            get { return _OutputFile; }
            set { lock (LockObject) { _OutputFile = value; } }
        }
    }
}
