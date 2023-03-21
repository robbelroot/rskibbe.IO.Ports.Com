namespace rskibbe.IO.Ports.Com;

public interface IComPortEvents
{

    event EventHandler PortAdded;

    event EventHandler PortRemoved;

}
