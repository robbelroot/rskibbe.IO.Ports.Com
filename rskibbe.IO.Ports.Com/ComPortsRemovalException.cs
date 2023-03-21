using System.Runtime.Serialization;

namespace rskibbe.IO.Ports.Com;

public class ComPortsRemovalException : Exception
{
    public ComPortsRemovalException(string? message) : base(message)
    {
    }

    public ComPortsRemovalException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ComPortsRemovalException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
