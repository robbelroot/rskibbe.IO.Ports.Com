using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com
{
    public class ComPortsRegistrationException : Exception
    {
        public ComPortsRegistrationException(string? message) : base(message)
        {
        }

        public ComPortsRegistrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ComPortsRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
