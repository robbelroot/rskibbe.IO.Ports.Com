using rskibbe.IO.Ports.Com.ValueObjects;

namespace rskibbe.IO.Ports.Com.System;

public interface ISystemComPortEvents
{

    public event EventHandler<ComPortEventArgs> SystemComPortAdded;

    public event EventHandler<ComPortEventArgs> SystemComPortRemoved;

}
