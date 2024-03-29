﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CodeArtEng.Diagnostics;

namespace Diagnostics_Example
{
    public partial class MainForm : Form
    {
        private CodeArtEng.Diagnostics.ProcessExecutor procExecutor;
        private CodeArtEng.Diagnostics.ProcessExecutor terminal = null;

        public MainForm()
        {
            InitializeComponent();
            diagnosticsTextBox1.OutputFile = "Output.log";
            diagnosticsTextBox1.WriteToFile = true;

            chkListenerEnabled.Checked = diagnosticsTextBox1.ListenerEnabled;
            chkAutoFlushEnabled.Checked = diagnosticsTextBox1.FlushEnabled;

            procExecutor = new CodeArtEng.Diagnostics.ProcessExecutor();
            procExecutor.Application = "../../TestA.bat";
            procExecutor.TraceLogEnabled = true;
            propertyGrid1.SelectedObject = procExecutor;
            propertyGrid2.SelectedObject = diagnosticsRichTextBox1;
            procExecutor.Exited += ProcExecutor_Exited;

            diagnosticsRichTextBox1.AddFormattingRule("Thread", Color.Blue);
            diagnosticsRichTextBox1.AddFormattingRule("Line 10", Color.Blue);
            diagnosticsRichTextBox1.AddFormattingRule("Trace Message 9", Color.Red);

            Trace.WriteLine("Admin Mode = " + Utility.IsRunAsAdmin());
            Trace.WriteLine("Startup Arguments = " + string.Join(" ", Environment.GetCommandLineArgs().Skip(1)));

        }

        private void ProcExecutor_Exited(object sender, ProcessResult e)
        {
            Trace.WriteLine(string.Join("\n", e.Output));
        }

        private void chkListenerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            diagnosticsTextBox1.ListenerEnabled = chkListenerEnabled.Checked;
            diagnosticsRichTextBox1.ListenerEnabled = !chkListenerEnabled.Checked;
            propertyGrid2.Refresh();
        }

        private void chkAutoFlushEnabled_CheckedChanged(object sender, EventArgs e)
        {
            diagnosticsTextBox1.FlushEnabled = chkAutoFlushEnabled.Checked;
            diagnosticsRichTextBox1.FlushEnabled = chkAutoFlushEnabled.Checked;
            propertyGrid2.Refresh();
        }

        private void BtFlush_Click(object sender, EventArgs e)
        {
            Trace.Flush();
        }

        private void BtWrite_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < 20; x++)
                Trace.Write(x.ToString() + " ");

            Trace.WriteLine(" ");
            Trace.WriteLine(" "); 
            
            for(int x = 0; x < 20; x++)
                Trace.WriteLine("Line " + x.ToString());


            Trace.WriteLine("Multiline String:\r\nLine 1\rLine2\nLine3");
        }

        private void WorkerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            Trace.WriteLine("Write message from worker thread...");
            for (int x = 0; x < 200; x++)
            {
                //Debug.WriteLine("Thread: Debug Message " + x.ToString());
                Trace.WriteLine("Thread: Trace Message " + x.ToString());
                System.Threading.Thread.Sleep(10);
            }
        }

        private void btThreadWrite_Click(object sender, EventArgs e)
        {
            CodeProfiler.Start("Thread Write");
            WorkerThread.RunWorkerAsync();
            for (int x = 0; x < 100; x++)
            {
                //Debug.WriteLine("Main: Debug Message " + x.ToString());
                Trace.WriteLine("Main: Trace Message " + x.ToString());
                System.Threading.Thread.Sleep(20);
                Application.DoEvents();
            }

            CodeProfiler.Stop("Thread Write");
        }

        private void chkShowTimeStamp_CheckedChanged(object sender, EventArgs e)
        {
            diagnosticsTextBox1.ShowTimeStamp = chkShowTimeStamp.Checked;
            diagnosticsRichTextBox1.ShowTimeStamp = chkShowTimeStamp.Checked;
            propertyGrid2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CodeArtEng.Diagnostics.ProcessResult result = procExecutor.Execute(false);
            if(result != null)
            {
                if (result.ErrorDetected) Trace.WriteLine("ERROR Detected.");
                foreach (string line in result.Output)
                    Trace.WriteLine(line);
            }
        }

        private void btStartTerminal_Click(object sender, EventArgs e)
        {
            if (terminal != null) terminal.Dispose();
            terminal = new CodeArtEng.Diagnostics.ProcessExecutor();
            terminal.Application = "cmd.exe";
            terminal.TraceLogEnabled = true;
            terminal.Name = "Terminal";
            terminal.ShowConsoleWindow = false;
            terminal.RedirectStandardInput = true;
            terminal.RedirectOutputToFile("Terminal.txt");
            terminal.Execute(false);
        }

        private void btStopTerminal_Click(object sender, EventArgs e)
        {
            terminal.Dispose();
            terminal = null;
        }

        private void txtTerminalCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Return)
            {
                if (terminal == null) return;
                terminal.ProcessHandler.StandardInput.WriteLine(txtTerminalCmd.Text);
                txtTerminalCmd.Text = string.Empty;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (terminal != null) terminal.Dispose();
            procExecutor.Dispose();
        }

        private void diagnosticsTextBox1_MessageReceived(object sender, CodeArtEng.Diagnostics.Controls.TextEventArgs e)
        {
            if (e.Message.Contains("4")) e.Message = string.Empty; //Filter any message contains '4'
        }

        private void BtProfilerTest_Click(object sender, EventArgs e)
        {
            CodeProfiler.Start("Test Timer 1");
            Thread.Sleep(100);
            CodeProfiler.Start("Test Timer 2");
            Thread.Sleep(100);
            CodeProfiler.Stop("Test Timer 1");
            CodeProfiler.Stop("Test Timer 2");
        }

        private void BtStartAsAdmin_Click(object sender, EventArgs e)
        {
            CodeArtEng.Diagnostics.Utility.RestartApplicationAsAdmin(txtAdminStartArg.Text);
        }
    }
}
