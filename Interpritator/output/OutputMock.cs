using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator.output
{
    public class OutputMock : IOutput
    {
        private string _input;
        public OutputMock(string input)
        {
            this._input = input;
        }

        private StringBuilder output = new();

        public string Output { get { return output.ToString(); } }
        public string Input { set { _input = value; } }

        public void Print(string message)
        {
            output.AppendLine(message);
        }

        public string ReadAll()
        {
            return _input;
        }

        private string[]? lines = null;
        private int currentIndex = 0;

        public string ReadLine()
        {
            if (lines == null) { lines = _input.Split("\n"); }

            if (currentIndex > lines.Length) return "";
            
            return lines[currentIndex++];
        }

        public void PrintLine(string message)
        {
            output.Append(message);
        }
    }
}
