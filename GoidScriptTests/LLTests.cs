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