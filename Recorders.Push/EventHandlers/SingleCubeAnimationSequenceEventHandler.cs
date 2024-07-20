using Recorders.Push.SceneObjects;

namespace Recorders.Push.EventHandlers;

class SingleCubeAnimationSequenceEventHandler(ApiClient client, string label): IEventHandler<IAnimatable>
{
    public void Register(IAnimatable eventContainer)
    {
        eventContainer.Start += SingleCubeAnimationSequenceOnStart;
    }

    private void SingleCubeAnimationSequenceOnStart()
    {
        var resp = client.SetLabel(label);
        if(resp is not null) Console.WriteLine($"Recording starting error: {resp.Message}");
    }
}