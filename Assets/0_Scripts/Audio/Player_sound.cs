using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 사운드 관련
public class Player_sound : MonoBehaviour
{
    [Header("플레이어 사운드 데이터")]
    public Player_sound_data player_sound_data;


    // 플레이어 레이저 이펙트 재생
    public void Play_player_laser_sound()
    {
        Audio_manager.instance.Play_effect_bgm(player_sound_data.laser_sound);
    }

    // 보호막 데미지 받음
    public void Play_shield_receive_dmg_sound()
    {
        Audio_manager.instance.Play_effect_bgm(player_sound_data.shield_protect);
    }

    // 플레이어 미사일 이펙트 재생
    public void Play_player_missile_sound()
    {
        Audio_manager.instance.Play_effect_bgm(player_sound_data.missile_sound);
    }

    // 플레이어 죽음 이펙트 재생
    public void Play_bullet_death_sound()
    {
        Audio_manager.instance.Play_effect_bgm(player_sound_data.death_sound);
    }
    
    // 적 플레이어간 충돌 시 효과음
    public void Play_collision_death_sound()
    {
        Audio_manager.instance.Play_effect_bgm(player_sound_data.monster_collision_sound);
    }
}

[System.Serializable]
public class Player_sound_data
{
    public AudioClip laser_sound;
    public AudioClip missile_sound;
    public AudioClip shield_protect;
    public AudioClip death_sound;
    public AudioClip monster_collision_sound;
}