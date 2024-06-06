using Анализатор_лексем;
using Interpritator;
namespace GoidScriptTests
{
    public class LLTests
    {
        [SetUp]
        public void Setup()
        {
        }
        // int n = 5; int arr[n] = { 1, 2, 3, 4, 5 }; int S = 0; int i = 0; while( i < n ) { S = arr[i] + S i = i + 1 } print(S)
        [Test]
        public void Test22terminals()
        {
            string input = "int a = 5; while(a > 0){print(a) a = a - 1}";

            S.Analyse(input);

            var terminalsList = LexemToTerminalMapper.ToTerminalQueue(InputData.lexems);
            Assert.That(terminalsList.Count, Is.EqualTo(22));
        }

        [Test]
        public void TestIgnoreNewLine()
        {
            string input = "int a = 5;\nwhile(a > 0){print(a) a = a - 1}";

            S.Analyse(input);

            var terminalsList = LexemToTerminalMapper.ToTerminalQueue(InputData.lexems);
            Assert.That(terminalsList.Count, Is.EqualTo(22));
        }
    }
}