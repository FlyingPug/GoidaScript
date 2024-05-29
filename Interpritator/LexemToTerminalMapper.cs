using System;

namespace Interpritator
{
    public class UnidentifiedException : Exception
    {
        public UnidentifiedException() 
        {
        }
    }

    internal class LexemToTerminalMapper
    {
        private static readonly Dictionary<string, Func<Terminal>> systemWords = new()
        {
            {"while", () => new Terminal(Terminal.TerminalType.WhileStatement)},
            {"int", () => new Terminal(Terminal.TerminalType.Integer)},
            {"string", () => new Terminal(Terminal.TerminalType.String)},
            {"bool", () => new Terminal(Terminal.TerminalType.Boolean)},
            {"print", () => new PrintTerminal()},
            {"input", () => new InputTerminal()},
            {"if", () => new Terminal(Terminal.TerminalType.IfStatement)},
            {"not", () => new NotButTerminal()},
            {"or", () => new OrTerminal()},
            {"and", () => new AndTerminal()}
        };

        private static readonly Dictionary<string, Func<Terminal>> comparisons = new()
        {
            {">=", () => new MoreOrEqualTerminal()},
            {"<=", () => new LessOrEqualTerminal()},
            {">", () => new LessTerminal()},
            {"<", () => new MoreTerminal()}
        };

        private static readonly Dictionary<string, Func<Terminal>> terminalTypes = new()
        {
            {"COMMA", () => new Terminal(Terminal.TerminalType.Comma)},
            {"=", () => new EqualTerminal()},
            {"+", () => new PlusTerminal()},
            {"-", () => new MinusTerminal()},
            {"(", () => new Terminal(Terminal.TerminalType.OpenBracket)},
            {"[", () => new Terminal(Terminal.TerminalType.OpenSquareBracket)},
            {"{", () => new Terminal(Terminal.TerminalType.OpenFigureBracket)},
            {")", () => new Terminal(Terminal.TerminalType.CloseBracket)},
            {"]", () => new Terminal(Terminal.TerminalType.CloseBracket)},
            {"}", () => new Terminal(Terminal.TerminalType.CloseBracket)},

        };

        private static readonly Dictionary<string, Func<string, Terminal>> terminalWithValue = new()
        {
            {"STRING", (value) => new ValueTerminal<string>(Terminal.TerminalType.String, value)},
            {"NUMBER", (value) => new ValueTerminal<int>(Terminal.TerminalType.Number, int.Parse(value))},
            {"BOOLEAN", (value) => new ValueTerminal<bool>(Terminal.TerminalType.BooleanValue, bool.Parse(value))},
            {"IDENTIFIER", (value) => new IdentifierTerminal(value) },
        };

        public Terminal ToTerminal((string, string) lexem)
        {
            if (terminalTypes.TryGetValue(lexem.Item1, out var createTerminal))
            {
                return createTerminal.Invoke();
            }
            
            if (terminalWithValue.TryGetValue(lexem.Item1, out var createTerminalWithValue))
            {
                return createTerminalWithValue.Invoke(lexem.Item2);
            }

            if (lexem.Item1 == "IDENTIFIER")
            {
                return new IdentifierTerminal(lexem.Item2);
            }

            if (lexem.Item1 == "SYSTEM_WORD" && systemWords.TryGetValue(lexem.Item2, out createTerminal))
            {
                return createTerminal.Invoke();
            }

            if (lexem.Item1 == "COMPARSION" && comparisons.TryGetValue(lexem.Item2, out createTerminal))
            {
                return createTerminal.Invoke();
            }

            throw new UnidentifiedException();
        }

        public List<Terminal> ToTerminalList(ICollection<(string, string)> lexems)
        {
            List<Terminal> terminals = new List<Terminal>();

            foreach ((string, string) lexem in lexems)
            {
                terminals.Add(ToTerminal(lexem));
            }

            return terminals;
        }
    }
}
