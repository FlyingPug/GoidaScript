using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Анализатор_лексем
{
    public static class S 
    {
        public static void END(string data)
        {
           // Console.WriteLine($"Распознана лексема: <{InputData.Current}> - {data};");
            InputData.lexems.Add((data, InputData.Current.ToString()));
            InputData.Pointer++;
        }

        public static void Analyse(string data)
        {
            InputData.Data = data;
            while (InputData.Pointer < InputData.Data.Length)
            {
                string current = InputData.CurentCharGroup();
                switch (current)
                {
                    case "<ц>":
                        NUM.Analyse();
                        break;

                    case "<б>":
                        ID.Analyse();
                        break;

                    case "< >":
                        InputData.Pointer++;
                        break;

                    case "<'>":
                        STR.Analyse();
                        break;

                    case "<,>":
                        END("COMMA");
                        break;

                    case "<;>":
                        END("LINE_END");
                        break;

                    case "<c>":
                        END($"{InputData.Current}");
                        break;

                    case ">,<":
                        COMP.Analyse();
                        break;

                    case "<=>":
                        END("EQUAL");
                        break;
                    default:
                        throw new Exception($"Недопустимый символ. {current}");
                }
            }

        }
    }
}
