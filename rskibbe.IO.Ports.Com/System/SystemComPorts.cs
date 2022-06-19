using rskibbe.IO.Ports.Com.ValueObjects;

namespace rskibbe.IO.Ports.Com.System;

public abstract class SystemComPorts : ISystemComPorts
{

    public abstract Task<IEnumerable<byte>> ListUsedPortIdsAsync();

    public abstract Task<IEnumerable<string>> ListUsedPortNamesAsync();

    public abstract event EventHandler<ComPortEventArgs> SystemComPortAdded;

    public abstract event EventHandler<ComPortEventArgs> SystemComPortRemoved;

}
