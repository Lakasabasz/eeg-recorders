using System.Data;
using Recorders.Push.EventHandlers;
using Recorders.Push.SceneObjects;
using Recorders.Push.SceneObjects.Primitives;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Recorders.Push.Window;

class DisplayContextManager
{
    private MainWindow? _window;
    private RectangleShape? _background;
    private readonly List<IAnimatable> _animatables = [];

    public DisplayContextManager(DisplayContextManagerEventHandler eventHandler)
    {
        eventHandler.Register(this);
    }
    
    private RectangleShape? Background
    {
        get => _background;
        set
        {
            if(_window is null) throw new NoNullAllowedException(nameof(_background)); 
            if (value is null)
            {
                if (_background is not null) _window.RemoveDrawable(_background);
            }
            else _window.AddDrawable(value);
            _background = value;
        }
    }

    public void Init(MainWindow window)
    {
        _window = window;
        InitBackground();
        InitErrorBox();
        InitCubeSequence();
    }

    public void LoopTick(float deltaSeconds)
    {
        foreach (var animatable in _animatables) animatable.UpdateAnimation(Tools.RequireNotNull<MainWindow>(_window), deltaSeconds);
    }

    private void InitBackground()
    {
        Background = new RectangleShape((Vector2f)_window!.Size);
        Background.FillColor = Color.White;
    }

    private void InitCubeSequence()
    {
        var sequence = new CubesAnimationSequence(_window!);
        _animatables.Add(sequence);
    }

    private void InitErrorBox()
    {
        var sequence = new ErrorBoxController(_window!);
        _animatables.Add(sequence);
    }
    
    public void ResetAnimation()
    {
        foreach (var animatable in _animatables) animatable.ResetState();
    }

    public event EventHandler<KeyEventArgs>? KeyReleased;

    public void InvokeKeyReleased(object? sender, KeyEventArgs e) => KeyReleased?.Invoke(sender, e);
}