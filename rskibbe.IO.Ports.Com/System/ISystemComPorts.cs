using rskibbe.IO.Ports.Com.ValueObjects;

namespace rskibbe.IO.Ports.Com.System;

public interface ISystemComPorts : ISystemComPortEvents
{

    List<string> ExistingPorts { get; }

    Task<IEnumerable<byte>> ListUsedPortIdsAsync();

    Task<IEnumerable<string>> ListUsedPortNamesAsync();

    event EventHandler<ComPortEventArgs>? SystemComPortAdded;

    event EventHandler<ComPortEventArgs>? SystemComPortRemoved;

}