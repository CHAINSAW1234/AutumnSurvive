using System;
using System.Reflection;
using System.ComponentModel;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public static class Utils
{
    public static bool NullCheck<T>(T obj) where T : class
    {
        if (obj == null)
        {
            LogNull<T>();
            return true;
        }

        return false;
    }

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

    public static Vector2 GetRandomDirection(Vector2 direction, float minDegree, float maxDegree)
    {
        float degree = UnityEngine.Random.Range(minDegree, maxDegree);

        degree *= UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;

        Quaternion rotation = Quaternion.AngleAxis(degree, Vector3.forward);
        Vector2 rotated = rotation * direction.normalized;
        return rotated;
    }

    public static List<T> GetRandomUnique<T>(T[] array, int count)
    {
        List<T> list = new List<T>(array);

        // Fisher-Yates 셔플
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        // N개 선택
        int selectNum = Mathf.Min(count, list.Count);
        return list.GetRange(0, selectNum);
    }


    [Conditional("UNITY_EDITOR")]
    private static void LogNull<T>()
    {
        UnityEngine.Debug.LogError($"[NullCheck] {typeof(T).Name} is null.");
    }
}
