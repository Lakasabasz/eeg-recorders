using System.Diagnostics;
using Recorders.Push.EventHandlers;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Recorders.Push.Window;

class MainWindow
{
    private readonly RenderWindow _window;
    private readonly List<Drawable> _drawables = [];
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

    public void AddDrawable(Drawable drawable) => _drawables.Add(drawable);
    public void RemoveDrawable(Drawable drawable) => _drawables.Remove(drawable);

    public void Run()
    {
        _manager.Init(this);

        var fpsStopwatch = new Stopwatch();
        var contextStopwatch = new Stopwatch();
        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            _manager.LoopTick((float)contextStopwatch.Elapsed.TotalSeconds);
            contextStopwatch.Restart();
            foreach (var drawable in _drawables) _window.Draw(drawable);

            _window.Display();
            var progress = fpsStopwatch.Elapsed;
            var diff = _fpsTargetTime - progress;
            if (diff < TimeSpan.FromMilliseconds(1)) Console.WriteLine($"[Warning] Time slip => {-diff}");
            else Thread.Sleep(diff);
            fpsStopwatch.Restart();
        }
    }
}