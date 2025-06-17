using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private static int PauseCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        PauseCount = 0;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        ++PauseCount;
        UpdateTimeScale();
    }

    public void Resoume()
    {
        --PauseCount;
        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        if (PauseCount < 0)
            PauseCount = 0;

        Time.timeScale = (PauseCount == 0) ? 1f : 0f;
    }
}
