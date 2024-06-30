using Recorders.Push.SceneObjects;

namespace Recorders.Push.EventHandlers;

class AnimatedCubeEventHandler(ApiClient client): IEventHandler<IAnimatable>
{
    public void Register(IAnimatable eventContainer)
    {
        eventContainer.Start += AnimatedCubeOnStart;
        eventContainer.Finish += AnimatedCubeOnFinish;
    }

    private void AnimatedCubeOnFinish()
    {
        var resp = client.StopRecording();
        if(resp is not null) Console.WriteLine($"Recording stopping error: {resp.Message}");
    }

    private void AnimatedCubeOnStart()
    {
        Task.Delay(TimeSpan.FromSeconds(0.5)).ContinueWith(x =>
        {
            var resp = client.StartRecording();
            if(resp is not null) Console.WriteLine($"Recording starting error: {resp.Message}");
        });
    }
}