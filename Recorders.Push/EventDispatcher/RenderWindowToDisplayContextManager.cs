using Recorders.Push.Window;
using SFML.Graphics;

namespace Recorders.Push.EventDispatcher;

class RenderWindowToDisplayContextManager: IEventDispatcher<RenderWindow, DisplayContextManager>
{
    public void SetUp(RenderWindow source, DisplayContextManager destination)
    {
        source.KeyReleased += destination.InvokeKeyReleased;
    }
}