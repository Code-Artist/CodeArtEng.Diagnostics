namespace Diagnostics_Example
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chkListenerEnabled = new System.Windows.Forms.CheckBox();
            this.chkAutoFlushEnabled = new System.Windows.Forms.CheckBox();
            this.BtWrite = new System.Windows.Forms.Button();
            this.BtFlush = new System.Windows.Forms.Button();
            this.btThreadWrite = new System.Windows.Forms.Button();
            this.WorkerThread = new System.ComponentModel.BackgroundWorker();
            this.chkShowTimeStamp = new System.Windows.Forms.CheckBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gpTerminal = new System.Windows.Forms.GroupBox();
            this.txtTerminalCmd = new System.Windows.Forms.TextBox();
            this.btStopTerminal = new System.Windows.Forms.Button();
            this.btStartTerminal = new System.Windows.Forms.Button();
            this.BtProfilerTest = new System.Windows.Forms.Button();
            this.diagnosticsRichTextBox1 = new CodeArtEng.Diagnostics.Controls.DiagnosticsRichTextBox();
            this.diagnosticsTextBox1 = new CodeArtEng.Diagnostics.Controls.DiagnosticsTextBox();
            this.gpTerminal.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkListenerEnabled
            // 
            this.chkListenerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListenerEnabled.AutoSize = true;
            this.chkListenerEnabled.Location = new System.Drawing.Point(1652, 16);
            this.chkListenerEnabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkListenerEnabled.Name = "chkListenerEnabled";
            this.chkListenerEnabled.Size = new System.Drawing.Size(91, 19);
            this.chkListenerEnabled.TabIndex = 1;
            this.chkListenerEnabled.Text = "StartListener";
            this.chkListenerEnabled.UseVisualStyleBackColor = true;
            this.chkListenerEnabled.CheckedChanged += new System.EventHandler(this.chkListenerEnabled_CheckedChanged);
            // 
            // chkAutoFlushEnabled
            // 
            this.chkAutoFlushEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoFlushEnabled.AutoSize = true;
            this.chkAutoFlushEnabled.Location = new System.Drawing.Point(1650, 43);
            this.chkAutoFlushEnabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkAutoFlushEnabled.Name = "chkAutoFlushEnabled";
            this.chkAutoFlushEnabled.Size = new System.Drawing.Size(80, 19);
            this.chkAutoFlushEnabled.TabIndex = 2;
            this.chkAutoFlushEnabled.Text = "AutoFlush";
            this.chkAutoFlushEnabled.UseVisualStyleBackColor = true;
            this.chkAutoFlushEnabled.CheckedChanged += new System.EventHandler(this.chkAutoFlushEnabled_CheckedChanged);
            // 
            // BtWrite
            // 
            this.BtWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtWrite.Location = new System.Drawing.Point(1643, 112);
            this.BtWrite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtWrite.Name = "BtWrite";
            this.BtWrite.Size = new System.Drawing.Size(88, 27);
            this.BtWrite.TabIndex = 3;
            this.BtWrite.Text = "Writes ...";
            this.BtWrite.UseVisualStyleBackColor = true;
            this.BtWrite.Click += new System.EventHandler(this.BtWrite_Click);
            // 
            // BtFlush
            // 
            this.BtFlush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtFlush.Location = new System.Drawing.Point(1643, 145);
            this.BtFlush.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtFlush.Name = "BtFlush";
            this.BtFlush.Size = new System.Drawing.Size(88, 27);
            this.BtFlush.TabIndex = 4;
            this.BtFlush.Text = "Flush";
            this.BtFlush.UseVisualStyleBackColor = true;
            this.BtFlush.Click += new System.EventHandler(this.BtFlush_Click);
            // 
            // btThreadWrite
            // 
            this.btThreadWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btThreadWrite.Location = new System.Drawing.Point(1643, 179);
            this.btThreadWrite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btThreadWrite.Name = "btThreadWrite";
            this.btThreadWrite.Size = new System.Drawing.Size(88, 27);
            this.btThreadWrite.TabIndex = 6;
            this.btThreadWrite.Text = "ThreadWrite";
            this.btThreadWrite.UseVisualStyleBackColor = true;
            this.btThreadWrite.Click += new System.EventHandler(this.btThreadWrite_Click);
            // 
            // WorkerThread
            // 
            this.WorkerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerThread_DoWork);
            // 
            // chkShowTimeStamp
            // 
            this.chkShowTimeStamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowTimeStamp.AutoSize = true;
            this.chkShowTimeStamp.Location = new System.Drawing.Point(1652, 69);
            this.chkShowTimeStamp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkShowTimeStamp.Name = "chkShowTimeStamp";
            this.chkShowTimeStamp.Size = new System.Drawing.Size(89, 19);
            this.chkShowTimeStamp.TabIndex = 8;
            this.chkShowTimeStamp.Text = "Time Stamp";
            this.chkShowTimeStamp.UseVisualStyleBackColor = true;
            this.chkShowTimeStamp.CheckedChanged += new System.EventHandler(this.chkShowTimeStamp_CheckedChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(14, 43);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(308, 410);
            this.propertyGrid1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 43);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 27);
            this.button1.TabIndex = 10;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid2.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid2.Location = new System.Drawing.Point(329, 43);
            this.propertyGrid2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(308, 410);
            this.propertyGrid2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Process Executor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "DiagnosticsTextBox";
            // 
            // gpTerminal
            // 
            this.gpTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpTerminal.Controls.Add(this.txtTerminalCmd);
            this.gpTerminal.Controls.Add(this.btStopTerminal);
            this.gpTerminal.Controls.Add(this.btStartTerminal);
            this.gpTerminal.Location = new System.Drawing.Point(644, 452);
            this.gpTerminal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpTerminal.Name = "gpTerminal";
            this.gpTerminal.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpTerminal.Size = new System.Drawing.Size(524, 92);
            this.gpTerminal.TabIndex = 14;
            this.gpTerminal.TabStop = false;
            this.gpTerminal.Text = "Terminal";
            // 
            // txtTerminalCmd
            // 
            this.txtTerminalCmd.Location = new System.Drawing.Point(7, 55);
            this.txtTerminalCmd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTerminalCmd.Name = "txtTerminalCmd";
            this.txtTerminalCmd.Size = new System.Drawing.Size(502, 23);
            this.txtTerminalCmd.TabIndex = 2;
            this.txtTerminalCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTerminalCmd_KeyDown);
            // 
            // btStopTerminal
            // 
            this.btStopTerminal.Location = new System.Drawing.Point(102, 22);
            this.btStopTerminal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btStopTerminal.Name = "btStopTerminal";
            this.btStopTerminal.Size = new System.Drawing.Size(88, 27);
            this.btStopTerminal.TabIndex = 1;
            this.btStopTerminal.Text = "Stop";
            this.btStopTerminal.UseVisualStyleBackColor = true;
            this.btStopTerminal.Click += new System.EventHandler(this.btStopTerminal_Click);
            // 
            // btStartTerminal
            // 
            this.btStartTerminal.Location = new System.Drawing.Point(7, 22);
            this.btStartTerminal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btStartTerminal.Name = "btStartTerminal";
            this.btStartTerminal.Size = new System.Drawing.Size(88, 27);
            this.btStartTerminal.TabIndex = 0;
            this.btStartTerminal.Text = "Start";
            this.btStartTerminal.UseVisualStyleBackColor = true;
            this.btStartTerminal.Click += new System.EventHandler(this.btStartTerminal_Click);
            // 
            // BtProfilerTest
            // 
            this.BtProfilerTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtProfilerTest.Location = new System.Drawing.Point(1643, 212);
            this.BtProfilerTest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtProfilerTest.Name = "BtProfilerTest";
            this.BtProfilerTest.Size = new System.Drawing.Size(88, 27);
            this.BtProfilerTest.TabIndex = 15;
            this.BtProfilerTest.Text = "Profiler";
            this.BtProfilerTest.UseVisualStyleBackColor = true;
            this.BtProfilerTest.Click += new System.EventHandler(this.BtProfilerTest_Click);
            // 
            // diagnosticsRichTextBox1
            // 
            this.diagnosticsRichTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(28)))), ((int)(((byte)(0)))));
            this.diagnosticsRichTextBox1.FlushEnabled = false;
            this.diagnosticsRichTextBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.diagnosticsRichTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(198)))), ((int)(((byte)(12)))));
            this.diagnosticsRichTextBox1.ListenerEnabled = false;
            this.diagnosticsRichTextBox1.Location = new System.Drawing.Point(1140, 14);
            this.diagnosticsRichTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.diagnosticsRichTextBox1.Name = "diagnosticsRichTextBox1";
            this.diagnosticsRichTextBox1.OutputFile = null;
            this.diagnosticsRichTextBox1.OutputFileBackup = null;
            this.diagnosticsRichTextBox1.ReadOnly = true;
            this.diagnosticsRichTextBox1.RefreshInterval = 100;
            this.diagnosticsRichTextBox1.Size = new System.Drawing.Size(488, 431);
            this.diagnosticsRichTextBox1.TabIndex = 16;
            this.diagnosticsRichTextBox1.Theme = CodeArtEng.Diagnostics.TextBoxTheme.NightVision;
            this.diagnosticsRichTextBox1.TimeStampFormat = "dd-MMM-yyyy h:mm:ss.fff tt";
            this.diagnosticsRichTextBox1.WordWrap = false;
            // 
            // diagnosticsTextBox1
            // 
            this.diagnosticsTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(36)))), ((int)(((byte)(86)))));
            this.diagnosticsTextBox1.FlushEnabled = false;
            this.diagnosticsTextBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.diagnosticsTextBox1.ForeColor = System.Drawing.Color.White;
            this.diagnosticsTextBox1.ListenerEnabled = false;
            this.diagnosticsTextBox1.Location = new System.Drawing.Point(644, 14);
            this.diagnosticsTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.diagnosticsTextBox1.Multiline = true;
            this.diagnosticsTextBox1.Name = "diagnosticsTextBox1";
            this.diagnosticsTextBox1.OutputFile = null;
            this.diagnosticsTextBox1.OutputFileBackup = null;
            this.diagnosticsTextBox1.ReadOnly = true;
            this.diagnosticsTextBox1.RefreshInterval = 50;
            this.diagnosticsTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.diagnosticsTextBox1.Size = new System.Drawing.Size(488, 431);
            this.diagnosticsTextBox1.TabIndex = 7;
            this.diagnosticsTextBox1.Theme = CodeArtEng.Diagnostics.TextBoxTheme.PowerShell;
            this.diagnosticsTextBox1.TimeStampFormat = "d/M/yyyy h:mm:ss.fff tt";
            this.diagnosticsTextBox1.WordWrap = false;
            this.diagnosticsTextBox1.WriteToFile = true;
            this.diagnosticsTextBox1.MessageReceived += new System.EventHandler<CodeArtEng.Diagnostics.Controls.TextEventArgs>(this.diagnosticsTextBox1_MessageReceived);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1758, 550);
            this.Controls.Add(this.diagnosticsRichTextBox1);
            this.Controls.Add(this.BtProfilerTest);
            this.Controls.Add(this.gpTerminal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propertyGrid2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.chkShowTimeStamp);
            this.Controls.Add(this.diagnosticsTextBox1);
            this.Controls.Add(this.btThreadWrite);
            this.Controls.Add(this.BtFlush);
            this.Controls.Add(this.BtWrite);
            this.Controls.Add(this.chkAutoFlushEnabled);
            this.Controls.Add(this.chkListenerEnabled);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.gpTerminal.ResumeLayout(false);
            this.gpTerminal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkListenerEnabled;
        private System.Windows.Forms.CheckBox chkAutoFlushEnabled;
        private System.Windows.Forms.Button BtWrite;
        private System.Windows.Forms.Button BtFlush;
        private System.Windows.Forms.Button btThreadWrite;
        private System.ComponentModel.BackgroundWorker WorkerThread;
        private CodeArtEng.Diagnostics.Controls.DiagnosticsTextBox diagnosticsTextBox1;
        private System.Windows.Forms.CheckBox chkShowTimeStamp;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpTerminal;
        private System.Windows.Forms.Button btStartTerminal;
        private System.Windows.Forms.Button btStopTerminal;
        private System.Windows.Forms.TextBox txtTerminalCmd;
        private System.Windows.Forms.Button BtProfilerTest;
        private CodeArtEng.Diagnostics.Controls.DiagnosticsRichTextBox diagnosticsRichTextBox1;
    }
}

