using System.ComponentModel;

public enum TaskTypeEnum
{
    [Description("入库")]
    In,
    [Description("出库")]
    Out,
    [Description("移库")]
    Move,
}
public enum TaskStatusTypeEnum
{
    [Description("已创建")]
    Craeted,
    [Description("待执行")]
    Pending,
    [Description("执行中")]
    InProgress,
    [Description("已完成")]
    Completed,
    [Description("已取消")]
    Canceled
}
public enum DeviceTaskTypeEnum
{
   Conveyor,
   Elevator,
   Stacker,
}