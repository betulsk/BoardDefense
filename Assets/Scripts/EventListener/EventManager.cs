using System.Collections.Generic;

public static class EventManager<TEvent>
{
    private static readonly List<EventListener<TEvent>> Listeners = new List<EventListener<TEvent>>();

    public static void CustomAction(object sender, TEvent @event)
    {
        for(var i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i]?.Invoke(sender, @event);
        }
    }

    public static void SubscribeToEvent(EventListener<TEvent> eventListener)
    {
        Listeners.Add(eventListener);
    }

    public static void UnsubscribeToEvent(EventListener<TEvent> eventListener)
    {
        Listeners.Remove(eventListener);
    }
}
