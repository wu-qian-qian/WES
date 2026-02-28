using System.Reflection;

namespace Device.Application;

public class CreatDBEntity
{
    private static readonly Dictionary<string, PropertyInfo[]?> _properMap;

    static CreatDBEntity()
    {
        _properMap = new Dictionary<string, PropertyInfo[]?>();
    }
    public static T CreatEntity<T>(KeyValuePair<string,string>[] plcBuffers) where T : IDBEntity, new()
    {
        var type = typeof(T);
        var t = new T();
        PropertyInfo[]? propers = default;
        _properMap.TryGetValue(type.Name, out propers);
        //双重校验
        if (propers == null)
        {
            throw new ArgumentNullException($"{type.Name} 没有被初始化加载到缓存队列");
        }
        for (var i = 0; i < plcBuffers.Length; i++)
        {
            var propertyInfo = propers.First(p => p.Name.ToLower() == plcBuffers[i].Key.ToLower());
            propertyInfo.SetValue(t, plcBuffers[i].Value);
        }

        return t;
    }
    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    public void AddDbEntity<T>() where T : IDBEntity
    {
        _semaphoreSlim.Wait();
        try
        {
            var ty = typeof(T);
            if (_properMap.ContainsKey(ty.Name))
                return;
            _properMap[ty.Name]=ty.GetProperties();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _semaphoreSlim.Release(); 
        }
    }
}