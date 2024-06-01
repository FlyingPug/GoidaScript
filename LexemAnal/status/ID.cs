using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Анализатор_лексем
{
    public static class ID 
    {
        private static readonly List<string> _reservedWords = new List<string>() { "while", "int", "string", "bool", "print", "input", "if", "else", "or", "not", "and" };
        private static string _name = "";
        private static void END()
        {
            InputData.Pointer++;
            END_minus();
        }

        private static void END_minus()
        {
            if (_reservedWords.Contains(_name))
                InputData.lexems.Add(("SYSTEM_WORD", _name));
            //Console.WriteLine($"Распознано зарезервированное слово: {_name};");
            else
                InputData.lexems.Add(("IDENTIFIER", _name));
            //Console.WriteLine($"Распознан идентификатор: {_name};");

            _name = "";
        }

        public static void Analyse()
        {
            _name += InputData.Current;
            InputData.Pointer++;

            if (InputData.Pointer >= InputData.Data.Length) throw new Exception($"неожиданное окончание файла при чтении name");

            var currentGroup = InputData.CurentCharGroup();
            switch (currentGroup)
            {
                case "<ц>": Analyse(); break;
                case "<б>": Analyse(); break;
                case "< >": END(); break;
                case "<'>": throw new Exception("Недопустимый символ");
                case "<,>": END_minus(); break;
                case "<;>": END_minus(); break;
                case "<c>": END_minus(); break;
                case ">,<": END_minus(); break;
                case "<=>": END_minus(); break;
                default: throw new Exception($"Недопустимый символ {currentGroup}");
            }
        }
    }
}
