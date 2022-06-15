namespace rskibbe.IO.Ports.Com
{
    public interface IComPortRegister
    {
        Task<IComPortRegistration> CreateVirtualPortsAsync();

        Task<IComPortRegistration> CreateVirtualPortsAsync(byte portIdA, byte portIdB);

        Task RemoveVirtualPortsByNameAsync(string portNameAOrB);

        Task RemoveVirtualPortsByRegistrationIdAsync(int portIdAOrB);

        Task RemoveAllVirtualPortsAsync();

        Task<IEnumerable<string>> ListUsedPortNamesAsync();

        Task<IEnumerable<byte>> ListUsedPortIdsAsync();

        Task<IEnumerable<IComPortRegistration>> ListVirtualPortRegistrationsAsync();

        Task<IEnumerable<string>> ListUsedVirtualPortNamesAsync();

        Task<IEnumerable<byte>> ListUsedVirtualPortIdsAsync();      

    }
}
