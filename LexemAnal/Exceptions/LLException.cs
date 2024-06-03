using System.Runtime.Serialization;

namespace Анализатор_лексем.Exceptions
{
    [Serializable]
    public class LLException : Exception
    {
        public LLException()
        {
        }

        public LLException(string? message) : base(message)
        {
        }

        public LLException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LLException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}