using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com
{
    public class ComWatcherException : Exception
    {
        public ComWatcherException()
        {
        }

        public ComWatcherException(string message) : base(message)
        {
        }

        public ComWatcherException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComWatcherException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
