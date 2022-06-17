using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com.Monitoring.Exceptions
{

    public class ComWatcherNotRunningException : ComWatcherStopException
    {
        public ComWatcherNotRunningException()
        {
        }

        public ComWatcherNotRunningException(string message) : base(message)
        {
        }

        public ComWatcherNotRunningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComWatcherNotRunningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
