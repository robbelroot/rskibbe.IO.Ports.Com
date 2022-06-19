# Description
COM port infrastructure package like events, notifications & more.

# Getting started
After Installation, just go ahead and import the corresponding namespace:

**C#**

    using rskibbe.IO.Ports.Com;
    
**Visual Basic .NET**
    
    Imports rskibbe.IO.Ports.Com

Now you are ready to write your own implementation of an **IComPortRegister** / **ComPortRegisterBase**.

## Functions & Methods
The basic functionality which should be implemented is listed here / each **IComPortRegister** should be able to:

### CreateVirtualPortsAsync()
This function should create a virtual port pair by the next COM port ids being available. It should return an implementation instance of IComPortRegistration. It should also throw an ComPortsRegistrationException if the registration failed.

### CreateVirtualPorts(byte portIdA, byte portIdB)
This function should create a virtual port pair based on the provided ids. Ids 5 & 6 should result in COM5 & COM6 being created. It should return an implementation instance of IComPortRegistration. It should also throw a ComPortsRegistrationException if the registration failed.

### RemoveVirtualPortsByNameAsync(string portNameAOrB)
This method should remove a registration (which consists of two ports / one port pair like COM5 & COM6) by providing either one of those port names. It should throw a ComPortsRemovalException, if it doesn't work.

### RemoveAllVirtualPortsAsync()
This method should remove all registered virtual ports. It should throw a ComPortsRemovalException if one of the removals fails.

### ListUsedPortNamesAsync()
This function should list / return all port names being used (therefore not being able to be registered).
> It should retrieve every "used" port, not only the virtual ones.

### ListUsedPortIdsAsync()
This function should list / return all port ids being used (therefore not being able to be registered). If COM5 & COM6 are in use, it should therefore return 5 & 6.

### ListVirtualPortRegistrationsAsync()
This function should list all virtual registrations in effect by returning instances of implemented IComPortRegistration's.

### ListUsedVirtualPortNamesAsync()
This function should return a list of all registered virtual port names.

### ListUsedVirtualPortIdsAsync()
This function should return a list of all registered virtual port ids.