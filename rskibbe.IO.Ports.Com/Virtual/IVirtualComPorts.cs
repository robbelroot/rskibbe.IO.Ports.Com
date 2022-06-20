namespace rskibbe.IO.Ports.Com.Virtual;

public interface IVirtualComPorts : IVirtualComPortEvents
{

    Task<IVirtualComPortRegistration> CreateVirtualPortsAsync();

    Task<IVirtualComPortRegistration> CreateVirtualPortsAsync(byte portIdA, byte portIdB);

    Task RemoveVirtualPortsAsync(string portNameAOrB);

    Task RemoveVirtualPortsAsync(byte portIdAOrB);

    Task RemoveAllVirtualPortsAsync();

    Task<IEnumerable<IVirtualComPortRegistration>> ListVirtualPortRegistrationsAsync();

    Task<IEnumerable<byte>> ListUsedVirtualPortIdsAsync();

    Task<IEnumerable<string>> ListUsedVirtualPortNamesAsync();

}
