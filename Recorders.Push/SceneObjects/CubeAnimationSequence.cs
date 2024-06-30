using Recorders.Push.Window;
using SFML.System;

namespace Recorders.Push.SceneObjects;

class CubeAnimationSequence: IAnimatable
{
    private readonly List<IDrawableAnimation> _sequence;
    private IDrawableAnimation _current;

    public CubeAnimationSequence(MainWindow window, Vector2f direction, bool draw = true)
    {
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
            new AnimatedCube(direction, 80, TimeSpan.FromSeconds(3))
            {
                Position = (Vector2f)(window.Size/2)
            }
        ];
        _current = _sequence[0];
        if(draw) window.AddDrawable(_current);
    }
    
    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        _current.UpdateAnimation(window, deltaTime);
        if (!_current.Done) return;
        int index = _sequence.IndexOf(_current);
        if (index >= _sequence.Count - 1) return;
        index++;
        window.RemoveDrawable(_current);
        _current = _sequence[index];
        window.AddDrawable(_current);
    }

    public bool Done => _sequence.IndexOf(_current) == _sequence.Count - 1 && _current.Done;

    public void Draw(MainWindow window) => window.AddDrawable(_current);
    public void Remove(MainWindow window) => window.RemoveDrawable(_current);
}