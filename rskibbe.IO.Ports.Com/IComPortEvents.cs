namespace rskibbe.IO.Ports.Com;

public interface IComPortEvents
{

    public event EventHandler PortAdded;

    public event EventHandler PortRemoved;

}
