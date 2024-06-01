using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    public static class Logger
    {
        public static int LogLevel { get; set; } = 3;

        public static void Log(string output, int logLevel = 1)
        {
            if (logLevel <= LogLevel)
            {
                Console.WriteLine($"[Log - {DateTime.Now.ToString("HH:mm:ss tt")}]{output}");
            }
        }
    }
}
