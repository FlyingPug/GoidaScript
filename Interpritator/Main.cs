using Interpritator.output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    internal class Interp
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            
            ConsoleOutput output = new ConsoleOutput();

            Interpreter interpreter = new Interpreter(output);

            interpreter.Interpret(input);

        }
    }
}
