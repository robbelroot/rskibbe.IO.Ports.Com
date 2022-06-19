namespace rskibbe.IO.Ports.Com.Tests;

public static class AssertExtensions
{
    // doesn't work now :/
    public static async void EventAsync(object target, string eventName, int timeoutMs = 200)
    {
        var type = target.GetType();
        var eventInfo = type.GetEvent(eventName);
        if (eventInfo == null)
            throw new InvalidOperationException($"Event named {eventName} not found on object {target}");

        var handled = false;
        EventHandler<EventArgs> handler = null;
        handler = (object? sender, EventArgs e) =>
        {
            eventInfo.RemoveEventHandler(target, handler);
            handled = true;
        };
        //var argType = eventInfo.EventHandlerType!.GetGenericArguments()[0];
        //var methodInfo = typeof(AssertExtensions).GetMethod(nameof(EventAsyncDelegate));
        //var declType = Expression.GetDelegateType(new Type[] { typeof(object), eventInfo.EventHandlerType.GetGenericArguments()[0] });
        //var handler = Delegate.CreateDelegate(declType, methodInfo);
        eventInfo.AddEventHandler(target, handler);
        var started = DateTime.Now;
        do
        {
            await Task.Delay(5);
            var timeoutReached = (DateTime.Now - started).TotalMilliseconds >= timeoutMs;
            if (timeoutReached)
            {
                eventInfo.RemoveEventHandler(target, handler);
                throw new TimeoutException();
            }
            if (handled)
                break;
        } while (true);
    }

}
