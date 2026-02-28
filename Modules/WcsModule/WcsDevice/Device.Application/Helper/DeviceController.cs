using Device.Application;

public static class DeviceController
{
    public static Dictionary<string,IController> _deviceController;
    static DeviceController()
    {
        _deviceController=new Dictionary<string, IController>();
    }
    
    
}