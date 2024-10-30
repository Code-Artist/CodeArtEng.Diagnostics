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
            Assert.That(result.ExitCode,Is.EqualTo(0));
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
            Assert.That(executor.HasExited,Is.False);
            executor.Abort();
            Assert.That(executor.HasExited,Is.True);
        }

        [Test]
        public void ExceuteProcessAsAdmin()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
#if NET48 || NET8_0
            appPath = Path.GetFullPath(Path.Combine(appPath, "./../../../../IsAdminCheck/bin/Debug/net48/IsAdminCheck.exe"));
#endif

#if NET6_0
            appPath = Path.GetFullPath(Path.Combine(appPath, "./../../../../IsAdminCheck/bin/Debug/net6.0-windows/IsAdminCheck.exe"));
#endif

            ProcessExecutor executor = new ProcessExecutor();
            executor.ShowConsoleWindow = true;
            executor.Application = appPath;
            executor.RunAsAdministrator = true;
            ProcessResult result = executor.Execute(true);
            Assert.That(executor.HasExited,Is.True);
            Assert.That(result.ExitCode,Is.EqualTo(0));
        }
    }
}
