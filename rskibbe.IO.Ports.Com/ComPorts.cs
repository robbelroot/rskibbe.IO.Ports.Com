using rskibbe.IO.Ports.Com.System;
using rskibbe.IO.Ports.Com.ValueObjects;
using rskibbe.IO.Ports.Com.Virtual;
using rskibbe.IO.Ports.Com.Virtual.ValueObjects;

namespace rskibbe.IO.Ports.Com;

public class ComPorts : IComPorts
{
    protected ISystemComPorts _systemComPorts { get; }

    protected IVirtualComPorts _virtualComPorts { get; }

    public ComPorts(ISystemComPorts systemComPorts, IVirtualComPorts virtualComPorts)
    {
        _systemComPorts = systemComPorts;
        _virtualComPorts = virtualComPorts;
        AttachHandlers();
    }

    /// <summary>
    /// Waits for the specific COM port to be removed async
    /// </summary>
    /// <param name="portName">For example COM3 (or com3) - use id 2 to 256</param>
    public Task PortAddedEventAsync(string portName)
    {
        var tcs = new TaskCompletionSource();
        void handler(object? sender, ComPortEventArgs e)
        {
            if (e.PortName == portName)
            {
                tcs.SetResult();
                PortAdded -= handler;
            }
        }
        PortAdded += handler;
        return tcs.Task;
    }

    /// <summary>
    /// Waits for the specific COM port to be removed async
    /// </summary>
    /// <param name="portName">For example COM3 (or com3) - use id 2 to 256</param>
    public Task PortRemovedEventAsync(string portName)
    {
        var tcs = new TaskCompletionSource();
        void handler(object? sender, ComPortEventArgs e)
        {
            if (e.PortName.ToLower() == portName.ToLower())
            {
                tcs.SetResult();
                PortRemoved -= handler;
            }
        }
        PortRemoved += handler;
        return tcs.Task;
    }

    /// <summary>
    /// Waits for any COM port to be added async
    /// </summary>
    public Task AnyPortAddedEventAsync()
    {
        var tcs = new TaskCompletionSource();
        void handler(object? sender, ComPortEventArgs e)
        {
            tcs.SetResult();
            PortAdded -= handler;
        }
        PortAdded += handler;
        return tcs.Task;
    }

    /// <summary>
    /// Waits for any COM port to be removed async
    /// </summary>
    public Task AnyPortRemovedEventAsync()
    {
        var tcs = new TaskCompletionSource();
        void handler(object? sender, ComPortEventArgs e)
        {
            tcs.SetResult();
            PortRemoved -= handler;
        }
        PortRemoved += handler;
        return tcs.Task;
    }

    #region SystemComPorts
    public Task<IEnumerable<byte>> ListUsedPortIdsAsync()
        => _systemComPorts.ListUsedPortIdsAsync();

    public Task<IEnumerable<string>> ListUsedPortNamesAsync()
        => _systemComPorts.ListUsedPortNamesAsync();
    #endregion

    #region VirtualComPorts
    public Task<IVirtualComPortRegistration> CreateVirtualPortsAsync()
        => _virtualComPorts.CreateVirtualPortsAsync();

    public Task<IVirtualComPortRegistration> CreateVirtualPortsAsync(byte portIdA, byte portIdB)
        => _virtualComPorts.CreateVirtualPortsAsync(portIdA, portIdB);

    public Task RemoveVirtualPortsAsync(string portNameAOrB)
        => _virtualComPorts.RemoveVirtualPortsAsync(portNameAOrB);

    public Task RemoveVirtualPortsAsync(int portIdAOrB)
        => _virtualComPorts.RemoveVirtualPortsAsync(portIdAOrB);

    public Task RemoveAllVirtualPortsAsync()
        => _virtualComPorts.RemoveAllVirtualPortsAsync();

    public Task<IEnumerable<IVirtualComPortRegistration>> ListVirtualPortRegistrationsAsync()
        => _virtualComPorts.ListVirtualPortRegistrationsAsync();

    public Task<IEnumerable<byte>> ListUsedVirtualPortIdsAsync()
        => _virtualComPorts.ListUsedVirtualPortIdsAsync();

    public Task<IEnumerable<string>> ListUsedVirtualPortNamesAsync()
        => _virtualComPorts.ListUsedVirtualPortNamesAsync();
    #endregion

    protected void AttachHandlers()
    {
        _systemComPorts.SystemComPortAdded += SystemComPorts_SystemComPortAdded;
        _systemComPorts.SystemComPortRemoved += SystemComPorts_SystemComPortRemoved;
        _virtualComPorts.VirtualComPortsAdded += VirtualComPorts_VirtualComPortsAdded;
        _virtualComPorts.VirtualComPortsRemoved += VirtualComPorts_VirtualComPortsRemoved;
    }

    protected void DetachHandlers()
    {
        _systemComPorts.SystemComPortAdded -= SystemComPorts_SystemComPortAdded;
        _systemComPorts.SystemComPortRemoved -= SystemComPorts_SystemComPortRemoved;
        _virtualComPorts.VirtualComPortsAdded -= VirtualComPorts_VirtualComPortsAdded;
        _virtualComPorts.VirtualComPortsRemoved -= VirtualComPorts_VirtualComPortsRemoved;
    }

    private void SystemComPorts_SystemComPortAdded(object? sender, ComPortEventArgs e)
    {
        OnSystemComPortAdded(e);
        OnPortAdded(e);
    }

    protected virtual void OnSystemComPortAdded(ComPortEventArgs e)
    {
        SystemComPortAdded?.Invoke(this, e);
    }

    private void SystemComPorts_SystemComPortRemoved(object? sender, ComPortEventArgs e)
    {
        OnSystemComPortRemoved(e);
        OnPortRemoved(e);
    }

    protected virtual void OnSystemComPortRemoved(ComPortEventArgs e)
    {
        SystemComPortRemoved?.Invoke(this, e);
    }

    private void VirtualComPorts_VirtualComPortsAdded(object? sender, VirtualComPortsEventArgs e)
    {
        OnVirtualComPortsAdded(e);
        OnPortAdded(new ComPortEventArgs(e.PortPair.NameA));
        OnPortAdded(new ComPortEventArgs(e.PortPair.NameB));
    }

    protected virtual void OnVirtualComPortsAdded(VirtualComPortsEventArgs e)
    {
        VirtualComPortsAdded?.Invoke(this, e);
    }

    private void VirtualComPorts_VirtualComPortsRemoved(object? sender, VirtualComPortsEventArgs e)
    {
        OnVirtualComPortsRemoved(e);
        OnPortRemoved(new ComPortEventArgs(e.PortPair.NameA));
        OnPortRemoved(new ComPortEventArgs(e.PortPair.NameB));
    }

    protected virtual void OnVirtualComPortsRemoved(VirtualComPortsEventArgs e)
    {
        VirtualComPortsRemoved?.Invoke(this, e);
    }

    protected virtual void OnPortAdded(ComPortEventArgs e)
    {
        PortAdded?.Invoke(this, e);
    }

    protected virtual void OnPortRemoved(ComPortEventArgs e)
    {
        PortRemoved?.Invoke(this, e);
    }

    public event EventHandler<ComPortEventArgs>? SystemComPortAdded;

    public event EventHandler<ComPortEventArgs>? SystemComPortRemoved;

    public event EventHandler<VirtualComPortsEventArgs>? VirtualComPortsAdded;

    public event EventHandler<VirtualComPortsEventArgs>? VirtualComPortsRemoved;

    public event EventHandler<ComPortEventArgs>? PortAdded;

    public event EventHandler<ComPortEventArgs>? PortRemoved;
}
