using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator.output
{
    public class ConsoleOutput : IOutput
    {
        public void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

        void IOutput.Print(string message)
        {
            Console.Write(message);
        }

        string IOutput.ReadAll()
        {
            StringBuilder input = new();

            int ms;
            while((ms = Console.Read()) != -1)
            {
                input.Append(Convert.ToChar(ms));   
            }

            return input.ToString();
        }

        string IOutput.ReadLine()
        {
            return Console.ReadLine() ?? "";
        }
    }
}
