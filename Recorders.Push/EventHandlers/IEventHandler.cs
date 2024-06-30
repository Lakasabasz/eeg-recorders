namespace Recorders.Push.EventHandlers;

interface IEventHandler<in T>
{
    void Register(T eventContainer);
}