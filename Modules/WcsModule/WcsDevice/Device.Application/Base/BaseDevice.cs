using Common.JsonExtension;
using System.Linq.Expressions;


namespace Device.Application;

/// <summary>
///     设备数据结构
/// </summary>
/// <typeparam name="TConfig"></typeparam>
/// <typeparam name="TDBEntity"></typeparam>
public abstract class BaseDevice<TConfig, TDBEntity,TWcsTask> : IDevice<TConfig>
    where TConfig : BaseDeviceConfig where TDBEntity : BaseDBEntity, new()
{
    protected BaseDevice(bool enable)
    {
        Enable = enable;
        DBEntity = new TDBEntity();
    }

    /// <summary>
    ///     信号量
    /// </summary>
    public abstract TDBEntity DBEntity { get; protected set; }
    public abstract TWcsTask WcsTask { get; protected set; }
    /// <summary>
    ///     是否启动
    /// </summary>
    public bool Enable { get; protected set; }

    /// <summary>
    ///     设备名
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     设备的独立配置文件
    /// </summary>
    public abstract TConfig Config { get; protected set; }

    /// <summary>
    ///     是否可以执行新任务
    /// </summary>
    /// <returns></returns>
    public  bool IsNewStart()
    {
        return Enable&&WcsTask==null;
    }

    /// <summary>
    ///     设置配置项
    /// </summary>
    /// <param name="config"></param>
    public virtual void SetConfig(string config)
    {
        Config = config.ParseJson<TConfig>();
    }

    public void SetWcsTask(TWcsTask task)
    {
        WcsTask = task;
    }
    public void Clear()
    {
        WcsTask = default(TWcsTask);
    }
    /// <summary>
    ///     创建出键值对
    /// </summary>
    /// <param name="propertyExpression"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public string CreatWriteExpression(Expression<Func<TDBEntity, string>> propertyExpression)
    {
        var key = GetPropertyName(propertyExpression);
        return key;
    }

    private static string GetPropertyName<T>(Expression<Func<T, string>> propertyExpression)
    {
        // 解析表达式，获取属性访问节点
        MemberExpression memberExpr;

        // 处理值类型的装箱情况（如果属性是值类型，表达式会被包装为Convert）
        if (propertyExpression.Body is UnaryExpression unaryExpr &&
            unaryExpr.Operand is MemberExpression)
            memberExpr = (MemberExpression)unaryExpr.Operand;
        // 直接是属性访问表达式
        else if (propertyExpression.Body is MemberExpression)
            memberExpr = (MemberExpression)propertyExpression.Body;
        else
            throw new ArgumentException("表达式必须是属性访问表达式", nameof(propertyExpression));

        // 返回属性名称
        return memberExpr.Member.Name;
    }


    #region 抽象的实现
    public virtual void SetEnable(bool enable)
    {
        if (Enable != enable) Enable = enable;
    }

    /// <summary>
    ///     设置DB实体
    /// </summary>
    /// <param name="dBEntity"></param>
    public void SetDBEntiry(BaseDBEntity dBEntity)
    {
        DBEntity = (TDBEntity)dBEntity;
    }

    #endregion
}