using Recorders.Push.Window;
using SFML.Graphics;
using SFML.Window;

namespace Recorders.Push.EventHandlers;

class MainWindowEventHandler: IEventHandler<RenderWindow>
{
    public void Register(RenderWindow window)
    {
        window.Closed += WindowOnClosed;
        window.KeyReleased += WindowOnKeyReleased;
    }

    private void WindowOnKeyReleased(object? sender, KeyEventArgs e)
    {
        
    }

    private void WindowOnClosed(object? sender, EventArgs e) => Tools.RequireNotNull<SFML.Window.Window>(sender).Close();
}