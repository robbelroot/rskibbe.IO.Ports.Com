namespace rskibbe.IO.Ports.Com
{
    public abstract class ComPortRegistrationBase : IComPortRegistration
    {

        public ComPortPair ComPorts { get; set; }

        public ComPortRegistrationBase()
        {
            ComPorts = new ComPortPair();
        }

    }
}
