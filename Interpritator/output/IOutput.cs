using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator.output
{
    public interface IOutput
    {
        public void Print(string message);

        public void PrintLine(string message);

        public string ReadAll();

        public string ReadLine();
    }
}
