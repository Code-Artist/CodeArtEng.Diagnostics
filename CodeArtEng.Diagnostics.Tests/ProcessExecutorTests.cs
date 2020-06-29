using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeArtEng.Diagnostics.Tests
{
    [TestFixture]
    class ProcessExecutorTests
    {
        [Test]
        public void ExecuteConsoleCommand()
        {
            ProcessExecutor executor = new ProcessExecutor();
            executor.ShowConsoleWindow = true;
            executor.Application = "cmd.exe";
            executor.Arguments = "/c echo Test";
            ProcessResult result = executor.Execute();
            Assert.AreEqual(0, result.ExitCode);
        }

        [Test]
        public void ExecuteConsoleCommand_Abort()
        {
            ProcessExecutor executor = new ProcessExecutor();
            executor.ShowConsoleWindow = false;
            executor.Application = "notepad.exe";
            //executor.Arguments = "/k timeout 50";
            ProcessResult result = executor.Execute(false);
            System.Threading.Thread.Sleep(1000);
            Assert.IsFalse(executor.HasExited);
            executor.Abort();
            Assert.IsTrue(executor.HasExited);
        }

        [Test]
        public void ExceuteProcessAsAdmin()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            appPath = Path.GetFullPath(Path.Combine(appPath, "./../../../IsAdminCheck/bin/Debug/IsAdminCheck.exe"));
            ProcessExecutor executor = new ProcessExecutor();
            executor.ShowConsoleWindow = true;
            executor.Application = appPath;
            executor.RunAsAdministrator = true;
            ProcessResult result = executor.Execute(true);
            Assert.IsTrue(executor.HasExited);
            Assert.AreEqual(0, result.ExitCode);
        }
    }
}
