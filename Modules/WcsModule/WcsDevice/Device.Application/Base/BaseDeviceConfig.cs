namespace Device.Application;

public abstract class BaseDeviceConfig
{
    
    /// <summary>
    ///     缓存使用    唯一标识
    /// </summary>
    public string Key { get; protected set; }



    /// <summary>
    ///     用于任务获取的Key
    /// </summary>
    public string TaskKey { get; protected set; }

    public void InitKeys()
    {
        
    }
}