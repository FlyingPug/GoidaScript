
namespace Interpritator
{
    public class Context
    {
        private Stack<ValueTerminal> _operands = new Stack<ValueTerminal>();

        private string _currentIdentifierName = "";
        private Dictionary<string, ValueTerminal> _variables = new();

        public void AddValue(ValueTerminal terminal)
        {
            _operands.Push(terminal);
        }

        public ValueTerminal PopValue()
        {
            return _operands.Pop();
        }

        public void AssignmentOperation()
        {

        }
    }
}