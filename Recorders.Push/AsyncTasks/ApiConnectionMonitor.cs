using System.Diagnostics;

namespace Recorders.Push.AsyncTasks;


record MonitorTaskDto(Task Task, CancellationTokenSource Cancel);
class ApiConnectionMonitor
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly CancellationToken _cancellationToken;
    private readonly ApiClient _client;

    public ApiConnectionMonitor()
    {
        _cancellationToken = _cancellationTokenSource.Token;
        _client = ApiClient.Instance;
    }
    
    public MonitorTaskDto Begin()
    {
        return new MonitorTaskDto(new Task(Invoke), _cancellationTokenSource);
    }

    private void Invoke()
    {
        while (!_cancellationToken.IsCancellationRequested)
        {
            Stopwatch sw = new Stopwatch();
            GlobalData.ApiOnline = _client.HealthCheck();
            var sleep = TimeSpan.FromSeconds(2) - sw.Elapsed;
            if(sleep > TimeSpan.Zero) Thread.Sleep(sleep);
        }
    }
}