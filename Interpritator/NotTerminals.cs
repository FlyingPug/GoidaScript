using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interpritator
{
    /*
     * В генератор мы должны запихнуть лишь два типа терминалов
     * Пустышки, которые мы потом будем просто убирать,
     * Терминалы, которые затем будут перенесены в ОПС, а это могут быть
     * идентификаторы, которые у нас хранятся в виде строки
     * терминалы, хранящие значение для записи (это различные числа, строки, boolean)
     * операции, это может быть как +, -, так и print, program11, program16
     * a 5 + print - пример нашей опс
     * всякие терминалы типа скобочек, мы не учитываем, поэтому в генератор вместо них
     * мы как и вместо терминалов должны занести new Terminal(Terminal.TerminalType.Empty),
     * но, когда мы получаем на вход терминал, которые должны быть перенесены в ОПС (a 5 + print),
     * мы должны сохранить в генераторе именно этот терминал currentTerminal, а не создавать
     * new Terminal(Terminal.TerminalType.Number), потому что тогда мы потеряем значение или операцию,
     * которую несет терминал.
     * 
     * Т.Е
     * Заменить
     * generator.Push(new Tuple<Token, Terminal>(
        new Terminal(Terminal.TerminalType.MultiplieOperation),
        new Terminal(Terminal.TerminalType.MultiplieOperation)));
       break;
    на
    generator.Push(new Tuple<Token, Terminal>(
        currentTerminal,
        currentTerminal));
       break;
     * */

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
                // good
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
                // 
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
                        new InstructionBlock(),
                        new Program1()));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Else),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlock(),
                        new Program3()));
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
                        new Program1(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlock(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program5(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Integer:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program16(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program9(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Integer(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.String:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program16(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program12(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new StringNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));                   
                    break;
                case Terminal.TerminalType.Boolean:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program16(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program13(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalVariable(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch(currentTerminal.Type)
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
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlock(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program1(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlock(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program3(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.WhileStatement:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program4(),
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
                        new Program1(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionBlock(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program5(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Integer:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program16(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program9(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Integer(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.String:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program16(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program12(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new StringNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Boolean:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program16(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program13(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ReadIdentifier(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalVariable(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                    throw new NotImplementedException();
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
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
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
                       new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Equal),
                        new Terminal(Terminal.TerminalType.Equal)));
                    break;
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
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

    internal class InstructionBlock : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.OpenFigureBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                       currentTerminal,
                       currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new InstructionList(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                       new Terminal(Terminal.TerminalType.CloseFigureBracket),
                       new Terminal(Terminal.TerminalType.Empty)));
                    break;            
                default:
                    throw new NotImplementedException();
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
                        new Program11(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Number),
                        new Terminal(Terminal.TerminalType.Number)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseSquareBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new IntegerOptions(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.Equal:
                    generator.Push(new Tuple<Token, Terminal>(
                        new IntegerEqual(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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

    internal class IntegerEqual : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {               
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program10(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.MultiplieOperation),
                        new Terminal(Terminal.TerminalType.MultiplieOperation)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.OpenSquareBracket:
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program11(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Z(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseFigureBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Program14(),
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
                        new Enumeration(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.Equal),
                        new Terminal(Terminal.TerminalType.Equal)));
                    break;
                default:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
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
                        new Terminal(Terminal.TerminalType.Line),
                        new Terminal(Terminal.TerminalType.Line)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                        new LogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new EnumerationNumber(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException();
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
                        currentTerminal,
                        currentTerminal));
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
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Identifier)));
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
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.OpenBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Arguments(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Terminal(Terminal.TerminalType.CloseBracket),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                        new ExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException();
            }
            return generator;
        }
    }

    internal class ExpressionID : Token
    {
        public Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
    
    internal class OperationStreak : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.AdditionOperation:                
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                    break;
                case Terminal.TerminalType.Identifier:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                    throw new NotImplementedException();
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
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                        currentTerminal));
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
                case Terminal.TerminalType.Number:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                default:
                    throw new NotImplementedException();
            }
            return generator;
        }
    }

    internal class LogicalExpression : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.NotOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException();
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
                        new PriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new LogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.NotOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException();
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
                        new HigherPriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityLogicalExpressionStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.NotOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpression(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException();
            }
            return generator;
        }
    }

    internal class EvenHigherPriorityLogicalExpression : NotTerminal
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
                    generator.Push(new Tuple<Token, Terminal>(
                        new EvenHigherPriorityLogicalExpressionID(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    break;
                default:
                    throw new NotImplementedException();
            }
            return generator;
        }
    }

    internal class EvenHigherPriorityLogicalExpressionID : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.MultiplieOperation:
                    generator.Push(new Tuple<Token, Terminal>(
                        new PriorityOpereation(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new MultiplieOperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));;
                    generator.Push(new Tuple<Token, Terminal>(
                        new OperationStreak(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new Compare(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        new ExpressionNT(),
                        new Terminal(Terminal.TerminalType.Empty)));
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
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

    internal class Compare : NotTerminal
    {
        public override Stack<Tuple<Token, Terminal>> Evaluate(Terminal currentTerminal)
        {
            Stack<Tuple<Token, Terminal>> generator = new();

            switch (currentTerminal.Type)
            {
                case Terminal.TerminalType.CompareOperaion:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
                    break;
                default:
                    throw new NotImplementedException();
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
                case Terminal.TerminalType.ImbededFunction:
                    generator.Push(new Tuple<Token, Terminal>(
                        currentTerminal,
                        currentTerminal));
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
                        currentTerminal));
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