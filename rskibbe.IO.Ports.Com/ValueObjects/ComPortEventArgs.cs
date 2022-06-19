namespace rskibbe.IO.Ports.Com.ValueObjects;

public class ComPortEventArgs : EventArgs
{

    public string PortName { get; }

    public byte PortId
    {
        get
        {
            PortName.ExtractByte(out var portId);
            return portId;
        }
    }

    public ComPortEventArgs(string portName)
    {
        PortName = portName;
    }

    public override string ToString()
        => PortName;
}
