using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;

namespace CodeArtEng.Diagnostics.Tests
{
    [TestFixture]
    class TraceFileWritterTests
    {
        TraceFileWritter fileWritter;
        private string OutputFile, BackupFile, OutputFileInvalid;

        public TraceFileWritterTests()
        {
            fileWritter = new TraceFileWritter();
            fileWritter.OutputFile = OutputFile = ".\\Output\\Log.txt";
            fileWritter.BackupOutputFile = BackupFile = ".\\Backup\\Log.txt";
            OutputFileInvalid = "A:\\Output\\Log.txt";
            fileWritter.ListenerEnabled = false;
        }

        [Test]
        public void WriteOutputNormalMode()
        {
            if (File.Exists(OutputFile)) File.Delete(OutputFile);
            Directory.Delete(Path.GetDirectoryName(OutputFile));
            fileWritter.ListenerEnabled = true;
            WriteTestStrings(1, 100);
            fileWritter.ListenerEnabled = false;
            Assert.AreEqual(100, File.ReadAllLines(OutputFile).Count());
        }

        public void WriteTestStrings(int start, int end)
        {
            for (int x = start; x <= end; x++)
            {
                Debug.WriteLine(string.Format("Test String : {0}", x));
            }
        }

        [Test]
        public void WriteOutputBackupMode()
        {
            try
            {
                if (File.Exists(BackupFile)) File.Delete(BackupFile);
                Directory.Delete(Path.GetDirectoryName(BackupFile));
                fileWritter.OutputFile = OutputFileInvalid;
                fileWritter.ListenerEnabled = true;
                WriteTestStrings(1, 200);
                fileWritter.ListenerEnabled = false;
                Assert.AreEqual(200, File.ReadAllLines(BackupFile).Count());
            }
            finally
            {
                fileWritter.ListenerEnabled = false;
                fileWritter.OutputFile = OutputFile;
            }
        }

        [Test]
        public void WriteOutputInvalidPathDisabled()
        {
            try
            {
                fileWritter.OutputFile = OutputFileInvalid;
                fileWritter.BackupOutputFile = OutputFileInvalid;
                fileWritter.ListenerEnabled = true;
                Debug.WriteLine("Testing");
                Assert.AreEqual(TraceFileWritterMode.Disabled, fileWritter.OperationMode);
            }
            finally
            {
                fileWritter.ListenerEnabled = false;
                fileWritter.OutputFile = OutputFile;
                fileWritter.BackupOutputFile = BackupFile;
            }
        }

        [Test]
        public void RecoveryMode()
        {
            string outputPath = Path.GetDirectoryName(OutputFile);
            if (File.Exists(OutputFile)) File.Delete(OutputFile);
            if(Directory.Exists(outputPath)) Directory.Delete(outputPath);

            string backupPath = Path.GetDirectoryName(BackupFile);
            if (File.Exists(BackupFile)) File.Delete(BackupFile);
            if(Directory.Exists(backupPath)) Directory.Delete(backupPath);

            fileWritter.ShowTimeStamp = true;
            fileWritter.RetryInterval_ms = 10;
            fileWritter.ListenerEnabled = true;
            WriteTestStrings(1, 1000);
            Assert.AreEqual(TraceFileWritterMode.Normal, fileWritter.OperationMode);
            fileWritter.OutputFile = OutputFileInvalid;
            WriteTestStrings(1001, 2000);
            Assert.AreEqual(TraceFileWritterMode.Backup, fileWritter.OperationMode);
            fileWritter.OutputFile = OutputFile;
            WriteTestStrings(2001, 3000);
            Assert.AreEqual(TraceFileWritterMode.Normal, fileWritter.OperationMode);
            fileWritter.ListenerEnabled = false;
            fileWritter.ShowTimeStamp = false;

            Assert.AreEqual(3000, File.ReadAllLines(OutputFile).Count());
        }
    }
}
