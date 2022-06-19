namespace rskibbe.IO.Ports.Com.Virtual;

public abstract class VirtualComPortRegistrationBase : IVirtualComPortRegistration
{

    public VirtualComPortPair ComPorts { get; set; }

    public VirtualComPortRegistrationBase()
    {
        ComPorts = new VirtualComPortPair();
    }

}