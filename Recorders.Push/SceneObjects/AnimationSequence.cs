using Recorders.Push.Window;
using SFML.System;

namespace Recorders.Push.SceneObjects;

enum Direction
{
    Top, Left, Bottom, Right
}

class AnimationSequence: IAnimatable
{
    private Dictionary<Direction, AnimatedCube> _cubes = new()
    {
        [Direction.Bottom] = new AnimatedCube(new Vector2f(0, 1), 80, TimeSpan.FromSeconds(3)),
        [Direction.Top] = new AnimatedCube(new Vector2f(0, -1), 80, TimeSpan.FromSeconds(3)),
        [Direction.Left] = new AnimatedCube(new Vector2f(-1, 0), 80, TimeSpan.FromSeconds(3)),
        [Direction.Right] = new AnimatedCube(new Vector2f(1, 0), 80, TimeSpan.FromSeconds(3))
    };

    private List<Direction> _sequence = ResetSequence();

    private Direction _current;

    public AnimationSequence(MainWindow window)
    {
        _current = _sequence.First();
        foreach (var (_, cube) in _cubes) cube.Position = (Vector2f)(window.Size / 2);
        window.AddDrawable(_cubes[_current]);
    }

    private static List<Direction> ResetSequence()
    {
        return ((IEnumerable<Direction>) [Direction.Top, Direction.Left, Direction.Bottom, Direction.Right])
            .OrderBy(_ => Guid.NewGuid()).ToList();
    }

    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        _cubes[_current].UpdateAnimation(window, deltaTime);
        if (!_cubes[_current].Done) return;
        int index = _sequence.IndexOf(_current);
        if (index >= 3) return;
        index++;
        window.RemoveDrawable(_cubes[_current]);
        _current = _sequence[index];
        window.AddDrawable(_cubes[_current]);
    }
}