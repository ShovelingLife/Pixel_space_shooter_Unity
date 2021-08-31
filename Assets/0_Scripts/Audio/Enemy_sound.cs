using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 사운드 상속 클래스
public class Enemy_sound : MonoBehaviour
{
    [Header("적 사운드 데이터")]
    public Enemy_sound_data enemy_sound_data;


    // 적의 스폰 사운드 이펙트 재생
    public void Play_enemy_spawn_sound()
    {
        Audio_manager.instance.Play_effect_bgm(enemy_sound_data.spawn_sound);
    }

    // 적의 작은 레이저 이펙트 재생
    public void Play_enemy_small_laser_sound()
    {
        Audio_manager.instance.Play_effect_bgm(enemy_sound_data.small_laser_sound);
    }

    // 적의 중간 크기의 레이저 이펙트 재생
    public void Play_enemy_medium_laser_sound()
    {
        Audio_manager.instance.Play_effect_bgm(enemy_sound_data.medium_laser_sound);
    }

    // 적의 큰 레이저 이펙트 재생
    public void Play_enemy_big_laser_sound()
    {
        Audio_manager.instance.Play_effect_bgm(enemy_sound_data.big_laser_sound);
    }

    // 적 비행기 죽음 이펙트 재생
    public void Play_enemy_death_sound()
    {
        Audio_manager.instance.Play_effect_bgm(enemy_sound_data.death_sound);
    }
}

[System.Serializable]
public class Enemy_sound_data
{
    public AudioClip spawn_sound;
    public AudioClip small_laser_sound;
    public AudioClip medium_laser_sound;
    public AudioClip big_laser_sound;
    public AudioClip death_sound;
}