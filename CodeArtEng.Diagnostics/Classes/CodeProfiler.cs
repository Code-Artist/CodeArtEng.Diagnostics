using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeArtEng.Diagnostics
{
    /// <summary>
    /// Code performance monitor tool.
    /// </summary>
    public sealed class CodeProfiler
    {
        private static Dictionary<string, DateTime> TimeKeeper = new Dictionary<string, DateTime>();

        //Private constructor for static class
        private CodeProfiler() { }
        
        /// <summary>
        /// Record start time with reference serverPath.
        /// Reset start time for named item when called each time.
        /// </summary>
        /// <param serverPath="serverPath"></param>
        [Conditional("CodeProfiler")]
        static public void Start(string name){ TimeKeeper[name] = DateTime.Now; }
        /// <summary>
        /// Record stop time and calculate execution duration by unique reference serverPath.
        /// Call <c>Stop</c> before <c>Start</c> may get unexpected value
        /// </summary>
        /// <param serverPath="serverPath"></param>
        [Conditional("CodeProfiler")]
        static public void Stop(string name)
        {
            DateTime tTime = DateTime.Now;
            if (TimeKeeper.ContainsKey(name))
            {
                Trace.WriteLine(name + " completed in " + (tTime - TimeKeeper[name]).TotalMilliseconds.ToString("F4") + " ms");
            }
            else
            {
                Trace.WriteLine("CodeProfiler: Item [" + name + "] not exists.", "WARNING");
            }
        }
        /// <summary>
        /// Clear all recorded time.
        /// </summary>
        [Conditional("CodeProfiler")]
        static public void Clear() { TimeKeeper.Clear(); }
        /// <summary>
        /// Print debug message when CodeProfiler is switched ON.
        /// </summary>
        /// <param serverPath="message"></param>
        [Conditional("CodeProfiler")]
        static public void WriteDebugMessage(string message) { Trace.WriteLine(message); }
        /// <summary>
        /// Dump array to selected file when CodeProfiler is switched ON.
        /// </summary>
        /// <param name="targetFile"></param>
        /// <param name="title"></param>
        /// <param name="datas"></param>
        [Conditional("CodeProfiler")]
        static public void DumpArray(string targetFile, string title, double[] datas)
        {
            if (targetFile.EndsWith(".txt", StringComparison.CurrentCultureIgnoreCase) == false) targetFile += ".txt";
            List<string> lines = new List<string>();
            lines.Add(title);
            for (int x = 0; x < datas.Length; x++)
                lines.Add(datas[x].ToString());
            System.IO.File.WriteAllLines(targetFile, lines.ToArray());
        }
    }
}
