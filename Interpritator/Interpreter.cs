using Interpritator.Exceptions;
using Interpritator.output;
using Анализатор_лексем;
using Анализатор_лексем.Exceptions;

namespace Interpritator
{
    public class Interpreter
    {
        private Context _context;

        public Interpreter(IOutput output) 
        {
            _context = new(output);
        }    

        private static List<RPNTerminal> ConvertStringToRPN(string input)
        {
            S.Analyse(input);

            var terminalsList = LexemToTerminalMapper.ToTerminalQueue(InputData.lexems);

            return RPNGenerator.GenerateOps(terminalsList);
        }

        public void Interpret(string input)
        {
            try
            {
                List<RPNTerminal> rPNprogram = ConvertStringToRPN(input);

                while (_context.CurrentStep < rPNprogram.Count)
                {
                    var currentTerminal = rPNprogram[_context.CurrentStep];

                    Logger.Log($"[Интерпритатор] текущий обрабатываемый символ: {currentTerminal}", 2);

                    if (currentTerminal.GetType() == (typeof(ValueTerminal)) || currentTerminal.GetType().IsSubclassOf(typeof(ValueTerminal)))
                    {
                        _context.AddValue((ValueTerminal)currentTerminal);
                    }
                    else if (currentTerminal.GetType().IsSubclassOf(typeof(OperationTerminal)))
                    {
                        ((OperationTerminal)currentTerminal).doOperation(_context);
                    }
                    else
                    {
                        throw new TerminalEmptyException($"всё плохо {currentTerminal.Type}");
                    }

                    _context.NextStep();
                }
            }
            catch (LLException e)
            {
                Logger.Log($"[Критическая ошибка] ошибка лексического анализатора: {e.Message}");
            }
            catch (VariableDoesntExists e)
            {
                Logger.Log($"[Критическая ошибка] следующая переменная не была найдена {e.Message} Терминал исполнения - {_context.CurrentStep}");
            }
            catch (TerminalNotFoundException e)
            {
                Logger.Log($"[Критическая ошибка] не найден следующий терминал {e.Message} Терминал исполнения - {_context.CurrentStep}");
            }
            catch (Exception e)
            {
                Logger.Log($"[Критическая ошибка] получена неизвестная ошибка: {e.Message} Терминал исполнения - {_context.CurrentStep}");
            }
        }

    }
}
