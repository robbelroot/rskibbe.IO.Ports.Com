namespace rskibbe.IO.Ports.Com.System;

public interface ISystemComPorts : ISystemComPortEvents
{

    List<string> ExistingPorts { get; }

    Task<IEnumerable<byte>> ListUsedPortIdsAsync();

    Task<IEnumerable<string>> ListUsedPortNamesAsync();

}