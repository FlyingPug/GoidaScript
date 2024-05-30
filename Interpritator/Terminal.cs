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
            OrOperation,
            AndOperation,
            NotOperation,
            IfStatement,
            WhileStatement,
            ImbededFunction,
            Comma,
            Line,
            BooleanValue,
            Else
        }

        public Terminal(TerminalType _type)
        {
            this.Type = _type;
        }

        public readonly TerminalType Type;
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

    public class IdentifierTerminal : RPNTerminal
    {
        private readonly string _name;

        public IdentifierTerminal(string value) : base(TerminalType.Identifier)
        {
            _name = value;
        }

        public string Name { get { return _name; } }
    }

    public abstract class OperationTerminal : RPNTerminal
    {
        public OperationTerminal(TerminalType type) : base(type)
        {
        }

        public abstract void doOperation(Context context);
    }

    public class Program1 : OperationTerminal
    {
        public Program1() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program3 : OperationTerminal
    {
        public Program3() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program4 : OperationTerminal
    {
        public Program4() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program5 : OperationTerminal
    {
        public Program5() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program9 : OperationTerminal
    {
        public Program9() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program10 : OperationTerminal
    {
        public Program10() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program11 : OperationTerminal
    {
        public Program11() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program12 : OperationTerminal
    {
        public Program12() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program13 : OperationTerminal
    {
        public Program13() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program14 : OperationTerminal
    {
        public Program14() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class Program16 : OperationTerminal
    {
        public Program16() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class PlusTerminal : OperationTerminal
    {
        public PlusTerminal() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class MinusTerminal : OperationTerminal
    {
        public MinusTerminal() : base(TerminalType.AdditionOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class MultiplieTerminal : OperationTerminal
    {
        public MultiplieTerminal() : base(TerminalType.MultiplieOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class DivideTerminal : OperationTerminal
    {
        public DivideTerminal() : base(TerminalType.MultiplieOperation)
        {
        }

        public override void doOperation(Context context)
        {

        }
    }

    public class EqualTerminal : OperationTerminal
    {
        public EqualTerminal() : base(TerminalType.Equal)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (int)val1.Value >= (int)val2.Value));
        }
    }

    public class EqualBooleanTerminal : OperationTerminal
    {
        public EqualBooleanTerminal() : base(TerminalType.CompareOperaion)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (int)val1.Value == (int)val2.Value));
        }
    }

    public class MoreTerminal : OperationTerminal
    {
        public MoreTerminal() : base(TerminalType.CompareOperaion)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (int)val1.Value > (int)val2.Value));
        }
    }

    public class LessTerminal : OperationTerminal
    {
        public LessTerminal() : base(TerminalType.CompareOperaion)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (int)val1.Value < (int)val2.Value));
        }
    }

    public class LessOrEqualTerminal : OperationTerminal
    {
        public LessOrEqualTerminal() : base(TerminalType.CompareOperaion)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (int)val1.Value <= (int)val2.Value));
        }
    }

    public class MoreOrEqualTerminal : OperationTerminal
    {
        public MoreOrEqualTerminal() : base(TerminalType.CompareOperaion)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (int)val1.Value >= (int)val2.Value));
        }
    }

    public class OrTerminal : OperationTerminal
    {
        public OrTerminal() : base(TerminalType.OrOperation)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();

            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (bool)val1.Value || (bool)val2.Value));
        }
    }

    public class AndTerminal : OperationTerminal
    {
        public AndTerminal() : base(TerminalType.AndOperation)
        {
        }

        public override void doOperation(Context context)
        {
            var val1 = context.PopValue();
            var val2 = context.PopValue();
 
            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, (bool)val1.Value && (bool)val2.Value));
        }
    }

   

    public class NotButTerminal : OperationTerminal
    {
        public NotButTerminal() : base(TerminalType.NotOperation)
        {
        }

        public override void doOperation(Context context)
        {
            var val = context.PopValue();
            if (val.GetType() != typeof(bool)) throw new RuntimeException("not относится не к тому типу");
            context.AddValue(new ValueTerminal(TerminalType.BooleanValue, !(bool)val.Value));
        }
    }

    public class PrintTerminal : OperationTerminal
    {
        public PrintTerminal() : base(TerminalType.ImbededFunction)
        {

        }

        public override void doOperation(Context context)
        {
            Console.WriteLine(context.PopValue());
        }
    }

    public class InputTerminal : OperationTerminal
    {
        public InputTerminal() : base(TerminalType.ImbededFunction)
        {
        }

        public override void doOperation(Context context)
        {
            var val = Console.ReadLine();

            if(int.TryParse(val, out int res))
            {
                context.AddValue(new ValueTerminal(TerminalType.Number, res));
            }
            else if(bool.TryParse(val, out bool boolRes))
            {
                context.AddValue(new ValueTerminal(TerminalType.BooleanValue, boolRes));
            }
            else
            {
                if (val == null) val = "";
                context.AddValue(new ValueTerminal(TerminalType.Line, val));
            }
        }
    }
}
