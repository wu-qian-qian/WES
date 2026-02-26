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
    /// <summary>
    /// 创建
    /// </summary>
    [Description("已创建")]
    Craeted,
    /// <summary>
    /// 待执行
    /// </summary>
    [Description("待执行")]
    Pending,
    /// <summary>
    /// 执行中
    /// </summary>
    [Description("执行中")]
    InProgress,
    /// <summary>
    /// 已完成
    /// </summary>
    [Description("已完成")]
    Completed,
    /// <summary>
    /// 已取消
    /// </summary>
    [Description("已取消")]
    Canceled
}
/// <summary>
/// 任务系统类型枚举
/// </summary>
public  enum TaskSystemTypeEnum
{
        /// <summary>
        /// Wcs系统
        /// </summary>
        [Description("Wcs系统")]
    WCS,
    /// <summary>
    /// WMS系统
    /// </summary>
    [Description("WMS系统")]
    WMS,
}