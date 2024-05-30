namespace Анализатор_лексем
{
    public interface ILexicalAnalyzer
    {
        
    }

    public static class Program
    {

        static void Main()
        {

            string? input = Console.ReadLine();
            
            if (input != null) S.Analyse(input);

            foreach(var ls in InputData.lexems)
            {
                Console.WriteLine($"Распознана лексема: {ls.Item1} значение: {ls.Item2}");
            }
        }
    }
}

