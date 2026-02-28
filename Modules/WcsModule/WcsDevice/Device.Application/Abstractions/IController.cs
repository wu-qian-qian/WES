namespace Device.Application;
public interface IController<T> : IController where T : class, IDevice
{
    /// <summary>
    ///     设备结构
    /// </summary>
    T[] Devices { get; }    
}

public interface IController
{
    /// <summary>
    ///     逻辑执行
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask ExecuteAsync(CancellationToken token = default);

    IDevice GetDevice(string name);

}