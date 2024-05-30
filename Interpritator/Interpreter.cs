using Анализатор_лексем;

namespace Interpritator
{
    internal class Interpreter
    {
        private static List<RPNTerminal> ConvertStringToRPN(string input)
        {
            S.Analyse(input);

            var terminalsList = LexemToTerminalMapper.ToTerminalQueue(InputData.lexems);

            return RPNGenerator.GenerateOps(terminalsList);
        }

        private Context context = new();

        public void Interpret(string input)
        {
            List<RPNTerminal> rPNprogram = ConvertStringToRPN(input);

            /*foreach (var terminal in rPNprogram) 
            {
                if (terminal.GetType() == typeof(ValueTerminal))
                {
                    context.AddValue((ValueTerminal)terminal);
                }
                else if (terminal.GetType() == typeof(OperationTerminal))
                {
                    ((OperationTerminal)terminal).doOperation(context);
                }
                else
                {
                    throw new TerminalEmptyException($"всё плохо {terminal.Type}");
                }
            }*/
        }
    }
}
