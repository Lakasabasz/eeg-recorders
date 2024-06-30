using Recorders.Push.EventHandlers;
using Recorders.Push.Window;
using SFML.Graphics;
using SFML.System;

namespace Recorders.Push.SceneObjects.Primitives;

class AnimatedCube: RectangleShape, IDrawableAnimation
{
    private readonly Vector2f _direction;
    private readonly float _velocity;
    private readonly TimeSpan _duration;
    private TimeSpan _alreadyDone = TimeSpan.Zero;
    private bool _done;
    
    public AnimatedCube(Vector2f direction, float velocity, TimeSpan duration, IEventHandler<AnimatedCube>? eventHandler = null):
        base(new Vector2f(100, 100))
    {
        _direction = direction;
        _velocity = velocity;
        _duration = duration;
        FillColor = Color.Red;
        Origin = new Vector2f(50, 50);
        eventHandler?.Register(this);
    }

    public bool Done
    {
        get => _done;
        private set
        {
            if(value) Finish?.Invoke();
            _done = value;
        }
    }

    public event Action? Start;
    public event Action? Finish;

    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if (Done) return;
        if(_alreadyDone == TimeSpan.Zero) Start?.Invoke();
        Position += _direction * _velocity * deltaTime;
        if (_alreadyDone + TimeSpan.FromSeconds(deltaTime) > _duration) Done = true;
        _alreadyDone += TimeSpan.FromSeconds(deltaTime);
    }
}