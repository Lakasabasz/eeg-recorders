namespace Recorders.Push;

class Shared<T>
{
    private readonly object lockObject = new();
    private T? _obj;

    public void Set(T obj)
    {
        lock (lockObject)
        {
            _obj = obj;
        }
    }

    public T Get()
    {
        lock (lockObject)
        {
            return _obj ?? throw new NullReferenceException();
        }
    }
}