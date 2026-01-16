namespace Common.Application.FSM;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class FsmAttribute : Attribute
{
    public FsmAttribute(string keyName)
    {
        KeyName = keyName;
    }

    public string KeyName { get; set; }
}