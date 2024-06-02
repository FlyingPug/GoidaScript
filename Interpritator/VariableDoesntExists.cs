using System.Runtime.Serialization;

namespace Interpritator
{
    [Serializable]
    internal class VariableDoesntExists : Exception
    {
        public VariableDoesntExists()
        {
        }

        public VariableDoesntExists(string? message) : base(message)
        {
        }

        public VariableDoesntExists(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected VariableDoesntExists(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}