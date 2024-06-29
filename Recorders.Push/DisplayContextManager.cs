using System.Data;
using SFML.Graphics;
using SFML.System;

namespace push;

class DisplayContextManager
{
    private Guid? _cubeId;
    private RectangleShape? _cube;
    private DateTime? _lastTime = null;
    
    public void Init(MainWindow window)
    {
        InitBackground(window);
        InitCube(window);
    }

    public void LoopTick(MainWindow window, float deltaSeconds)
    {
        if (_cube is null || _cubeId is null) throw new NoNullAllowedException(nameof(_cube));
        _cube.Position += new Vector2f(40, 0) * deltaSeconds;
    }

    private static void InitBackground(MainWindow window)
    {
        var rect = new RectangleShape((Vector2f) window.Size);
        rect.FillColor = Color.White;
        window.AddDrawable(rect);
    }
    
    private void InitCube(MainWindow window)
    {
        _cube = new RectangleShape(new Vector2f(100, 100));
        _cube.FillColor = Color.Red;
        _cube.Origin = new Vector2f(50, 50);
        _cube.Position = (Vector2f)(window.Size / 2);
        _cubeId = window.AddDrawable(_cube);
    }
}