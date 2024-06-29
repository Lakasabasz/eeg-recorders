using System.Runtime.CompilerServices;
using SFML.Graphics;
using SFML.Window;

namespace push;

class MainWindowEventHandler
{
    private static T RequireNotNull<T>(object? o, [CallerArgumentExpression(nameof(o))] string caller = "")
    {
        if (o is null) throw new NullReferenceException(caller);
        return (T)o;
    }
    
    public void Register(RenderWindow window)
    {
        window.Closed += WindowOnClosed;
    }

    private void WindowOnClosed(object? sender, EventArgs e) => RequireNotNull<Window>(sender).Close();
}