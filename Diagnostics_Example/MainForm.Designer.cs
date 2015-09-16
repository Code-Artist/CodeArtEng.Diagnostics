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
            this.diagnosticsTextBox1 = new CodeArtEng.Diagnostics.Controls.DiagnosticsTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkListenerEnabled
            // 
            this.chkListenerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListenerEnabled.AutoSize = true;
            this.chkListenerEnabled.Location = new System.Drawing.Point(702, 14);
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
            this.chkAutoFlushEnabled.Location = new System.Drawing.Point(702, 37);
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
            this.BtWrite.Location = new System.Drawing.Point(700, 97);
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
            this.BtFlush.Location = new System.Drawing.Point(700, 126);
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
            this.btThreadWrite.Location = new System.Drawing.Point(700, 155);
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
            this.chkShowTimeStamp.Location = new System.Drawing.Point(702, 60);
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
            this.propertyGrid1.Location = new System.Drawing.Point(12, 37);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(277, 355);
            this.propertyGrid1.TabIndex = 9;
            // 
            // diagnosticsTextBox1
            // 
            this.diagnosticsTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagnosticsTextBox1.FlushEnabled = true;
            this.diagnosticsTextBox1.ListenerEnabled = false;
            this.diagnosticsTextBox1.Location = new System.Drawing.Point(295, 12);
            this.diagnosticsTextBox1.Multiline = true;
            this.diagnosticsTextBox1.Name = "diagnosticsTextBox1";
            this.diagnosticsTextBox1.OutputFile = null;
            this.diagnosticsTextBox1.ReadOnly = true;
            this.diagnosticsTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.diagnosticsTextBox1.Size = new System.Drawing.Size(399, 380);
            this.diagnosticsTextBox1.TabIndex = 7;
            this.diagnosticsTextBox1.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 404);
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
    }
}

