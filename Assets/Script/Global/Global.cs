using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

public static class NullCheck
{
    public static bool Invoke<T>(T obj) where T : class
    {
        if (obj == null)
        {
            LogNull<T>();
            return false;
        }

        return true;
    }

    [Conditional("UNITY_EDITOR")]
    private static void LogNull<T>()
    {
        UnityEngine.Debug.LogError($"[NullCheck] {typeof(T).Name} is null.");
    }

}

public static class GlobalEnum
{
    public static string ToDescription(this Enum source)
    {
        FieldInfo info = source.GetType().GetField(source.ToString());

        var attribute = (DescriptionAttribute)info.GetCustomAttribute(typeof(DescriptionAttribute), false);

        if(attribute != null)
        {
            return attribute.Description;
        }
        else
        {
            return source.ToString();
        }
    }
}