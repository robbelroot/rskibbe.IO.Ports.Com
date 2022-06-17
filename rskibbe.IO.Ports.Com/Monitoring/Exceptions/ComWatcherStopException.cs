using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com
{
    public class ComWatcherStopException : ComWatcherException
    {
        public ComWatcherStopException()
        {
        }

        public ComWatcherStopException(string message) : base(message)
        {
        }

        public ComWatcherStopException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComWatcherStopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
