using System;
using System.Reflection;
using System.ComponentModel;
using UnityEngine;

public static class Utils
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : UnityEngine.Component
    {
        T component = obj.GetComponent<T>();
        if (component == null)
            component = obj.AddComponent<T>();

        return component;
    }

    public static bool IsInCamera(Camera camera, Vector3 worldPosition) // check worldPosition is visible in the camera
    {
        Vector3 screenPoint = camera.WorldToScreenPoint(worldPosition);

        if (screenPoint.x < 0 || screenPoint.x > Screen.width)
        {
            return false;
        }

        if (screenPoint.y < 0 || screenPoint.y > Screen.height)
        {
            return false;
        }

        return true;
    }

    public static string ToDescription(this Enum source) // from enum get description as string if not exists get name as string
    {
        FieldInfo info = source.GetType().GetField(source.ToString());

        var attribute = (DescriptionAttribute)info.GetCustomAttribute(typeof(DescriptionAttribute), false);

        if (attribute != null)
        {
            return attribute.Description;
        }
        else
        {
            return source.ToString();
        }
    }
}
