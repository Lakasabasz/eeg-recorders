using RestSharp;

namespace Recorders.Push;

record ErrorMessage(string Message);

class ApiClient(string url)
{
    public static ApiClient Instance => _instance ??= new ApiClient(EnvironmentData.ApiUrl);

    private readonly RestClient _client = new(url);
    private static ApiClient? _instance;

    private ErrorMessage? Execute(RestRequest request)
    {
        var response = _client.Execute<ErrorMessage?>(request);
        return response.StatusCode == 0 ? new ErrorMessage("API unavailable") : response.Data;
    }

    public bool HealthCheck()
    {
        return Execute(new RestRequest("/", Method.Get)) is null;
    }

    public ErrorMessage? SetLabel(string labelName)
    {
        var request = new RestRequest("/currentlabel", Method.Patch);
        request.AddJsonBody(new { label = labelName });
        return Execute(request);
    }

    public ErrorMessage? StartRecording()
    {
        var request = new RestRequest("/start", Method.Patch);
        return Execute(request);
    }

    public ErrorMessage? StopRecording()
    {
        var request = new RestRequest("/stop", Method.Patch);
        return Execute(request);
    }
}