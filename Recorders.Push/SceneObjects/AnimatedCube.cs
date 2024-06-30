using Recorders.Push.Window;
using SFML.Graphics;
using SFML.System;

namespace Recorders.Push.SceneObjects;

class AnimatedCube: RectangleShape, IDrawableAnimation
{
    private readonly Vector2f _direction;
    private readonly float _velocity;
    private readonly TimeSpan _duration;
    private TimeSpan _alreadyDone = TimeSpan.Zero;
    private bool _done;
    
    public AnimatedCube(Vector2f direction, float velocity, TimeSpan duration): base(new Vector2f(100, 100))
    {
        _direction = direction;
        _velocity = velocity;
        _duration = duration;
        FillColor = Color.Red;
        Origin = new Vector2f(50, 50);
    }

    public bool Done => _done;

    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if (_done) return;
        Position += _direction * _velocity * deltaTime;
        if (_alreadyDone + TimeSpan.FromSeconds(deltaTime) > _duration) _done = true;
        _alreadyDone += TimeSpan.FromSeconds(deltaTime);
    }
}