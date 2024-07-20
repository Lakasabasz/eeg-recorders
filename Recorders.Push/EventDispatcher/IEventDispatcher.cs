namespace Recorders.Push.EventDispatcher;

interface IEventDispatcher<in TEventSource, in TEventDestination>
{
    void SetUp(TEventSource source, TEventDestination destination);
}