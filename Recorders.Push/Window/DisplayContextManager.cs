using System.Data;
using Recorders.Push.SceneObjects;
using Recorders.Push.SceneObjects.Primitives;
using SFML.Graphics;
using SFML.System;

namespace Recorders.Push.Window;

class DisplayContextManager
{
    private MainWindow? _window;
    private RectangleShape? _background;
    private readonly List<IAnimatable> _animatables = [];
    
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
        //InitArrow();
        InitCubeSequence();
        //InitCubeAnimationSequence();
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
    
    private void InitCube()
    {
        var cube = new AnimatedCube(new Vector2f(1, 0), 80, TimeSpan.FromSeconds(2));
        cube.Position = (Vector2f)(_window!.Size / 2);
        _animatables.Add(cube);
    }

    private void InitArrow()
    {
        var arrow = new AnimatedArrow(new Vector2f(1, 0), Fade.In, TimeSpan.FromSeconds(3));
        arrow.Position = (Vector2f)(_window!.Size / 2) + new Vector2f(100, 0);
        _animatables.Add(arrow);
        _window.AddDrawable(arrow);
        
        var arrow2 = new AnimatedArrow(new Vector2f(0, 1), Fade.In, TimeSpan.FromSeconds(3));
        arrow2.Position = (Vector2f)(_window!.Size / 2) + new Vector2f(0, 100);
        _animatables.Add(arrow2);
        _window.AddDrawable(arrow2);
        
        var arrow3 = new AnimatedArrow(new Vector2f(-1, 0), Fade.In, TimeSpan.FromSeconds(3));
        arrow3.Position = (Vector2f)(_window!.Size / 2) - new Vector2f(100, 0);
        _animatables.Add(arrow3);
        _window.AddDrawable(arrow3);
        
        var arrow4 = new AnimatedArrow(new Vector2f(0, -1), Fade.In, TimeSpan.FromSeconds(3));
        arrow4.Position = (Vector2f)(_window!.Size / 2) - new Vector2f(0, 100);
        _animatables.Add(arrow4);
        _window.AddDrawable(arrow4);
    }

    private void InitCubeAnimationSequence()
    {
        var animationSequence = new CubeAnimationSequence(_window!, new Vector2f(1, 0));
        _animatables.Add(animationSequence);
    }

    private void InitCubeSequence()
    {
        var sequence = new AnimationSequence(_window!);
        _animatables.Add(sequence);
    }
}