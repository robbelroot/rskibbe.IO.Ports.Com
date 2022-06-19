using rskibbe.IO.Ports.Com.Virtual.ValueObjects;

namespace rskibbe.IO.Ports.Com.Virtual;

public abstract class VirtualComPortsBase : IVirtualComPorts
{

    protected VirtualComPortsBase()
    {

    }

    public abstract Task<IVirtualComPortRegistration> CreateVirtualPortsAsync();

    public abstract Task<IVirtualComPortRegistration> CreateVirtualPortsAsync(byte portIdA, byte portIdB);

    public abstract Task RemoveVirtualPortsAsync(string portNameAOrB);

    public abstract Task RemoveVirtualPortsAsync(int portIdAOrB);

    public abstract Task RemoveAllVirtualPortsAsync();

    public abstract Task<IEnumerable<string>> ListUsedPortNamesAsync();

    public abstract Task<IEnumerable<byte>> ListUsedPortIdsAsync();

    public abstract Task<IEnumerable<IVirtualComPortRegistration>> ListVirtualPortRegistrationsAsync();

    public abstract Task<IEnumerable<string>> ListUsedVirtualPortNamesAsync();

    public abstract Task<IEnumerable<byte>> ListUsedVirtualPortIdsAsync();

    public event EventHandler<VirtualComPortsEventArgs> VirtualComPortsAdded;

    public event EventHandler<VirtualComPortsEventArgs> VirtualComPortsRemoved;

}
