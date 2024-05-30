using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Interpritator
{
    internal static class QueueExtensions
    {
        public static void AddRange<T>(this Queue<T> queue, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                queue.Enqueue(item);
            }
        }
    }

    // дикий папуанский танк
    public class RPNGenerator
    {
        // input - вход из лексического анализатора
        public static List<RPNTerminal> GenerateOps(Stack<Terminal> input)
        {
            List<RPNTerminal> rPNTerminals = new List<RPNTerminal>();
            Stack<Token> store = new(); // магазин
            Stack<Terminal> generator = new();

            store.Push(new Program());
            store.Push(new Z());

            generator.Push(new EmptyTerminal());
            generator.Push(new EmptyTerminal());

            Logger.Log($"[Генератор ОПС] начинается формирование ОПС");
            while (store.Count > 1)
            {
                Token currentToken = store.Pop();
                Terminal currentTerminal = generator.Pop();

                if (currentTerminal.GetType() == typeof(RPNTerminal))
                {
                    Logger.Log($"[Генератор ОПС] добавляется {currentTerminal.Type}");
                    rPNTerminals.Add((RPNTerminal)currentTerminal);
                }
                
                if (currentToken.GetType() == typeof(NotTerminal))
                {
                    List<(Token, Terminal)> toAdd = ((NotTerminal)currentToken).Evaluate(input.Peek());

                    if (toAdd.First().Item2.Type != Terminal.TerminalType.Lambda)
                    {
                        foreach (var item in toAdd)
                        {
                            store.Push(item.Item1);
                            generator.Push(item.Item2);
                        }
                    }
                }
                else
                {
                    input.Pop();
                }
            }
            Logger.Log($"[Генератор ОПС] генерация опс завершена");

            return rPNTerminals;
        }
    }
}
