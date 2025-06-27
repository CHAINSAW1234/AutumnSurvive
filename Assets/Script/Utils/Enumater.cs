using System;
using System.Collections;
using UnityEngine;

class IEnumerators
{
    public static IEnumerator WaitForDurationAndAction(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
        yield break;
    }

}

