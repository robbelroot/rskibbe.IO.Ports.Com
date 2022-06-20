using rskibbe.IO.Ports.Com.ValueObjects;

namespace rskibbe.IO.Ports.Com.System;

public interface ISystemComPortEvents
{

    event EventHandler<ComPortEventArgs>? SystemComPortAdded;

    event EventHandler<ComPortEventArgs>? SystemComPortRemoved;

}
