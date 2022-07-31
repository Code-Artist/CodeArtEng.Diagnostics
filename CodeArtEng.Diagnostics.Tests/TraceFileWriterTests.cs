using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using System.Linq;
//ToDo: Review Test Cases

namespace CodeArtEng.Diagnostics.Tests
{
    [TestFixture]
    class TraceFileWriterTests
    {
        private string OutputFile, BackupFile;
        private static string OutputFileInvalid = "A:/Output/Log.txt";

        public TraceFileWriter SetupTraceFileWritter(string outputFile, string backupFile)
        {
            TraceFileWriter traceWriter = new TraceFileWriter();
            traceWriter.ListenerEnabled = false;
            traceWriter.OutputFile = OutputFile = outputFile;
            traceWriter.BackupOutputFile = BackupFile = backupFile;
            if (File.Exists(OutputFile)) File.Delete(OutputFile);
            if (File.Exists(BackupFile)) File.Delete(BackupFile);
            return traceWriter;
        }

        [Test]
        public void WriteOutputNormalMode()
        {
            TraceFileWriter traceWriter = SetupTraceFileWritter("./Output/NormalLog.txt", "./Backup/NormalLog.txt");
            traceWriter.ListenerEnabled = true;
            WriteTestStrings(1, 100);
            Assert.AreEqual(TraceFileWriterMode.Normal, traceWriter.OperationMode);
            traceWriter.ListenerEnabled = false;
            Assert.AreEqual(100, File.ReadAllLines(OutputFile).Count());
            Assert.IsFalse(File.Exists(traceWriter.BackupOutputFile));

        }

        public void WriteTestStrings(int start, int end)
        {
            for (int x = start; x <= end; x++)
            {
                Trace.WriteLine(string.Format("Test String : {0}", x));
            }
        }

        public void WriteTestStrings(int start, int end, string comment)
        {
            for (int x = start; x <= end; x++)
            {
                Trace.WriteLine(string.Format("Test String : {0} : {1}", x, comment));
            }
        }

        [Test]
        public void WriteOutputBackupMode()
        {
            TraceFileWriter traceWriter = SetupTraceFileWritter(OutputFileInvalid, "./Backup/BackupLog.txt");
            traceWriter.ListenerEnabled = true;
            WriteTestStrings(1, 200);
            Assert.AreEqual(TraceFileWriterMode.Backup, traceWriter.OperationMode);
            traceWriter.ListenerEnabled = false;
            Assert.AreEqual(200, File.ReadAllLines(BackupFile).Count());
        }

        [Test]
        public void WriteOutputInvalidPathDisabled()
        {
            TraceFileWriter traceWriter = SetupTraceFileWritter(OutputFileInvalid, OutputFileInvalid);
            traceWriter.OutputFile = OutputFileInvalid;
            traceWriter.BackupOutputFile = OutputFileInvalid;
            traceWriter.ListenerEnabled = true;
            Trace.WriteLine("Testing");
            Assert.AreEqual(TraceFileWriterMode.Disabled, traceWriter.OperationMode);
        }

        [Test]
        public void RecoveryMode()
        {
            string recoveryOutputFile = "./Output/RecoveryLog.txt";
            TraceFileWriter traceWriter = SetupTraceFileWritter(recoveryOutputFile, "./Backup/RecoverLog.txt");
            traceWriter.ShowTimeStamp = true;
            traceWriter.RetryInterval_ms = 10;
            traceWriter.ListenerEnabled = true;
            
            //Normal Mode
            WriteTestStrings(1, 1000);
            Assert.AreEqual(TraceFileWriterMode.Normal, traceWriter.OperationMode);

            //Backup Mode
            traceWriter.OutputFile = OutputFileInvalid;
            WriteTestStrings(1001, 2000, "Backup Mode");
            Assert.AreEqual(TraceFileWriterMode.Backup, traceWriter.OperationMode);

            //Recovery Mode
            traceWriter.OutputFile = recoveryOutputFile;
            WriteTestStrings(2001, 3000);
            Assert.AreEqual(TraceFileWriterMode.Normal, traceWriter.OperationMode);

            traceWriter.ListenerEnabled = false;
            traceWriter.ShowTimeStamp = false;

            Assert.AreEqual(3000, File.ReadAllLines(OutputFile).Count());
        }
    }
}
