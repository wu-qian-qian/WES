namespace Common.Application.Enums;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
public class DIAttrubite:Attribute
{
    public DIAttrubite(DILifeTimeEnum lifeTime, Type baseType)
    {
        LifeTime = lifeTime;
        BaseType = baseType;
    }

    public DILifeTimeEnum LifeTime { get; set; }

    public Type BaseType { get; set; } 
}