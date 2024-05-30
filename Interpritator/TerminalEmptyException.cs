using System.Runtime.Serialization;

namespace Interpritator
{
    [Serializable]
    internal class TerminalEmptyException : Exception
    {
        public TerminalEmptyException()
        {
        }

        public TerminalEmptyException(string? message) : base(message)
        {
        }

        public TerminalEmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TerminalEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}