using Recorders.Push.EventHandlers;
using Recorders.Push.SceneObjects.Primitives;
using Recorders.Push.Window;
using SFML.System;

namespace Recorders.Push.SceneObjects;

class SingleCubeAnimationSequence: IAnimatable
{
    private readonly List<IDrawableAnimation> _sequence;
    private IDrawableAnimation _current;
    private bool _started;
    private readonly MainWindow _window;
    private bool _draw;
    
    public SingleCubeAnimationSequence(MainWindow window, Vector2f direction, bool draw = true,
        IEventHandler<IAnimatable>? eventHandler = null)
    {
        _window = window;
        _draw = draw;
        _sequence =
        [
            new AnimatedArrow(direction, Fade.In, TimeSpan.FromSeconds(1))
            {
                Position = (Vector2f)window.Size / 2
            },
            new AnimatedArrow(direction, Fade.Out, TimeSpan.FromSeconds(1))
            {
                Position = (Vector2f)window.Size / 2
            },
            new AnimatedCube(direction, 80, TimeSpan.FromSeconds(3), new AnimatedCubeEventHandler(ApiClient.Instance))
            {
                Position = (Vector2f)(window.Size/2)
            }
        ];
        _current = _sequence[0];
        if(draw) Draw(window);
        eventHandler?.Register(this);
    }
    
    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if (!_started)
        {
            Start?.Invoke();
            _started = true;
        }
        _current.UpdateAnimation(window, deltaTime);
        if (!_current.Done) return;
        int index = _sequence.IndexOf(_current);
        if (index >= _sequence.Count - 1)
        {
            Finish?.Invoke();
            return;
        }
        index++;
        window.RemoveDrawable(_current);
        _current = _sequence[index];
        window.AddDrawable(_current);
    }

    public void ResetState()
    {
        if(_started && !Done) Finish?.Invoke();
        foreach (var sequenceElement in _sequence)
        {
            sequenceElement.ResetState();
            sequenceElement.Position = (Vector2f)(_window.Size / 2);
        }
        _window.RemoveDrawable(_current);
        _current = _sequence[0];
        _started = false;
        if(_draw)_window.AddDrawable(_current);
    }

    public bool Done => _sequence.IndexOf(_current) == _sequence.Count - 1 && _current.Done;
    public event Action? Start;
    public event Action? Finish;

    public void Draw(MainWindow window) => window.AddDrawable(_current);
    public void Remove(MainWindow window) => window.RemoveDrawable(_current);
}