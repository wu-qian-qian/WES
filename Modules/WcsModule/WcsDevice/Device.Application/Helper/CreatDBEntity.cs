using System.Reflection;

namespace Device.Application;

public class CreatDBEntity
{
    private static readonly Dictionary<string, PropertyInfo[]?> _properMap;

    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    static CreatDBEntity()
    {
        _properMap = new Dictionary<string, PropertyInfo[]?>();
    }

}