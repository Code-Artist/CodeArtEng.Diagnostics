using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// Collection of shared utility functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Check if process current process is run as Administrator.
        /// </summary>
        /// <returns></returns>
        public static bool IsRunAsAdmin()
        {
            try
            {
                using (WindowsIdentity identify = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identify);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Restart application in adminstrator mode
        /// </summary>
        /// <param name="arguments">Command line argument for restart instance. Leave blank to use current startup arguments</param>
        public static void RestartApplicationAsAdmin(string arguments = null)
        {
            ProcessExecutor executor = new ProcessExecutor();
            executor.RunAsAdministrator = true;
            executor.Application = Environment.GetCommandLineArgs()[0];
            if (string.IsNullOrEmpty(arguments))
            {
                executor.Arguments = string.Join(" ", Environment.GetCommandLineArgs().Skip(1));
            }
            else executor.Arguments = arguments;
            executor.ShowConsoleWindow = true;
            executor.Execute(false);
            Environment.Exit(0);
        }

    }
}
