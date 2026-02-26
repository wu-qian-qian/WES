using Common.Infrastructure.Enums;

namespace Common.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
public class DIAttribute : Attribute
{
    public DIAttribute(DILifeTimeEnum lifeTime, Type baseType)
    {
        LifeTime = lifeTime;
        BaseType = baseType;
    }

    public DILifeTimeEnum LifeTime { get; set; }

    public Type BaseType { get; set; }
}