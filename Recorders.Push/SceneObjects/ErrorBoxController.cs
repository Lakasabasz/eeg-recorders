using Recorders.Push.SceneObjects.Primitives;
using Recorders.Push.Window;

namespace Recorders.Push.SceneObjects;

class ErrorBoxController(MainWindow window): IAnimatable
{
    private readonly Dictionary<Fade, ErrorBox> _errorBoxes = new()
    {
        [Fade.In] = new ErrorBox(window, "Api timeout", Fade.In),
        [Fade.Out] = new ErrorBox(window, "Api timeout", Fade.Out)
    };

    private Fade? _current;
    private bool _lastApiState = true;
    
    public void UpdateAnimation(MainWindow window, float deltaTime)
    {
        if (_lastApiState != GlobalData.ApiOnline)
        {
            if(_current is not null) window.RemoveDrawable(_errorBoxes[_current.Value]);
            var oldCurrent = _current;
            _current = _lastApiState switch
            {
                true when !GlobalData.ApiOnline => Fade.In,
                false when GlobalData.ApiOnline => Fade.Out,
                _ => throw new InvalidOperationException("Change was detected, but now it can't be detected")
            };
            if(oldCurrent != _current) _errorBoxes[_current.Value].ResetState();
            window.AddDrawable(_errorBoxes[_current.Value], 1000);
        }

        if (_current is null) return;
        _errorBoxes[_current.Value].UpdateAnimation(window, deltaTime);
        if (_errorBoxes[_current.Value].Done)
        {
            _lastApiState = _current == Fade.Out;
        }
    }

    public void ResetState(){}

    public bool Done => false;
    public event Action? Start;
    public event Action? Finish;
}