
using Interpritator.Exceptions;
using Interpritator.output;
using System.Text;
using Анализатор_лексем;
using static Interpritator.Terminal;

namespace Interpritator
{
    public class Context
    {
        public enum Mode
        {
            NONE,
            SAVE_IDENTF,
            SAVE_VALUE,
            SAVE_ARRAY,
            WHILE,
            IF,
            IGNORE,
            SAVE_ARRAY_VAL,
        }

        public int CurrentStep { get { return _currentStep; } }

        //Для вложенных циклов
        public Stack<Mode> _mods = new();
        private Stack<ValueTerminal> _operands = new Stack<ValueTerminal>();
        private Stack<int> _marks = new Stack<int>();
        private int _currentStep = 0;

        private Dictionary<string, ValueTerminal> _variables = new();
        private Dictionary<string, List<ValueTerminal>> _arrays = new();

        private string _currentIdf = "";
        private int _currentInd = 0;
        private int _arrayCount = 0;

        // сраные костыли
        private int openCount = 0;

        private IOutput _output;

        public Context(IOutput output)
        {
            _output = output;
            _mods.Push(Mode.NONE);
        }

        public bool Valid(RPNTerminal terminal)
        {
            if(_mods.Peek() == Mode.IGNORE) return false;
            return true;
        }

        public void AddValue(ValueTerminal terminal)
        {
            if (_mods.Peek() == Mode.IGNORE) return;

            if (_mods.Peek() == Mode.SAVE_IDENTF)
            {
                _currentIdf = terminal.Value.ToString() ?? "";
                _mods.Pop();
                _mods.Push(Mode.SAVE_VALUE);    
            }
            else
            {
                if (_mods.Peek() == Mode.SAVE_ARRAY) _arrayCount++;
                
                _operands.Push(terminal);
            }
        }

        private ValueTerminal GetValue(ValueTerminal value)
        {

            if (value.GetType() == typeof(IdentifierTerminal))
            {
                if (!_variables.ContainsKey(value.Value.ToString() ?? "")) throw new VariableDoesntExists();

                return _variables[value.Value.ToString() ?? ""];
            }

            return value;
        }

        private ValueTerminal PopValue()
        {
            var value = _operands.Pop();
            return GetValue(value);
        }

        private ValueTerminal PeekValue()
        {
            var value = _operands.Peek();
            return GetValue(value);
        }

        public void AssignmentOperation()
        {
            if (_mods.Peek() == Mode.IGNORE || _mods.Peek() == Mode.SAVE_ARRAY) return;

            var value = this.PopValue();

            if (_mods.Peek() == Mode.SAVE_ARRAY_VAL)
            {
                _arrays[_currentIdf][_currentInd] = value;
                _mods.Pop();
            }
            else
            {
                var idef = _operands.Pop();

                if (idef.GetType() == typeof(IdentifierTerminal))
                {
                    _variables[idef.Value.ToString() ?? ""] = value;
                }
            }
        }

        public void StartWhile()
        {
            if (_mods.Peek() == Mode.IGNORE && (openCount) > 0) { return; }
            // 4
            _marks.Push(_currentStep);
            _mods.Push(Mode.WHILE);
        }

        public void EnterArea()
        {
            if (_mods.Peek() == Mode.IGNORE && (openCount) > 0) { openCount++; return; }
            // 1
            var cond = PeekValue();
            
            if (!(bool)cond.Value)
            {
                openCount = 1;
                _mods.Push(Mode.IGNORE);
            }
            else
            {
                _mods.Push(Mode.IF);
                openCount = 0;
            }
        }

        public void StartElse()
        {
            // 2 
            var mode = _mods.Peek();
            if (mode == Mode.IGNORE && (openCount - 1) > 0) { return; }

            
            if (mode == Mode.IGNORE)
            {
                _mods.Pop();
                openCount = 0;
                _mods.Push(Mode.IF);
            }
            else if(mode == Mode.IF)
            {
                openCount = 1;
                _mods.Pop();
                _mods.Push(Mode.IGNORE);
            }
            else
            {
                throw new Exception("temop");
            }
        }

        public void CloseElse()
        {
            // 3
            openCount--;
            if (_mods.Peek() == Mode.IGNORE && (openCount) > 0) {  return; }

            _mods.Pop();
        }


        public void CloseWhile()
        {
            openCount--;
            if (_mods.Peek() == Mode.IGNORE && (openCount) > 0) { return; }

            var mod = _mods.Pop();
            var mod1 = _mods.Pop();
            var mark = _marks.Pop();
            // 5
            if (mod1 == Mode.WHILE)
            {
                if (mod == Mode.IF)
                {
                    _currentStep = mark - 1;
                }
                else if (mod == Mode.IGNORE)
                {
                }
                else
                {
                    throw new Exception("temop");
                }
            }
            else
            {
                throw new Exception("temop");
            }
        }

        internal void StartSaveIdentf()
        {
            if (_mods.Peek() == Mode.IGNORE) {  return; }

            _mods.Push(Mode.SAVE_IDENTF);
        }

        internal void FinishSaveVariable()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }

            var mode = _mods.Pop();
            if (mode != Mode.SAVE_ARRAY)
            {
                _variables[_currentIdf] = _operands.Pop();
            }
            else
            {
                List<ValueTerminal> array = new();
                for (int i = 0; i < _arrayCount; i++)
                {
                   // int a[5] = {1, 2, 3, 4, 5}; print(a[0])
                   array.Add(_operands.Pop());
                }
                array.Reverse();
                _arrays[_currentIdf] = array;
            }
        }

        internal void StartSaveArray()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }

            _mods.Pop();
            _mods.Push(Mode.SAVE_ARRAY);
        }

        internal void StartSaveValue()
        {
           if (_mods.Peek() == Mode.IGNORE) { return; }

            _mods.Pop();
            _mods.Push(Mode.SAVE_VALUE);
        }

        internal void PushValueFromArrayToStack()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }

            var index = PopValue();
            var idef = _operands.Pop();



            if (idef.GetType() == typeof(IdentifierTerminal))
            {
                if (!_arrays.ContainsKey(idef.Value.ToString() ?? "")) throw new VariableDoesntExists();

                _operands.Push(_arrays[idef.Value.ToString() ?? ""][(int)index.Value]);
            }
        }

        public void NextStep()
        {
            _currentStep++;
        }

        internal void WriteOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }

            var val = _output.ReadLine();

            if (int.TryParse(val, out int res))
            {
                this.AddValue(new ValueTerminal(TerminalType.Number, res));
            }
            else if (bool.TryParse(val, out bool boolRes))
            {
                this.AddValue(new ValueTerminal(TerminalType.BooleanValue, boolRes));
            }
            else
            {
                this.AddValue(new ValueTerminal(TerminalType.Line, val ?? "null"));
            }
        }

        internal void PrintOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            _output.Print(this.PopValue().Value.ToString() ?? "null");
        }

        internal void NotOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val = this.PopValue();
            if (val.GetType() != typeof(bool)) throw new RuntimeException("not относится не к тому типу");
            this.AddValue(new ValueTerminal(TerminalType.BooleanValue, !(bool)val.Value));
        }

        internal void AndOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();

            this.AddValue(new ValueTerminal(TerminalType.BooleanValue, (bool)val1.Value && (bool)val2.Value));
        }

        internal void OrOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();

            this.AddValue(new ValueTerminal(TerminalType.BooleanValue, (bool)val1.Value || (bool)val2.Value));
        }

        private void CompareOperation(Func<int, int, bool> comparisonFunc)
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();

            this.AddValue(new ValueTerminal(TerminalType.BooleanValue, comparisonFunc((int)val1.Value, (int)val2.Value)));
        }
        
        private static Dictionary<TerminalType, Func<int, int, bool>> compareOperations = new Dictionary<TerminalType, Func<int, int, bool>>
        {
            { TerminalType.MoreCompareOperation, (a, b) => a > b },
            { TerminalType.LessCompareOperation, (a, b) => a < b },
            { TerminalType.LessEqualCompareOperation, (a, b) => a <= b },
            { TerminalType.MoreEqualCompareOperation, (a, b) => a >= b },
            { TerminalType.EqualCompareOperation, (a, b) => a == b }
        };

        internal void CompareOperation(TerminalType type)
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            if (compareOperations.ContainsKey(type))
                CompareOperation(compareOperations[type]);
        }

        private string RepeatString(string str, int n)
        {
            StringBuilder sb = new();
            for (int i = 0; i < n; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        internal void MultiplieOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();


            if ((val1.Type == TerminalType.Number || val1.Type == TerminalType.Integer) && val2.Type == TerminalType.Line) 
            {
                this.AddValue(new ValueTerminal(TerminalType.Line, RepeatString(val2.Value.ToString(), (int)val1.Value)));

            }
            else if((val2.Type == TerminalType.Number || val1.Type == TerminalType.Integer) && val1.Type == TerminalType.Line)
            {
                this.AddValue(new ValueTerminal(TerminalType.Line, RepeatString(val1.Value.ToString(), (int)val2.Value)));

            }
            else
            {
                this.AddValue(new ValueTerminal(TerminalType.Integer, (int)val2.Value * (int)val1.Value));
            }
        }

        internal void MinusOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();

            this.AddValue(new ValueTerminal(TerminalType.Integer, (int)val2.Value - (int)val1.Value));
        }

        internal void PlusOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();

            this.AddValue(new ValueTerminal(TerminalType.Integer, (int)val2.Value + (int)val1.Value));
        }

        internal void DivideOperation()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }
            var val1 = this.PopValue();
            var val2 = this.PopValue();

            this.AddValue(new ValueTerminal(TerminalType.Integer, (int)val2.Value / (int)val1.Value));
        }

        internal void StartSaveArrayValue()
        {
            if (_mods.Peek() == Mode.IGNORE) { return; }

            var index = PopValue();
            var idef = _operands.Pop();

            _currentInd = (int)index.Value;
            _currentIdf = idef.Value.ToString();

            _mods.Push(Mode.SAVE_ARRAY_VAL);
        }
    }
}