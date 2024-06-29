using System.Runtime.CompilerServices;

namespace Recorders.Push;

static class Tools
{
    public static T RequireNotNull<T>(object? o, [CallerArgumentExpression(nameof(o))] string caller = "")
    {
        if (o is null) throw new NullReferenceException(caller);
        return (T)o;
    }
}