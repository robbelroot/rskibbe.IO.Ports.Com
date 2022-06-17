using NUnit.Framework;
using rskibbe.IO.Ports.Com.Monitoring;
using rskibbe.IO.Ports.Com.Monitoring.ValueObjects;

namespace rskibbe.IO.Ports.Com.Tests;

public class ComPortsTests
{

    protected ComPorts comPorts;

    [SetUp]
    public void Setup()
    {
        comPorts = new ComPorts();
    }

    [Test]
    public async Task TestOneEventAsyncWithInserted()
    {
        await comPorts.OneEventAsync(ComWatcherEventType.Inserted);
    }

    [Test]
    public async Task TestOneEventAsyncWithRemoved()
    {
        await comPorts.OneEventAsync(ComWatcherEventType.Removed);
    }

    [Test]
    public async Task TestOneEventAsyncWithAnyButInserted()
    {
        await comPorts.OneEventAsync(ComWatcherEventType.Any);
    }

    [Test]
    public async Task TestOneEventAsyncWithAnyButRemoved()
    {
        await comPorts.OneEventAsync(ComWatcherEventType.Any);
    }

    [Test]
    public async Task TestOnePortEventByNameWithInsert()
    {
        await comPorts.OnePortEventByNameAsync("COM4", ComWatcherEventType.Inserted);
    }

    [Test]
    public async Task TestOnePortEventByNameWithRemove()
    {
        await comPorts.OnePortEventByNameAsync("COM4", ComWatcherEventType.Removed);
    }

    [Test]
    public async Task TestOnePortEventByNameWithAny()
    {
        await comPorts.OnePortEventByNameAsync("COM4", ComWatcherEventType.Any);
    }

}