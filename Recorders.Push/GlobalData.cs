// ReSharper disable InconsistentNaming
namespace Recorders.Push;

static class GlobalData
{
    public static void Init()
    {
        ApiOnline = true;
    }
    
    private static readonly Shared<bool> _apiOnline = new();

    public static bool ApiOnline
    {
        get => _apiOnline.Get();
        set => _apiOnline.Set(value);
    }
}