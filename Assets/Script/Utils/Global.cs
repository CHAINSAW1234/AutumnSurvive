using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
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

public static class GenerateRandom
{
    public static Vector2 GenerateRandomDirection(Vector2 direction, float minDegree, float maxDegree)
    {
        float degree = UnityEngine.Random.Range(minDegree, maxDegree);

        degree *= UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;

        Quaternion rotation = Quaternion.AngleAxis(degree, Vector3.forward);
        Vector2 rotated = rotation * direction.normalized;
        return rotated;
    }
}
