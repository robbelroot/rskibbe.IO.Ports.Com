using Moq;
using NUnit.Framework;
using rskibbe.IO.Ports.Com.System;
using rskibbe.IO.Ports.Com.ValueObjects;
using rskibbe.IO.Ports.Com.Virtual;
using rskibbe.IO.Ports.Com.Virtual.ValueObjects;

namespace rskibbe.IO.Ports.Com.Tests
{
    public class ComPortsTests
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestSystemComPortAddedBySystemComPortCreation()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var eventRaised = false;
            comPorts.SystemComPortAdded += (s, e) => eventRaised = true;
            systemComPortsMock.Raise(x => x.SystemComPortAdded += null, new ComPortEventArgs("COM3"));
            Assert.True(eventRaised);
        }

        [Test]
        public void TestSystemComPortRemovedBySystemComPortRemoval()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var eventRaised = false;
            comPorts.SystemComPortRemoved += (s, e) => eventRaised = true;
            systemComPortsMock.Raise(x => x.SystemComPortRemoved += null, new ComPortEventArgs("COM3"));
            Assert.True(eventRaised);
        }

        [Test]
        public void TestVirtualComPortsAddedByVirtualComPortsCreation()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var eventRaised = false;
            comPorts.VirtualComPortsAdded += (s, e) => eventRaised = true;
            virtualComPortsMock.Raise(x => x.VirtualComPortsAdded += null, new VirtualComPortsEventArgs("COM3", "COM4"));
            Assert.True(eventRaised);
        }

        [Test]
        public void TestVirtualComPortsRemovedByVirtualComPortsRemoval()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var eventRaised = false;
            comPorts.VirtualComPortsRemoved += (s, e) => eventRaised = true;
            virtualComPortsMock.Raise(x => x.VirtualComPortsRemoved += null, new VirtualComPortsEventArgs("COM3", "COM4"));
            Assert.True(eventRaised);
        }

        [Test]
        public void TestTwoPortsAddedByVirtualComPortsCreation()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var eventRaisedCount = 0;
            comPorts.PortAdded += (s, e) => eventRaisedCount++;
            virtualComPortsMock.Raise(x => x.VirtualComPortsAdded += null, new VirtualComPortsEventArgs("COM3", "COM4"));
            Assert.That(eventRaisedCount, Is.EqualTo(2));
        }

        [Test]
        public void TestTwoPortsRemovedByVirtualComPortsRemoval()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var eventRaisedCount = 0;
            comPorts.PortRemoved += (s, e) => eventRaisedCount++;
            virtualComPortsMock.Raise(x => x.VirtualComPortsRemoved += null, new VirtualComPortsEventArgs("COM3", "COM4"));
            Assert.That(eventRaisedCount, Is.EqualTo(2));
        }

        [Test]
        public void TestPortAddedEventAsyncWithTimeout()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.PortAddedEventAsync("COM4");
            Assert.False(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestPortAddedEventAsyncBySystemPortAdded()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.PortAddedEventAsync("COM4");
            systemComPortsMock.Raise(x => x.SystemComPortAdded += null, new ComPortEventArgs("COM4"));
            Assert.True(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestPortRemovedEventAsyncWithTimeout()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.PortRemovedEventAsync("COM4");
            Assert.False(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestPortRemovedEventAsyncBySystemPortRemoved()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.PortRemovedEventAsync("COM4");
            systemComPortsMock.Raise(x => x.SystemComPortRemoved += null, new ComPortEventArgs("COM4"));
            Assert.True(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestAnyPortAddedEventAsyncWithTimeout()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.AnyPortAddedEventAsync();
            Assert.False(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestAnyPortAddedEventAsyncBySystemPortAdded()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.AnyPortAddedEventAsync();
            systemComPortsMock.Raise(x => x.SystemComPortAdded += null, new ComPortEventArgs("COM4"));
            Assert.True(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestAnyPortRemovedEventAsyncWithTimeout()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.AnyPortRemovedEventAsync();
            Assert.False(task.IsCompletedSuccessfully);
        }

        [Test]
        public void TestAnyPortRemovedEventAsyncBySystemPortAdded()
        {
            var systemComPortsMock = new Mock<ISystemComPorts>();
            var virtualComPortsMock = new Mock<IVirtualComPorts>();
            var systemComPorts = systemComPortsMock.Object;
            var virtualComPorts = virtualComPortsMock.Object;
            var comPorts = new ComPorts(systemComPorts, virtualComPorts);
            var task = comPorts.AnyPortRemovedEventAsync();
            systemComPortsMock.Raise(x => x.SystemComPortRemoved += null, new ComPortEventArgs("COM4"));
            Assert.True(task.IsCompletedSuccessfully);
        }

    }

}
