using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsAdminCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Admin=" + CodeArtEng.Diagnostics.Utility.IsRunAsAdmin().ToString());
                if (CodeArtEng.Diagnostics.Utility.IsRunAsAdmin()) Environment.Exit(0);
                else Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ERROR: " + ex.ToString());
                Environment.Exit(-1);
            }
        }
    }
}
