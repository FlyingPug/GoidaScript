using Interpritator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Interpritator.Terminal;

namespace Interpritator
{

    /*
     * Класс Terminal нужен нам чисто для генерации ОПС, сам он в себе логики не несет
     * Его наследники в свою очередь находятся в опс и каждый из них имеет в себе
     * какую-то цель, которую при обработке самой ОПС мы будет выполнять. Очень важно,
     * чтобы при выводе из генератора в опс, в опс находились только те терминалы, которые
     * в себе что-то несут. 
     * Чисто логически, не стоит наследоваться от Terminal, а сделать
     * отдельный класс RPNCharacter, но тогда нужно будет переписывать генерацию ОПС
     * */

    public class Terminal : Token
    {
        public enum TerminalType
        {
            Lambda = 0,
            Empty,
            Number,
            Integer,
            Boolean,
            String,
            Identifier,
            Semicolon,
            OpenBracket,
            OpenSquareBracket,
            OpenFigureBracket,
            CloseBracket,
            CloseSquareBracket,
            CloseFigureBracket,
            Equal,
            AdditionOperation,
            MultiplieOperation,
            CompareOperaion,
            MoreCompareOperation,
            LessCompareOperation,
            MoreEqualCompareOperation,
            LessEqualCompareOperation,
            EqualCompareOperation,
            OrOperation,
            AndOperation,
            NotOperation,
            IfStatement,
            WhileStatement,
            ImbededFunction,
            Comma,
            Line,
            BooleanValue,
            Program,
            Else
        }

        public Terminal(TerminalType _type)
        {
            this.Type = _type;
        }

        public TerminalType Type;
    }

    public class EmptyTerminal : Terminal
    {
        public EmptyTerminal() : base(TerminalType.Empty)
        {
        }
    }

    // терминалы, которые могут быть в ОПС (скобки, запятые и тд там напрочь не нужны)
    public class RPNTerminal : Terminal
    {
        public RPNTerminal(TerminalType type) : base(type)
        {
        }
    }

    public class ValueTerminal : RPNTerminal
    {
        private readonly IConvertible _value;

        public ValueTerminal(TerminalType type, IConvertible value) : base(type)
        {
            _value = value;
        }

        public IConvertible Value { get { return _value; } }
    }

    public class IdentifierTerminal : ValueTerminal
    {
        public IdentifierTerminal(string value) : base(TerminalType.Identifier, value) {}
    }

    public abstract class OperationTerminal : RPNTerminal
    {
        public OperationTerminal(TerminalType type) : base(type) {}

        public abstract void doOperation(Context context);
    }

    public class Program1 : OperationTerminal
    {
        public Program1() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.EnterArea();
        }
    }

    public class Program2 : OperationTerminal
    {
        public Program2() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartElse();
        }
    }

    public class Program3 : OperationTerminal
    {
        public Program3() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.CloseElse();
        }
    }

    public class Program4 : OperationTerminal
    {
        public Program4() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartWhile();
        }
    }

    public class Program5 : OperationTerminal
    {
        public Program5() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.CloseWhile();
        }
    }


    public class Program6 : OperationTerminal
    {
        public Program6() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.PushValueFromArrayToStack();
        }
    }

    

    public class Program7 : OperationTerminal
    {
        public Program7() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartSaveArrayValue();
        }
    }

    public class Program9 : OperationTerminal
    {
        public Program9() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartSaveIdentf();
        }
    }

    public class Program10 : OperationTerminal
    {
        public Program10() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartSaveValue();
        }
    }

    public class Program11 : OperationTerminal
    {
        public Program11() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartSaveArray();
        }
    }

    public class Program12 : OperationTerminal
    {
        public Program12() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartSaveIdentf();
        }
    }

    public class Program13 : OperationTerminal
    {
        public Program13() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.StartSaveIdentf();
        }
    }

    public class Program14 : OperationTerminal
    {
        public Program14() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.FinishSaveVariable();
        }
    }

    public class Program16 : OperationTerminal
    {
        public Program16() : base(TerminalType.Program)
        {
        }

        public override void doOperation(Context context)
        {
            context.PushValueFromArrayToStack();
        }
    }

    public class PlusTerminal : OperationTerminal
    {
        public PlusTerminal() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.PlusOperation();

        }
    }

    public class MinusTerminal : OperationTerminal
    {
        public MinusTerminal() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.MinusOperation();

        }
    }

    public class MultiplieTerminal : OperationTerminal
    {
        public MultiplieTerminal() : base(TerminalType.MultiplieOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.MultiplieOperation();

        }
    }

    public class DivideTerminal : OperationTerminal
    {
        public DivideTerminal() : base(TerminalType.MultiplieOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.DivideOperation();

        }
    }

    // это присваивание ебалай
    public class EqualTerminal : OperationTerminal
    {
        public EqualTerminal() : base(TerminalType.Equal)
        {
        }

        public override void doOperation(Context context)
        {
            context.AssignmentOperation();
        }
    }

    // какой же это костыль, но я уже не знаю что делать
    public class CompareTerminal : OperationTerminal
    {
        public CompareTerminal(TerminalType terminalType) : base(terminalType)
        {
        }

        public override void doOperation(Context context)
        {
            context.CompareOperation(this.Type);

        }
    }

    public class OrTerminal : OperationTerminal
    {
        public OrTerminal() : base(TerminalType.OrOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.OrOperation();

        }
    }

    public class AndTerminal : OperationTerminal
    {
        public AndTerminal() : base(TerminalType.AndOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.AndOperation();
        }
    }

   

    public class NotButTerminal : OperationTerminal
    {
        public NotButTerminal() : base(TerminalType.NotOperation)
        {
        }

        public override void doOperation(Context context)
        {
            context.NotOperation();

        }
    }

    public class PrintTerminal : OperationTerminal
    {
        public PrintTerminal() : base(TerminalType.ImbededFunction)
        {

        }

        public override void doOperation(Context context)
        {
            context.PrintOperation();
        }
    }

    public class InputTerminal : OperationTerminal
    {
        public InputTerminal() : base(TerminalType.ImbededFunction)
        {
        }

        public override void doOperation(Context context)
        {
            context.WriteOperation();
            
        }
    }
}
