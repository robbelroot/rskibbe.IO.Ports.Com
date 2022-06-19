namespace rskibbe.IO.Ports.Com.Virtual.ValueObjects;

public class VirtualComPortsEventArgs : EventArgs
{

    public VirtualComPortPair PortPair { get; }

    public VirtualComPortsEventArgs(VirtualComPortPair portPair)
    {
        PortPair = portPair;
    }

    public VirtualComPortsEventArgs(string portNameA, string portNameB)
    {
        PortPair = new VirtualComPortPair(portNameA, portNameB);
    }

}
