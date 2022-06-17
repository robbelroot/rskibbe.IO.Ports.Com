namespace rskibbe.IO.Ports.Com.Monitoring.ValueObjects
{
    public class ComEventArgs : EventArgs
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

        public ComEventArgs(string portName)
        {
            PortName = portName;
        }

        public override string ToString()
            => PortName;
    }
}
