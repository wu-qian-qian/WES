namespace Device.Application;

public interface IDevice<T> : IDevice where T : BaseDeviceConfig
{
    T Config { get; }
}

public interface IDevice
{
    string Name { get; }

    /// <summary>
    ///     设置DB实体
    /// </summary>
    /// <param name="dBEntity"></param>
    void SetDBEntiry(IDBEntity dBEntity);

    bool IsNewStart();
}