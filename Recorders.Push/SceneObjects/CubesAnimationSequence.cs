using Recorders.Push.EventHandlers;
using Recorders.Push.Window;
using SFML.System;

namespace Recorders.Push.SceneObjects;

enum Direction
{
    Top, Left, Bottom, Right
}

class CubesAnimationSequence: IAnimatable
{
    private readonly Dictionary<Direction, SingleCubeAnimationSequence> _cubes;

    private List<Direction> _sequence = ResetSequence();

    private Direction _current;

    public CubesAnimationSequence(MainWindow window)
    {
        _cubes = new()
        {
            [Direction.Bottom] = new SingleCubeAnimationSequence(window, new Vector2f(0, 1), false, new CubeAnimationSequenceEventHandler(ApiClient.Instance, "push-bottom")),
            [Direction.Top] = new SingleCubeAnimationSequence(window, new Vector2f(0, -1), false, new CubeAnimationSequenceEventHandler(ApiClient.Instance, "push-top")),
            [Direction.Left] = new SingleCubeAnimationSequence(window, new Vector2f(-1, 0), false, new CubeAnimationSequenceEventHandler(ApiClient.Instance, "push-left")),
            [Direction.Right] = new SingleCubeAnimationSequence(window, new Vector2f(1, 0), false, new CubeAnimationSequenceEventHandler(ApiClient.Instance, "push-right"))
        };
        
        _current = _sequence.First();
        _cubes[_current].Draw(window);
    }

    private static List<Direction> ResetSequence()
    {
        return ((IEnumerable<Direction>) [Direction.Top, Direction.Left, Direction.Bottom, Direction.Right])
            .OrderBy(_ => Guid.NewGuid()).ToList();
    }

    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if (!GlobalData.ApiOnline) return;
        _cubes[_current].UpdateAnimation(window, deltaTime);
        if (!_cubes[_current].Done) return;
        int index = _sequence.IndexOf(_current);
        if (index >= 3) return;
        index++;
        _cubes[_current].Remove(window);
        _current = _sequence[index];
        _cubes[_current].Draw(window);
    }

    public void ResetState() => ResetSequence();

    public bool Done => _sequence.IndexOf(_current) == 3 && _cubes[_current].Done;
    public event Action? Start;
    public event Action? Finish;
}