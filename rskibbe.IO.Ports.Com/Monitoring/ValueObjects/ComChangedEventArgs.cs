namespace rskibbe.IO.Ports.Com.Monitoring.ValueObjects;

public class ComChangedEventArgs : ComEventArgs
{

    public ComWatcherEventType EventType { get; }

    public bool WasInsertion => EventType == ComWatcherEventType.Inserted;

    public bool WasRemoval => EventType == ComWatcherEventType.Removed;

    public ComChangedEventArgs(string portName, ComWatcherEventType eventType) : base(portName)
    {
        EventType = eventType;
    }

}
