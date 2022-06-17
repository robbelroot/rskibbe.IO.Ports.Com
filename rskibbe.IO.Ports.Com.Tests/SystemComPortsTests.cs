using NUnit.Framework;
using rskibbe.IO.Ports.Com.Monitoring;
using rskibbe.IO.Ports.Com.Monitoring.ValueObjects;

namespace rskibbe.IO.Ports.Com.Tests;

public class ComPortsTests
{

    protected SystemComPorts systemComPorts;

    [SetUp]
    public void Setup()
    {
        systemComPorts = new SystemComPorts();
    }

    [Test]
    public async Task TestOneEventAsyncWithInserted()
    {
        await systemComPorts.OneEventAsync(ComWatcherEventType.Inserted);
    }

    [Test]
    public async Task TestOneEventAsyncWithRemoved()
    {
        await systemComPorts.OneEventAsync(ComWatcherEventType.Removed);
    }

    [Test]
    public async Task TestOneEventAsyncWithAnyButInserted()
    {
        await systemComPorts.OneEventAsync(ComWatcherEventType.Any);
    }

    [Test]
    public async Task TestOneEventAsyncWithAnyButRemoved()
    {
        await systemComPorts.OneEventAsync(ComWatcherEventType.Any);
    }

    [Test]
    public async Task TestOnePortEventByNameWithInsert()
    {
        await systemComPorts.OnePortEventByNameAsync("COM4", ComWatcherEventType.Inserted);
    }

    [Test]
    public async Task TestOnePortEventByNameWithRemove()
    {
        await systemComPorts.OnePortEventByNameAsync("COM4", ComWatcherEventType.Removed);
    }

    [Test]
    public async Task TestOnePortEventByNameWithAny()
    {
        await systemComPorts.OnePortEventByNameAsync("COM4", ComWatcherEventType.Any);
    }

}