using rskibbe.IO.Ports.Com.System;
using rskibbe.IO.Ports.Com.Virtual;

namespace rskibbe.IO.Ports.Com;

public interface IComPorts : ISystemComPorts, IVirtualComPorts
{

    Task PortAddedEventAsync(string portName);

    Task PortRemovedEventAsync(string portName);

    Task AnyPortAddedEventAsync();

    Task AnyPortRemovedEventAsync();

}
