using rskibbe.IO.Ports.Com.Virtual.ValueObjects;

namespace rskibbe.IO.Ports.Com.Virtual;

public interface IVirtualComPortEvents
{

    public event EventHandler<VirtualComPortsEventArgs> VirtualComPortsAdded;

    public event EventHandler<VirtualComPortsEventArgs> VirtualComPortsRemoved;

}
