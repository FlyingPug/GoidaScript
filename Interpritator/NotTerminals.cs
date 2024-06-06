using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    public abstract class NotTerminal : Token
    {
        public abstract Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal);
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
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();
       
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.IfStatement:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.IfStatement),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlockIf(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ElseBlock(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.WhileStatement:
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.WhileStatement),
                       new Program4()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlockWhile(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                         new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                       currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Integer:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Integer),
                        new Program9()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Integer(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Empty),
                        new Program14()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.String:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.String),
                        new Program12()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new StringNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Empty),
                        new Program14()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));                   
                    break;
                case Terminal.TerminalType.Boolean:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Boolean),
                        new Program13()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalVariable(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Empty),
                        new Program14()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));                   
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
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

    internal class ElseBlock : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Else:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Program2()));
                    generator.Push(new Tuple<Token, Terminal>(
                         new InstructionBlockElse(),
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
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.IfStatement:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.IfStatement),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlockIf(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ElseBlock(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.WhileStatement:
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.WhileStatement),
                       new Program4()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlockWhile(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        currentTerminal));
                    break;
                case Terminal.TerminalType.Integer:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Integer),
                        new Program9()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Integer(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Empty),
                        new Program14()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.String:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.String),
                        new Program12()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new StringNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Empty),
                        new Program14()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Boolean:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Boolean),
                        new Program13()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalVariable(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Empty),
                        new Program14()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
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

    internal class ReadIdentifier : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class InstructionID : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new OperationStreak(),
                       currentTerminal));
                    break;
                case Terminal.TerminalType.OpenSquareBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
                       currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.CloseSquareBracket),
                       new Program7()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Equal),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                        new EqualTerminal()));
                    break;
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new ExpressionNT(),
                       new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                        currentTerminal));
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

    internal class InstructionBlockIf : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenFigureBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
                       new Program1()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.CloseFigureBracket),
                       new Terminal(Terminal.TerminalType.Empty)));
                    break;            
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class InstructionBlockElse : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenFigureBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
                       new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.CloseFigureBracket),
                       new Program3()));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class InstructionBlockWhile : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenFigureBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
                       new Program1()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.CloseFigureBracket),
                       new Program5()));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class Integer : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenSquareBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Program11()));

                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Number),
                        new Terminal(Terminal.TerminalType.Number)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseSquareBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new IntegerOptions(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Program10()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
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

    internal class IntegerOptions : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenFigureBracket), 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new Enumeration(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseFigureBracket),
                        currentTerminal));
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

    internal class StringReadBowlOfRice : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Line:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                default:
                    throw new Exception("sheise");
            }
            return generator;
        }
    }

    internal class StringNT : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new StringReadBowlOfRice(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                        currentTerminal));
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

    internal class LogicalVariable : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal, 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpression(),
                        currentTerminal));
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

    internal class Enumeration : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EnumerationNumber(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class EnumerationNumber : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Comma:
                    generator.Push(new Tuple<Token, Terminal>(
                        new EmptyTerminal(),
                        new EmptyTerminal()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Enumeration(),
                        new Terminal(Terminal.TerminalType.Number)));
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

    internal class ExpressionNT : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new EmptyTerminal()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),// че?????
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        currentTerminal));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
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

    internal class OperationID : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenSquareBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new Terminal(Terminal.TerminalType.CloseBracket),
                        new Program6()));
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

    internal class ExpressionID : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(), 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        currentTerminal));
                    break;
                case Terminal.TerminalType.OpenSquareBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new Terminal(Terminal.TerminalType.CloseBracket),
                        new Program6()));
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
    
    internal class OperationStreak : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.AdditionOperation:                
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        currentTerminal));
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

    internal class MultiplieOperation : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class MultiplieOperationStreak : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        currentTerminal));
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

    internal class PriorityOpereation : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new EmptyTerminal()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                case Terminal.TerminalType.Line:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class CheckMinusTerminal : NotTerminal
    {
        private Terminal currentTerminal;

        public CheckMinusTerminal(Terminal currentTerminal)
        {
            this.currentTerminal = currentTerminal;
        }

        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Number:

                    ((ValueTerminal)currentTerminal).Value = -(int)((ValueTerminal)currentTerminal).Value;

                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                default: throw new NotImplementedException();

            }
            return generator;
        }
    }

    internal class LogicalExpression : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            var compare = new CompareTerminal(Terminal.TerminalType.CompareOperaion);
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.NotOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpression(), 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(), 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        currentTerminal));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                       compare));
                    break;
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new EmptyTerminal()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        compare));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    

                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        compare));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        currentTerminal));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    } 

    internal class LogicalExpressionStreak : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OrOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal, 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new LogicalExpressionStreak(),
                        currentTerminal));
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

    internal class PriorityLogicalExpression : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            var compare = new CompareTerminal(Terminal.TerminalType.CompareOperaion);
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.NotOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new EvenHigherPriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        currentTerminal));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        compare));
                    break;
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        compare));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                        compare));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                       currentTerminal));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class PriorityLogicalExpressionStreak : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.AndOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new HigherPriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        currentTerminal));
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

    internal class HigherPriorityLogicalExpression : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            var compare = new CompareTerminal(Terminal.TerminalType.CompareOperaion);
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.NotOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpression(),
                        currentTerminal));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));
                    break;
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));

                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        compare));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class EvenHigherPriorityLogicalExpression : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            var compare = new CompareTerminal(Terminal.TerminalType.CompareOperaion);
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));
                    break;
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        compare));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class EvenHigherPriorityLogicalExpressionID : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            var compare = new CompareTerminal(Terminal.TerminalType.CompareOperaion);
            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(), 
                        new Terminal(Terminal.TerminalType.Empty)));;
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(), 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(), 
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(compare),
                        compare));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                         compare));
                    break;
                case Terminal.TerminalType.OpenSquareBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                         new Terminal(Terminal.TerminalType.CloseBracket),
                        new Program6()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.LessCompareOperation:
                case Terminal.TerminalType.MoreCompareOperation:
                case Terminal.TerminalType.MoreEqualCompareOperation:
                case Terminal.TerminalType.LessEqualCompareOperation:
                case Terminal.TerminalType.EqualCompareOperation:
                case Terminal.TerminalType.CompareOperaion:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                        currentTerminal)); ;
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

    internal class Compare : NotTerminal
    {
        private CompareTerminal _terminal;

        public Compare(CompareTerminal terminal) 
        {
            this._terminal = terminal;
        }

        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.LessCompareOperation:
                case Terminal.TerminalType.MoreCompareOperation:
                case Terminal.TerminalType.MoreEqualCompareOperation:
                case Terminal.TerminalType.LessEqualCompareOperation:
                case Terminal.TerminalType.EqualCompareOperation:
                case Terminal.TerminalType.CompareOperaion:
                    _terminal.Type = currentTerminal.Type;

                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Lambda)));
                    break;
                default:
                    throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
            }
            return generator;
        }
    }

    internal class Arguments : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty))); ;
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ArgumentsList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ArgumentsList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.AdditionOperation:
                    if (currentTerminal.GetType() == typeof(MinusTerminal))
                    {
                        generator.Push(new Tuple<Token, Terminal>(
                            new Terminal(Terminal.TerminalType.Empty),
                            new Terminal(Terminal.TerminalType.Empty)));
                        generator.Push(new Tuple<Token, Terminal>(
                            new CheckMinusTerminal(currentTerminal),
                            new Terminal(Terminal.TerminalType.Empty)));
                    }
                    else throw new NotImplementedException($"не должен быть сейчас данный терминал {currentTerminal}");
                    break;
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ArgumentsList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Line:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ArgumentsList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ArgumentsList(),
                        currentTerminal));
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

    internal class ArgumentsList : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.Comma:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ArgumentsList(),
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
    internal class Z : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            generator.Push(new Tuple<Token, Terminal>(
                new Terminal(Terminal.TerminalType.Lambda),
                new Terminal(Terminal.TerminalType.Lambda)));

            return generator;
        }
    }
}
