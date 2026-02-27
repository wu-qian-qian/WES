namespace Device.Application;
/// <summary>
///     控制中心的公共操作抽象
///     对ICommonController的一些公共接口
/// </summary>
/// <typeparam name="TDeviceStructure"></typeparam>
public abstract class BaseCommonController<TDeviceStructure> where TDeviceStructure : class, IDevice
{
    public TDeviceStructure[] Devices { get; protected set; }
}