using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    public static class Logger
    {
        public static bool Enabled { get; set; } = true;

        public static void Log(string output)
        {
            if (Enabled)
            {
                Console.WriteLine($"[Log - {DateTime.Now.ToString("HH:mm:ss tt")}]{output}");
            }
        }
    }
}
