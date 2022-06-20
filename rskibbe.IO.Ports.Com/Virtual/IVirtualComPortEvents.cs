using rskibbe.IO.Ports.Com.Virtual.ValueObjects;

namespace rskibbe.IO.Ports.Com.Virtual;

public interface IVirtualComPortEvents
{

    event EventHandler<VirtualComPortsEventArgs>? VirtualComPortsAdded;

    event EventHandler<VirtualComPortsEventArgs>? VirtualComPortsRemoved;

}
