namespace rskibbe.IO.Ports.Com.System;

public interface ISystemComPorts : ISystemComPortEvents
{

    Task<IEnumerable<byte>> ListUsedPortIdsAsync();

    Task<IEnumerable<string>> ListUsedPortNamesAsync();

}