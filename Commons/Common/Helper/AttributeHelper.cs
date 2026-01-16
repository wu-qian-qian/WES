using System.Diagnostics;
using System.Reflection;

namespace Common.Helper;

public static class AttributeHelper
{
    /// <summary>
    /// 获取当前堆栈调用类型的特性
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetDeclaringTypeAttribute<T>(int index) where T : Attribute
    {
        var stackTrace = new StackTrace();
        var callerFrame = stackTrace.GetFrame(index);
        var callerType = callerFrame?.GetMethod()?.DeclaringType;
        var attr = callerType?.GetCustomAttribute<T>();
        return attr;
    }
}