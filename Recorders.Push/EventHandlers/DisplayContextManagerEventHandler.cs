using Recorders.Push.Window;
using SFML.Window;

namespace Recorders.Push.EventHandlers;

class DisplayContextManagerEventHandler: IEventHandler<DisplayContextManager>
{
    private DisplayContextManager? _eventContainer;
    
    public void Register(DisplayContextManager eventContainer)
    {
        _eventContainer = eventContainer;
        eventContainer.KeyReleased += OnKeyReleased;
    }

    private void OnKeyReleased(object? sender, KeyEventArgs e)
    {
        if (e.Code is not (Keyboard.Key.Enter or Keyboard.Key.Space)) return;
        _eventContainer.ResetAnimation();
    }
}