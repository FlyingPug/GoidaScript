using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Анализатор_лексем
{
    public static class STR
    {
        private static string _string = "";
        private static void END()
        {
            _string += InputData.Current;
            InputData.lexems.Add(("STRING", _string));
            InputData.Pointer++;
            //Console.WriteLine($"Распознан строковый литерал: {_string}';");
            _string = "";
        }

        public static void Analyse()
        {
            _string += InputData.Current;
            InputData.Pointer++;

            if (InputData.Pointer >= InputData.Data.Length) throw new Exception($"неожиданное окончание файла {_string}");

            switch (InputData.CurentCharGroup())
            {
                case "<'>": END(); break;
                default:
                    Analyse(); break;
            }
        }
    }
}
