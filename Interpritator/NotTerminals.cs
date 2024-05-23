using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    public abstract class NotTerminal : Token
    {
        public List<(Token, RPNAction)> Evaluate(Terminal currentTerminal)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    internal class TerminalNotFoundException : Exception
    {
        public TerminalNotFoundException()
        {
        }

        public TerminalNotFoundException(string? message) : base(message)
        {
        }

        public TerminalNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TerminalNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class Program : NotTerminal
    {
        public Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();
       
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    break;
                case Terminal.TerminalType.IfStatement:
                    break;
                case Terminal.TerminalType.WhileStatement: 
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    break;
                case Terminal.TerminalType.Integer:
                    break;
                case Terminal.TerminalType.String:
                    break;
                case Terminal.TerminalType.Boolean:
                    break;
                case Terminal.TerminalType.Identificator:
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Number),
                        new Terminal(Terminal.TerminalType.Number)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Lambda),
                        new Terminal(Terminal.TerminalType.Lambda)));
                    break;
            }
            return generator;
        }
    }

    internal class InstructionList : NotTerminal
    {
    }

    internal class OperationStreak : NotTerminal
    {
    }

    internal class MultiplieOperation : NotTerminal
    {
    }
}
