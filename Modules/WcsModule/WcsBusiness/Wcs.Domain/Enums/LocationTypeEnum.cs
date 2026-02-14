using System.ComponentModel;

public enum LocationTypeEnum
{
    /// <summary>
    /// 存储位置
    /// </summary>
    Storage = 1,

    

    /// <summary>
    /// 其他位置
    /// </summary>
    Other = 3
}

public enum LocationStatusTypeEnum
{
    [Description("空闲")]
    Idle = 1,
    [Description("占用")]
    Occupied = 2,
    [Description("冻结")]
    Freezen = 3,
}

public enum LocationSizeTypeEnum
{
    Small = 1,
    Medium = 2,
    Large = 3
}