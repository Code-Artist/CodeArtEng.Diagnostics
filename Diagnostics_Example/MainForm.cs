using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Diagnostics_Example
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            chkListenerEnabled.Checked = diagnosticsTextBox1.ListenerEnabled;
            chkAutoFlushEnabled.Checked = diagnosticsTextBox1.FlushEnabled;
        }

        private void chkListenerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            diagnosticsTextBox1.ListenerEnabled = chkListenerEnabled.Checked;
        }

        private void chkAutoFlushEnabled_CheckedChanged(object sender, EventArgs e)
        {
            diagnosticsTextBox1.FlushEnabled = chkAutoFlushEnabled.Checked;
        }

        private void BtFlush_Click(object sender, EventArgs e)
        {
            Trace.Flush();
        }

        private void BtWrite_Click(object sender, EventArgs e)
        {
            diagnosticsTextBox1.SuspendLayout();
            for(int x = 0; x < 20; x++)
                Trace.Write(x.ToString() + " ");

            Trace.WriteLine(" ");
            Trace.WriteLine(" "); 
            
            for(int x = 0; x < 20; x++)
                Trace.WriteLine("Line " + x.ToString());
            diagnosticsTextBox1.ResumeLayout();
        }

        private void WorkerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            Trace.WriteLine("Write message from worker thread...");
            for (int x = 0; x < 10; x++)
            {
                Debug.WriteLine("Thread: Debug Message " + x.ToString());
                Trace.WriteLine("Thread: Trace Message " + x.ToString());
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void btThreadWrite_Click(object sender, EventArgs e)
        {
            WorkerThread.RunWorkerAsync();
        }
    }
}
