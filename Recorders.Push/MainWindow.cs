using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace push;

class MainWindow
{
    private readonly RenderWindow _window;
    private readonly Dictionary<Guid, Drawable> _drawables = new();
    private readonly DisplayContextManager _manager;
    private TimeSpan _fpsTargetTime;

    public MainWindow(VideoMode size, MainWindowEventHandler eventHandler, DisplayContextManager manager)
    {
        _window = new(size, "Push recorder", Styles.Default, new ContextSettings(){AntialiasingLevel = 3});
        eventHandler.Register(_window);
        _fpsTargetTime = TimeSpan.FromSeconds(1) / 60.0;
        _manager = manager;
    }

    public Vector2u Size => _window.Size;

    public Guid AddDrawable(Drawable drawable)
    {
        Guid objectId = Guid.NewGuid();
        _drawables.Add(objectId, drawable);
        return objectId;
    }

    public void Run()
    {
        _manager.Init(this);

        var fpsStopwatch = new Stopwatch();
        var contextStopwatch = new Stopwatch();
        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            _manager.LoopTick(this, (float)contextStopwatch.Elapsed.TotalSeconds);
            contextStopwatch.Restart();
            foreach (var drawable in _drawables.Values) _window.Draw(drawable);

            _window.Display();
            var progress = fpsStopwatch.Elapsed;
            var diff = _fpsTargetTime - progress;
            if (diff < TimeSpan.FromMilliseconds(1)) Console.WriteLine($"[Warning] Time slip => {-diff}");
            else Thread.Sleep(diff);
            fpsStopwatch.Restart();
        }
    }
}