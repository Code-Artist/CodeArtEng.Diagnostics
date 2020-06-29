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

    }
}
