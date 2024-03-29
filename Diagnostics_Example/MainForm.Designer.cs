﻿namespace Diagnostics_Example
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtStartAsAdmin = new System.Windows.Forms.Button();
            this.txtAdminStartArg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gpTerminal.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkListenerEnabled
            // 
            this.chkListenerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListenerEnabled.AutoSize = true;
            this.chkListenerEnabled.Location = new System.Drawing.Point(1409, 14);
            this.chkListenerEnabled.Name = "chkListenerEnabled";
            this.chkListenerEnabled.Size = new System.Drawing.Size(85, 17);
            this.chkListenerEnabled.TabIndex = 1;
            this.chkListenerEnabled.Text = "StartListener";
            this.chkListenerEnabled.UseVisualStyleBackColor = true;
            this.chkListenerEnabled.CheckedChanged += new System.EventHandler(this.chkListenerEnabled_CheckedChanged);
            // 
            // chkAutoFlushEnabled
            // 
            this.chkAutoFlushEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoFlushEnabled.AutoSize = true;
            this.chkAutoFlushEnabled.Location = new System.Drawing.Point(1410, 37);
            this.chkAutoFlushEnabled.Name = "chkAutoFlushEnabled";
            this.chkAutoFlushEnabled.Size = new System.Drawing.Size(73, 17);
            this.chkAutoFlushEnabled.TabIndex = 2;
            this.chkAutoFlushEnabled.Text = "AutoFlush";
            this.chkAutoFlushEnabled.UseVisualStyleBackColor = true;
            this.chkAutoFlushEnabled.CheckedChanged += new System.EventHandler(this.chkAutoFlushEnabled_CheckedChanged);
            // 
            // BtWrite
            // 
            this.BtWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtWrite.Location = new System.Drawing.Point(1408, 97);
            this.BtWrite.Name = "BtWrite";
            this.BtWrite.Size = new System.Drawing.Size(75, 23);
            this.BtWrite.TabIndex = 3;
            this.BtWrite.Text = "Writes ...";
            this.BtWrite.UseVisualStyleBackColor = true;
            this.BtWrite.Click += new System.EventHandler(this.BtWrite_Click);
            // 
            // BtFlush
            // 
            this.BtFlush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtFlush.Location = new System.Drawing.Point(1408, 126);
            this.BtFlush.Name = "BtFlush";
            this.BtFlush.Size = new System.Drawing.Size(75, 23);
            this.BtFlush.TabIndex = 4;
            this.BtFlush.Text = "Flush";
            this.BtFlush.UseVisualStyleBackColor = true;
            this.BtFlush.Click += new System.EventHandler(this.BtFlush_Click);
            // 
            // btThreadWrite
            // 
            this.btThreadWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btThreadWrite.Location = new System.Drawing.Point(1408, 155);
            this.btThreadWrite.Name = "btThreadWrite";
            this.btThreadWrite.Size = new System.Drawing.Size(75, 23);
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
            this.chkShowTimeStamp.Location = new System.Drawing.Point(1410, 60);
            this.chkShowTimeStamp.Name = "chkShowTimeStamp";
            this.chkShowTimeStamp.Size = new System.Drawing.Size(82, 17);
            this.chkShowTimeStamp.TabIndex = 8;
            this.chkShowTimeStamp.Text = "Time Stamp";
            this.chkShowTimeStamp.UseVisualStyleBackColor = true;
            this.chkShowTimeStamp.CheckedChanged += new System.EventHandler(this.chkShowTimeStamp_CheckedChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(12, 37);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(264, 355);
            this.propertyGrid1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid2.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid2.Location = new System.Drawing.Point(282, 37);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(264, 355);
            this.propertyGrid2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Process Executor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "DiagnosticsTextBox";
            // 
            // gpTerminal
            // 
            this.gpTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gpTerminal.Controls.Add(this.txtTerminalCmd);
            this.gpTerminal.Controls.Add(this.btStopTerminal);
            this.gpTerminal.Controls.Add(this.btStartTerminal);
            this.gpTerminal.Location = new System.Drawing.Point(552, 392);
            this.gpTerminal.Name = "gpTerminal";
            this.gpTerminal.Size = new System.Drawing.Size(449, 80);
            this.gpTerminal.TabIndex = 14;
            this.gpTerminal.TabStop = false;
            this.gpTerminal.Text = "Terminal";
            // 
            // txtTerminalCmd
            // 
            this.txtTerminalCmd.Location = new System.Drawing.Point(6, 48);
            this.txtTerminalCmd.Name = "txtTerminalCmd";
            this.txtTerminalCmd.Size = new System.Drawing.Size(431, 20);
            this.txtTerminalCmd.TabIndex = 2;
            this.txtTerminalCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTerminalCmd_KeyDown);
            // 
            // btStopTerminal
            // 
            this.btStopTerminal.Location = new System.Drawing.Point(87, 19);
            this.btStopTerminal.Name = "btStopTerminal";
            this.btStopTerminal.Size = new System.Drawing.Size(75, 23);
            this.btStopTerminal.TabIndex = 1;
            this.btStopTerminal.Text = "Stop";
            this.btStopTerminal.UseVisualStyleBackColor = true;
            this.btStopTerminal.Click += new System.EventHandler(this.btStopTerminal_Click);
            // 
            // btStartTerminal
            // 
            this.btStartTerminal.Location = new System.Drawing.Point(6, 19);
            this.btStartTerminal.Name = "btStartTerminal";
            this.btStartTerminal.Size = new System.Drawing.Size(75, 23);
            this.btStartTerminal.TabIndex = 0;
            this.btStartTerminal.Text = "Start";
            this.btStartTerminal.UseVisualStyleBackColor = true;
            this.btStartTerminal.Click += new System.EventHandler(this.btStartTerminal_Click);
            // 
            // BtProfilerTest
            // 
            this.BtProfilerTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtProfilerTest.Location = new System.Drawing.Point(1408, 184);
            this.BtProfilerTest.Name = "BtProfilerTest";
            this.BtProfilerTest.Size = new System.Drawing.Size(75, 23);
            this.BtProfilerTest.TabIndex = 15;
            this.BtProfilerTest.Text = "Profiler";
            this.BtProfilerTest.UseVisualStyleBackColor = true;
            this.BtProfilerTest.Click += new System.EventHandler(this.BtProfilerTest_Click);
            // 
            // diagnosticsRichTextBox1
            // 
            this.diagnosticsRichTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.diagnosticsRichTextBox1.FlushEnabled = false;
            this.diagnosticsRichTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.diagnosticsRichTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.diagnosticsRichTextBox1.ListenerEnabled = false;
            this.diagnosticsRichTextBox1.Location = new System.Drawing.Point(977, 12);
            this.diagnosticsRichTextBox1.Name = "diagnosticsRichTextBox1";
            this.diagnosticsRichTextBox1.ReadOnly = true;
            this.diagnosticsRichTextBox1.RefreshInterval = 100;
            this.diagnosticsRichTextBox1.Size = new System.Drawing.Size(419, 374);
            this.diagnosticsRichTextBox1.TabIndex = 16;
            this.diagnosticsRichTextBox1.TimeStampFormat = "dd-MMM-yyyy h:mm:ss.fff tt";
            this.diagnosticsRichTextBox1.WordWrap = false;
            // 
            // diagnosticsTextBox1
            // 
            this.diagnosticsTextBox1.FlushEnabled = false;
            this.diagnosticsTextBox1.ListenerEnabled = false;
            this.diagnosticsTextBox1.Location = new System.Drawing.Point(552, 12);
            this.diagnosticsTextBox1.Multiline = true;
            this.diagnosticsTextBox1.Name = "diagnosticsTextBox1";
            this.diagnosticsTextBox1.ReadOnly = true;
            this.diagnosticsTextBox1.RefreshInterval = 50;
            this.diagnosticsTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.diagnosticsTextBox1.Size = new System.Drawing.Size(419, 374);
            this.diagnosticsTextBox1.TabIndex = 7;
            this.diagnosticsTextBox1.TimeStampFormat = "d/M/yyyy h:mm:ss.fff tt";
            this.diagnosticsTextBox1.WordWrap = false;
            this.diagnosticsTextBox1.WriteToFile = true;
            this.diagnosticsTextBox1.MessageReceived += new System.EventHandler<CodeArtEng.Diagnostics.Controls.TextEventArgs>(this.diagnosticsTextBox1_MessageReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtStartAsAdmin);
            this.groupBox1.Controls.Add(this.txtAdminStartArg);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 401);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 71);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run As Admin";
            // 
            // BtStartAsAdmin
            // 
            this.BtStartAsAdmin.Location = new System.Drawing.Point(64, 39);
            this.BtStartAsAdmin.Name = "BtStartAsAdmin";
            this.BtStartAsAdmin.Size = new System.Drawing.Size(148, 23);
            this.BtStartAsAdmin.TabIndex = 2;
            this.BtStartAsAdmin.Text = "Start As Admin";
            this.BtStartAsAdmin.UseVisualStyleBackColor = true;
            this.BtStartAsAdmin.Click += new System.EventHandler(this.BtStartAsAdmin_Click);
            // 
            // txtAdminStartArg
            // 
            this.txtAdminStartArg.Location = new System.Drawing.Point(64, 16);
            this.txtAdminStartArg.Name = "txtAdminStartArg";
            this.txtAdminStartArg.Size = new System.Drawing.Size(288, 20);
            this.txtAdminStartArg.TabIndex = 1;
            this.txtAdminStartArg.Text = "Command Arg in Admin Mode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Argument";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1507, 477);
            this.Controls.Add(this.groupBox1);
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
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.gpTerminal.ResumeLayout(false);
            this.gpTerminal.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtStartAsAdmin;
        private System.Windows.Forms.TextBox txtAdminStartArg;
        private System.Windows.Forms.Label label3;
    }
}

