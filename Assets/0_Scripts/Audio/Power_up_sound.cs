using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up_sound : MonoBehaviour
{
    [Header("파워업 아이템 사운드 데이터")]
    public Power_up_sound_data power_up_sound_data;


    // 체력 아이템 획득
    public void Play_get_health_item_sound()
    {
        Audio_manager.instance.Play_effect_bgm(power_up_sound_data.health_pick_up_sound);
    }

    // 공격력 증가 아이템 획득
    public void Play_get_power_up_item_sound()
    {
        Audio_manager.instance.Play_effect_bgm(power_up_sound_data.power_up_pick_up_sound);
    }

    // 공격 속도 증가 아이템 획득
    public void Play_get_speed_up_item_sound()
    {
        Audio_manager.instance.Play_effect_bgm(power_up_sound_data.speed_up_pick_up_sound);
    }

    // 미사일 아이템 획득
    public void Play_get_missile_item_sound()
    {
        Audio_manager.instance.Play_effect_bgm(power_up_sound_data.missile_pick_up_sound);
    }

    // 보호막 아이템 획득
    public void Play_get_shield_item_sound()
    {
        Audio_manager.instance.Play_effect_bgm(power_up_sound_data.shield_pick_up_sound);
    }
}

[System.Serializable]
public class Power_up_sound_data
{
    public AudioClip power_up_pick_up_sound;
    public AudioClip speed_up_pick_up_sound;
    public AudioClip health_pick_up_sound;
    public AudioClip missile_pick_up_sound;
    public AudioClip shield_pick_up_sound;
}