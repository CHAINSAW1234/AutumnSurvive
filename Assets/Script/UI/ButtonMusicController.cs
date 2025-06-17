using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusicController : MonoBehaviour
{
    public void SetSoundMute(bool isMute)
    {
        Managers.Sound.Mute(isMute);
    }
}
