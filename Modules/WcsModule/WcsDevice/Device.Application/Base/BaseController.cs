namespace Device.Application;

public abstract class BaseController<T> : IController<T> where T : class, IDevice
{
    public abstract T[] Devices{get;}

    public abstract ValueTask ExecuteAsync(CancellationToken token = default);

    public IDevice GetDevice(string name)
    {
        return Devices.First(p=>p.Name==name);
    }
}