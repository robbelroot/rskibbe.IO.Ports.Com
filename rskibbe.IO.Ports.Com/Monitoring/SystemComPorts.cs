using rskibbe.IO.Ports.Com.Monitoring.Exceptions;
using rskibbe.IO.Ports.Com.Monitoring.ValueObjects;
using System.Diagnostics;
using System.IO.Ports;
using System.Management;

namespace rskibbe.IO.Ports.Com.Monitoring
{
    public class SystemComPorts
    {
        const string QUERY = "SELECT * FROM __InstanceOperationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_SerialPort'";

        protected ManagementEventWatcher? watcher;

        protected List<string> ExistingPorts { get; set; }

        public ComWatcherMode Mode { get; protected set; }

        public ComWatcherEventType SingleTargetEvent { get; private set; }

        public bool IsWatching { get; protected set; }

        public bool StopRequested { get; protected set; }

        public SystemComPorts()
        {
            ExistingPorts = new List<string>();
            InitializeWatcher();
        }

        protected void InitializeWatcher()
        {
#pragma warning disable CA1416 // Plattformkompatibilität überprüfen
            watcher = new ManagementEventWatcher(QUERY);
            watcher.Stopped += Watcher_Stopped;
            watcher.EventArrived += Watcher_EventArrived;
#pragma warning restore CA1416 // Plattformkompatibilität überprüfen
        }

        /// <summary>
        /// Watches for COM port changes in an event based pattern
        /// </summary>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public void StartWatching()
        {
            ThrowIfAlreadyRunning();
            Start();
        }

        /// <summary>
        /// Watches / triggers for one insertion/removal in an event based pattern
        /// </summary>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public void StartWatchingForOneEvent(ComWatcherEventType eventType = ComWatcherEventType.Any)
        {
            ThrowIfAlreadyRunning();
            Mode = ComWatcherMode.Single;
            SingleTargetEvent = eventType;
            Start();
        }

        /// <summary>
        /// Watches for one insertion AND one removal (cycle) in an event based pattern
        /// </summary>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public void StartWatchingForOneEventCycle()
        {
            ThrowIfAlreadyRunning();
            Mode = ComWatcherMode.Cycle;
            Start();
        }

        /// <summary>
        /// Waits async for a specific COM port event to occur
        /// </summary>
        /// <param name="portNameOrId">For example 3 for COM3 - use id 2 to 256</param>
        /// <exception cref="ArgumentException">If an invalid portId has been provided</exception>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public Task<ComEventArgs> OnePortEventByIdAsync(byte portId, ComWatcherEventType eventType = ComWatcherEventType.Any)
        {
            ThrowIfAlreadyRunning();
            if (portId < 2)
                throw new ArgumentException($"Please provide a valid id - 2 to 256", nameof(portId));
            // cant be created as out or return variable inside AttachRemoveAfterExecutedHandler
            // lambda expressions dont allow..
            var tcs = new TaskCompletionSource<ComEventArgs>();
            Mode = ComWatcherMode.Single;
            SingleTargetEvent = eventType;
            var idComparer = (ComEventArgs e) => e.PortId == portId;
            AttachRemoveAfterExecutedHandler(SingleTargetEvent, tcs, idComparer);
            Start();
            return tcs.Task;
        }

        /// <summary>
        /// Waits async for a specific COM port event to occur
        /// </summary>
        /// <param name="portNameOrId">For example COM3 - use id 2 to 256</param>
        /// <exception cref="ArgumentException">If an invalid portName has been provided</exception>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public Task<ComEventArgs> OnePortEventByNameAsync(string portName, ComWatcherEventType eventType = ComWatcherEventType.Any)
        {
            var lowerPortName = portName.ToLower();
            var notStartingWithCom = !lowerPortName.StartsWith("com");
            if (notStartingWithCom)
                throw new ArgumentException($"Please provide a valid portName: COM<id - 2 to 256>", nameof(portName));

            portName = lowerPortName.Replace("com", "");
            if (!byte.TryParse(portName, out var portId))
                throw new ArgumentException($"Please provide a valid portName: COM<id - 2 to 256>", nameof(portName));

            return OnePortEventByIdAsync(portId, eventType);
        }

        private void AttachRemoveAfterExecutedHandler(
            ComWatcherEventType eventType,
            TaskCompletionSource<ComEventArgs> tcs,
            Func<ComEventArgs, bool>? comparer = null
        )
        {
            EventHandler<ComEventArgs>? del = null;
            del = delegate (object? sender, ComEventArgs e)
            {
                // sadly i didnt get the switch working with getting the
                // event dynamically and then using +=... -= on it..
                if (comparer == null || comparer(e))
                {
                    switch (eventType)
                    {
                        case ComWatcherEventType.Any:
                            ComChanged -= del;
                            break;
                        case ComWatcherEventType.Inserted:
                            ComInserted -= del;
                            break;
                        case ComWatcherEventType.Removed:
                            ComRemoved -= del;
                            break;
                    }
                    tcs.SetResult(e);
                }
            };
            switch (eventType)
            {
                case ComWatcherEventType.Any:
                    ComChanged += del;
                    break;
                case ComWatcherEventType.Inserted:
                    ComInserted += del;
                    break;
                case ComWatcherEventType.Removed:
                    ComRemoved += del;
                    break;
            }
        }

        /// <summary>
        /// Waits async for a COM port event to occur
        /// </summary>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public Task<ComEventArgs> OneEventAsync(ComWatcherEventType eventType = ComWatcherEventType.Any)
        {
            ThrowIfAlreadyRunning();
            var tcs = new TaskCompletionSource<ComEventArgs>();
            Mode = ComWatcherMode.Single;
            SingleTargetEvent = eventType;
            AttachRemoveAfterExecutedHandler(SingleTargetEvent, tcs);
            Start();
            return tcs.Task;
        }

        /// <summary>
        /// Waits async for a COM port event cycle (connect & disconnect) to occur
        /// </summary>
        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        public Task<ComEventArgs> OneEventCycleAsync()
        {
            ThrowIfAlreadyRunning();
            var tcs = new TaskCompletionSource<ComEventArgs>();
            Mode = ComWatcherMode.Cycle;
            EventHandler<ComEventArgs>? del = null;
            del = delegate (object? sender, ComEventArgs e)
            {
                ComCycleCompleted -= del;
                tcs.SetResult(e);
            };
            ComCycleCompleted += del;
            Start();
            return tcs.Task;
        }

        /// <exception cref="ComWatcherAlreadyRunningException">If the watcher is already running</exception>
        protected void ThrowIfAlreadyRunning()
        {
            if (IsWatching)
                throw new ComWatcherAlreadyRunningException();
        }

        /// <summary>
        /// Loads existing COM ports internally and starts watching changes to them
        /// </summary>
        protected void Start()
        {
            RefreshExistingPorts();
#pragma warning disable CA1416 // Plattformkompatibilität überprüfen
            watcher!.Start();
#pragma warning restore CA1416 // Plattformkompatibilität überprüfen
            IsWatching = true;
            OnStartedWatching(EventArgs.Empty);
        }

        protected void RefreshExistingPorts()
        {
            ExistingPorts.Clear();
            // replace by exchangeable component with interface
            ExistingPorts.AddRange(SerialPort.GetPortNames());
        }

        protected void OnStartedWatching(EventArgs e)
        {
            StartedWatching?.Invoke(this, e);
        }

        /// <exception cref="ComWatcherNotRunningException">If the watcher is not running, but a stop has been requested</exception>
        /// <exception cref="ComWatcherStopAlreadyRequestedException">If a stop request is already running</exception>
        public void StopWatching()
        {
            if (!IsWatching)
                throw new ComWatcherNotRunningException("The Watcher isn't running, so it cannot be stopped");

            if (StopRequested)
                throw new ComWatcherStopAlreadyRequestedException("A stop has already been requested");

            StopRequested = true;
#pragma warning disable CA1416 // Plattformkompatibilität überprüfen
            watcher!.Stop();
#pragma warning restore CA1416 // Plattformkompatibilität überprüfen
        }

        private void Watcher_Stopped(object sender, StoppedEventArgs e)
        {
            IsWatching = false;
            StopRequested = false;
            OnStoppedWatching(EventArgs.Empty);
        }

        protected void OnStoppedWatching(EventArgs e)
        {
            StoppedWatching?.Invoke(this, e);
        }

        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            var portCountBefore = ExistingPorts.Count;
            // replace by exchangeable component with interface
            var portCountNow = SerialPort.GetPortNames().Count();
            var deviceInserted = portCountBefore < portCountNow;
            var deviceRemoved = portCountBefore > portCountNow;
            if (deviceInserted)
                OnDeviceInserted();
            else if (deviceRemoved)
                OnDeviceRemoved();
            else
                Debug.WriteLine($"Not changed - wrong WMI query?");
        }

        protected void OnDeviceInserted()
        {
            // replace by exchangeable component with interface
            var insertedPortName = SerialPort.GetPortNames().Except(ExistingPorts).First();
            OnComChanged(new ComChangedEventArgs(insertedPortName, ComWatcherEventType.Inserted));
            var comEventArgs = new ComEventArgs(insertedPortName);
            OnComInserted(comEventArgs);

            var isSingleMode = Mode == ComWatcherMode.Single;
            var isInterested = SingleTargetEvent == ComWatcherEventType.Any || SingleTargetEvent == ComWatcherEventType.Inserted;
            if (isSingleMode && isInterested)
            {
                StopWatching();
                return;
            }
        }

        protected void OnDeviceRemoved()
        {
            // replace by exchangeable component with interface
            var removedPortName = ExistingPorts.Except(SerialPort.GetPortNames()).First();
            OnComChanged(new ComChangedEventArgs(removedPortName, ComWatcherEventType.Removed));
            var comEventArgs = new ComEventArgs(removedPortName);
            OnComRemoved(comEventArgs);

            var isSingleMode = Mode == ComWatcherMode.Single;
            var isInterested = SingleTargetEvent == ComWatcherEventType.Any || SingleTargetEvent == ComWatcherEventType.Removed;
            if (isSingleMode && isInterested)
            {
                StopWatching();
                return;
            }

            var isCycleMode = Mode == ComWatcherMode.Cycle;
            var wasInsertedBefore = ExistingPorts.Contains(removedPortName);
            if (isCycleMode && wasInsertedBefore)
            {
                OnCycleCompleted(comEventArgs);
                StopWatching();
                return;
            }
        }

        protected void OnComChanged(ComChangedEventArgs e)
        {
            ComChanged?.Invoke(this, e);
        }

        protected void OnComInserted(ComEventArgs e)
        {
            ExistingPorts.Add(e.PortName);
            ComInserted?.Invoke(this, e);
        }

        protected void OnComRemoved(ComEventArgs e)
        {
            ExistingPorts.Remove(e.PortName);
            ComRemoved?.Invoke(this, e);
        }

        protected void OnCycleCompleted(ComEventArgs e)
        {
            ComCycleCompleted?.Invoke(this, e);
        }

        public event EventHandler? StartedWatching;

        public event EventHandler? StoppedWatching;

        public event EventHandler<ComEventArgs>? ComChanged;

        public event EventHandler<ComEventArgs>? ComInserted;

        public event EventHandler<ComEventArgs>? ComRemoved;

        public event EventHandler<ComEventArgs>? ComCycleCompleted;

    }
}
