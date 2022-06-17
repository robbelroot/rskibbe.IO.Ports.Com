using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com.Monitoring.Exceptions
{
    public class ComWatcherStopAlreadyRequestedException : ComWatcherStopException
    {
        public ComWatcherStopAlreadyRequestedException()
        {
        }

        public ComWatcherStopAlreadyRequestedException(string message) : base(message)
        {
        }

        public ComWatcherStopAlreadyRequestedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComWatcherStopAlreadyRequestedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
