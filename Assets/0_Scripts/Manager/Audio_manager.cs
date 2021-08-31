using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_manager : Singleton_global<Audio_manager>
{
    // BGM 관련
    public AudioSource main_bgm_sound_source;
    public AudioSource second_bgm_sound_source;

    // 이펙트 관련
    public AudioSource    effect_sound_source;
    public Player_sound   player_sound;
    public Enemy_sound    enemy_sound;
    public Power_up_sound power_up_sound;
    

    private void Start()
    {
        player_sound = GetComponent<Player_sound>();
        enemy_sound = GetComponent<Enemy_sound>();
        power_up_sound = GetComponent<Power_up_sound>();
    }

    // BGM 재생
    public void Play_background_sound(AudioClip _audio_clip)
    {
        main_bgm_sound_source.clip = _audio_clip;
        main_bgm_sound_source.Play();
    }

    // 효과음 재생
    public void Play_effect_bgm(AudioClip _audio_clip)
    {
        effect_sound_source.PlayOneShot(_audio_clip);
    }
}