using rskibbe.IO.Ports.Com.ValueObjects;

namespace rskibbe.IO.Ports.Com.System;

public abstract class SystemComPortsBase : ISystemComPorts
{

    public List<string> ExistingPorts { get; }

    public abstract Task<IEnumerable<byte>> ListUsedPortIdsAsync();

    public abstract Task<IEnumerable<string>> ListUsedPortNamesAsync();

    public SystemComPortsBase()
    {
        ExistingPorts = new List<string>();
    }

    protected virtual void OnSystemComPortAdded(ComPortEventArgs e)
        => SystemComPortAdded?.Invoke(this, e);

    protected virtual void OnSystemComPortRemoved(ComPortEventArgs e)
        => SystemComPortRemoved?.Invoke(this, e);

    public event EventHandler<ComPortEventArgs>? SystemComPortAdded;

    public event EventHandler<ComPortEventArgs>? SystemComPortRemoved;

}
