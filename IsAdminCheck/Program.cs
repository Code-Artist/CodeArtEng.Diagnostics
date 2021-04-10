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
                if (CodeArtEng.Diagnostics.Utility.IsRunAsAdmin())
                {
                    Console.WriteLine("Application in admin mode!");
                    Console.WriteLine("Arguments = " + string.Join(" ", Environment.GetCommandLineArgs().Skip(1)));
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                //else Environment.Exit(1);
                else CodeArtEng.Diagnostics.Utility.RestartApplicationAsAdmin("Custom Arguments" );
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ERROR: " + ex.ToString());
                Environment.Exit(-1);
            }
        }
    }
}
