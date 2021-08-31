using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_sound : MonoBehaviour
{
    public Global_sound_data global_sound_data;

    void Start()
    {
        Audio_manager.instance.Play_background_sound(global_sound_data.first_bgm);
    }
}

[System.Serializable]
public class Global_sound_data
{
    public AudioClip first_bgm;
}