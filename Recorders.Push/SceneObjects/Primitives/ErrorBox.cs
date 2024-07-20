using Recorders.Push.Window;
using SFML.Graphics;
using SFML.System;

namespace Recorders.Push.SceneObjects.Primitives;

class ErrorBox: IDrawableAnimation
{
    private readonly RectangleShape _background;
    private readonly Text _text;
    
    private readonly Fade _fade;
    private readonly TimeSpan _duration;
    private TimeSpan _alreadyDone = TimeSpan.Zero;
    private bool _done;

    public ErrorBox(MainWindow window, string message, Fade fade)
    {
        _duration = TimeSpan.FromSeconds(.5);
        _background = new RectangleShape((Vector2f)(window.Size / 2))
        {
            Origin = (Vector2f)(window.Size / 4),
            FillColor = new Color(255, 200, 200),
            OutlineColor = Color.Red,
            OutlineThickness = 2,
            Position = (Vector2f)(window.Size/2)
        };
        _text = new Text(message, new Font("assets/fonts/arial.ttf"), 32)
        {
            FillColor = Color.Black,
        };
        var bounds = _text.GetLocalBounds();
        _text.Origin = bounds.Size / 2;
        _text.Position = (Vector2f)(window.Size / 2);
        _fade = fade;
        
        _background.FillColor = _background.FillColor with { A = (byte)(_fade == Fade.In ? 0 : 255) };
        _text.FillColor = _text.FillColor with { A = (byte)(_fade == Fade.In ? 0 : 255) };
    }
    
    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if(_alreadyDone == TimeSpan.Zero) Start?.Invoke();
        if (Done) return;
        
        if (_alreadyDone + TimeSpan.FromSeconds(deltaTime) > _duration) Done = true;
        _alreadyDone += TimeSpan.FromSeconds(deltaTime);

        var percentDone = _alreadyDone > _duration ? 1.0 : _alreadyDone / _duration;
        var alpha = (byte)(_fade == Fade.In ? 255 * percentDone : 255 * (1 - percentDone));
        _background.FillColor = _background.FillColor with { A = alpha };
        _text.FillColor = _text.FillColor with { A = alpha };
    }

    public void ResetState()
    {
        _alreadyDone = TimeSpan.Zero;
        _done = false;
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
    
    public void Draw(RenderTarget target, RenderStates states)
    {
        _background.Draw(target, states);
        _text.Draw(target, states);
    }
}