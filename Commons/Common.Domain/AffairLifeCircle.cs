namespace Common.Domain;

/// <summary>
///     事件生命周期
///     A-B 服务
///     其中该事件生命周期在redis中生存
/// </summary>
public struct AffairLifeCircle
{
    public LifeCircleType LifeCircle { get; private set; }
    public string Message { get; private set; }

    public void Write(LifeCircleType lifeCircle, string message)
    {
        LifeCircle = lifeCircle;
        Message = message;
    }

    public enum LifeCircleType
    {
        Creat = 0,
        Send = 1,
        Processed = 2,
        Complate = 3,
        Cancel = 4,
        Fault = 5,
        Retry=6,
    }
}