namespace Recorders.Push;

class EnvironmentData
{
    private const string ApiUrlEnv = "API_URL";
    public static string ApiUrl => Environment.GetEnvironmentVariable(ApiUrlEnv) ?? "http://localhost:7999";
}