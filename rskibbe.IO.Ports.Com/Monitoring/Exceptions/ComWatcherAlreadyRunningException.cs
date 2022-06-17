using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com.Monitoring.Exceptions
{
    public class ComWatcherAlreadyRunningException : ComWatcherException
    {
        public ComWatcherAlreadyRunningException()
        {
        }

        public ComWatcherAlreadyRunningException(string message) : base(message)
        {
        }

        public ComWatcherAlreadyRunningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComWatcherAlreadyRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
