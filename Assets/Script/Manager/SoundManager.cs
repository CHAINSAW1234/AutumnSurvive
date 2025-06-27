using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] audioSources = new AudioSource[Enum.GetValues(typeof(Defines.Sound)).Length];
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init(Transform transform)
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            root.transform.parent = transform;

            string[] soundNames = System.Enum.GetNames(typeof(Defines.Sound));
            for (int i = 0; i < soundNames.Length; ++i)
            {
                GameObject obj = new GameObject { name = soundNames[i] };
                audioSources[i] = obj.AddComponent<AudioSource>();
                obj.transform.parent = root.transform;
            }
        }

        audioSources[(int)Defines.Sound.Bgm].loop = true;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.mute = false;
            audioSource.Stop();
        }

        audioClips.Clear();
    }

    public void Play(string path, Defines.Sound type = Defines.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Defines.Sound type = Defines.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        AudioSource audioSource = audioSources[(int)type];
        audioSource.volume = PlayerDataController.Instance.Sounds[(int)type];
        audioSource.pitch = pitch;

        if (type == Defines.Sound.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (type == Defines.Sound.Effect)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Mute(bool isMute)
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMute;
        }
    }

    AudioClip GetOrAddAudioClip(string path)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        AudioClip audioClip = null;
        if (audioClips.TryGetValue(path, out audioClip) == false)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing ! {path}");
            }
            else
            {
                audioClips.Add(path, audioClip);
            }
        }

        return audioClip;
    }

}
