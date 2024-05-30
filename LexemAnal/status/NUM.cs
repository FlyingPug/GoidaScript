using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Анализатор_лексем
{
    public static class NUM
    {
        private static int number = 0;
        private static void END()
        {
            InputData.Pointer++;
            // Console.WriteLine($"Распознано число: {number};");
            InputData.lexems.Add(("NUMBER", number.ToString()));
            number = 0;
        }
        private static void END_minus()
        {
            //Console.WriteLine($"Распознано число: {number};");
            InputData.lexems.Add(("NUMBER", number.ToString()));
            number = 0;
        }
        public static void Analyse()
        {
            number = number * 10 + (InputData.Current - '0');
            InputData.Pointer++;

            if (InputData.Pointer >= InputData.Data.Length) throw new Exception($"неожиданное окончание файла при чтении числа");


            switch (InputData.CurentCharGroup())
            {
                case "<ц>":  Analyse(); break;
                case "<б>":  throw new Exception("Недопустимый символ");
                case "< >":  END(); break;
                case "<'>": throw new Exception("Недопустимый символ");
                case "<,>": 
                case "<;>":  
                case "<с>": 
                case ">,<":  
                case "<=>":  END_minus(); break;
                default:     throw new Exception("Недопустимый символ");
            }
        }
    }
}
