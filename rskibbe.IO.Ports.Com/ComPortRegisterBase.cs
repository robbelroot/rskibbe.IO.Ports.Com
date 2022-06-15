namespace rskibbe.IO.Ports.Com
{
    public abstract class ComPortRegisterBase : IComPortRegister
    {

        public ComPortRegisterBase()
        {

        }

        public abstract Task<IComPortRegistration> CreateVirtualPortsAsync();

        public abstract Task<IComPortRegistration> CreateVirtualPortsAsync(byte portIdA, byte portIdB);

        public abstract Task RemoveVirtualPortsByNameAsync(string portNameAOrB);

        public abstract Task RemoveVirtualPortsByRegistrationIdAsync(int portIdAOrB);

        public abstract Task RemoveAllVirtualPortsAsync();

        public abstract Task<IEnumerable<string>> ListUsedPortNamesAsync();

        public abstract Task<IEnumerable<byte>> ListUsedPortIdsAsync();

        public abstract Task<IEnumerable<IComPortRegistration>> ListVirtualPortRegistrationsAsync();

        public abstract Task<IEnumerable<string>> ListUsedVirtualPortNamesAsync();

        public abstract Task<IEnumerable<byte>> ListUsedVirtualPortIdsAsync();

    }
}
