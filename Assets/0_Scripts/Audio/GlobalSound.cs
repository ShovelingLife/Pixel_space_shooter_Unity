using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public GlobalSoundData data;

    void Start()
    {
        AudioManager.inst.PlayBackgroundSound(data.firstBgm);
    }
}

[System.Serializable]
public class GlobalSoundData
{
    public AudioClip firstBgm;
}