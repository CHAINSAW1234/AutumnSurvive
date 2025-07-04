using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliderController : MonoBehaviour
{
    Slider[] sliders;

    private void Awake()
    {
        sliders = GetComponentsInChildren<Slider>();
        
        if(sliders == null)
        {
            Debug.Log("Sliders are Null");
        }

        ResetSliders();

    }
    private void OnEnable()
    {
        ResetSliders();
    }

    public void UpdateSounds()
    {
        foreach (Defines.Sound sound in Enum.GetValues(typeof(Defines.Sound)))
        {
            PlayerDataController.Instance.SetSoundAt(sound, sliders[(int)sound].value);
        }
    }

    public void ResetSliders()
    {
        foreach (Defines.Sound sound in Enum.GetValues(typeof(Defines.Sound)))
        {
            sliders[(int)sound].value = PlayerDataController.Instance.Sounds[(int)sound];
        }
    }
}
