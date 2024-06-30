using Recorders.Push.Window;

namespace Recorders.Push.SceneObjects;

interface IAnimatable
{
    public void UpdateAnimation(MainWindow window, float deltaTime);
    public bool Done { get; }
}