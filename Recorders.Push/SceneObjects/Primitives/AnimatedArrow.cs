using Recorders.Push.Extensions;
using Recorders.Push.Window;
using SFML.Graphics;
using SFML.System;

namespace Recorders.Push.SceneObjects.Primitives;

enum Fade
{
    In, Out
}

class AnimatedArrow: ConvexShape, IDrawableAnimation
{
    private readonly Fade _fade;
    private readonly TimeSpan _duration;
    private TimeSpan _alreadyDone = TimeSpan.Zero;
    private bool _done;
    
    public AnimatedArrow(Vector2f direction, Fade fade, TimeSpan duration): base(7)
    {
        SetPoint(0, new Vector2f(-12.5f, -50));
        SetPoint(1, new Vector2f(-12.5f, 0));
        SetPoint(2, new Vector2f(-25, 0));
        SetPoint(3, new Vector2f(0, 50));
        SetPoint(4, new Vector2f(25, 0));
        SetPoint(5, new Vector2f(12.5f, 0));
        SetPoint(6, new Vector2f(12.5f, -50));
        _fade = fade;
        _duration = duration;
        FillColor = new Color(Color.Blue)
        {
            A = (byte)(_fade == Fade.In ? 0 : 255)
        };
        Rotation = direction.Angle(new Vector2f(0, 1));
    }
    
    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if (_done) return;
        
        if (_alreadyDone + TimeSpan.FromSeconds(deltaTime) > _duration) _done = true;
        _alreadyDone += TimeSpan.FromSeconds(deltaTime);

        var percentDone = _alreadyDone > _duration ? 1.0 : _alreadyDone / _duration;
        FillColor = new Color(FillColor)
        {
            A = (byte)(_fade == Fade.In ? 255 * percentDone : 255 * (1 - percentDone))
        };
    }

    public bool Done => _done;
    public event Action? Start;
    public event Action? Finish;
}