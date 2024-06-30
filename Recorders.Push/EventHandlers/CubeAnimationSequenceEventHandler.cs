using Recorders.Push.SceneObjects;

namespace Recorders.Push.EventHandlers;

class CubeAnimationSequenceEventHandler(ApiClient client, string label): IEventHandler<IAnimatable>
{
    public void Register(IAnimatable eventContainer)
    {
        eventContainer.Start += CubeAnimationSequenceOnStart;
    }

    private void CubeAnimationSequenceOnStart()
    {
        var resp = client.SetLabel(label);
        if(resp is not null) Console.WriteLine($"Recording starting error: {resp.Message}");
    }
}