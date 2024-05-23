namespace Interpritator
{
    public struct Lexem
    {
        public readonly LexemType Type;
        public readonly string Value;

        public Lexem(LexemType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}