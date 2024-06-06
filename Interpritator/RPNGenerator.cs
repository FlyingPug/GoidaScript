using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

    public class RPNGenerator
    {
        // input - вход из лексического анализатора
        public static List<RPNTerminal> GenerateOps(Queue<Terminal> input)
        {
            List<RPNTerminal> rPNTerminals = new List<RPNTerminal>();
            Stack<Token> store = new(); // магазин
            Stack<Terminal> generator = new();

            store.Push(new Z());
            store.Push(new Program());
            
            generator.Push(new EmptyTerminal());
            generator.Push(new EmptyTerminal());

            Logger.Log($"[Генератор ОПС] начинается формирование ОПС", 1);
            while (store.Count > 1)
            {
                Token currentToken = store.Pop();
                Terminal currentTerminal = generator.Pop();
                Logger.Log($"[Генератор ОПС] работаю сейчас с {currentToken.GetType().Name}, из генератора беру - {currentTerminal.Type} текущий символ - {(input.Count > 0 ? input.Peek().Type : 0)} ", 3);
                if (currentTerminal.GetType().IsSubclassOf(typeof(RPNTerminal)))
                {
                    Logger.Log($"[Генератор ОПС] добавляется {currentTerminal.Type} {currentTerminal.GetType().Name}", 2);
                    rPNTerminals.Add((RPNTerminal)currentTerminal);
                }
                
                if (currentToken.GetType().IsSubclassOf(typeof(NotTerminal)))
                {
                    var term = input.Count > 0 ? input.Peek() : new Terminal(Terminal.TerminalType.Empty);
                    Stack<Tuple<Token, Terminal>> toAdd = ((NotTerminal)currentToken).Evaluate(term);

                    if (toAdd.First().Item2.Type != Terminal.TerminalType.Lambda)
                    {
                        foreach (var item in toAdd)
                        {
                            store.Push(item.Item1);
                            generator.Push(item.Item2);
                            Logger.Log($"[Генератор ОПС] добавляю в магазин {item.Item1.GetType().Name} а в генератор - {item.Item2.Type}", 3);
                        }
                    }
                }
                else if(currentToken.GetType().IsSubclassOf(typeof(Terminal)) || currentToken.GetType() == typeof(Terminal))
                { 
                    if(input.Count > 0)
                    {

                        var inp = input.Dequeue();
                    }
                }
                else
                {
                    throw new Exception($"чзх {currentToken.GetType()}");
                }
            }
            Logger.Log($"[Генератор ОПС] генерация опс завершена", 1);

            return rPNTerminals;
        }
    }

    [Serializable]
    internal class WrongTerminalException : Exception
    {
        public WrongTerminalException()
        {
        }

        public WrongTerminalException(string? message) : base(message)
        {
        }

        public WrongTerminalException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WrongTerminalException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
