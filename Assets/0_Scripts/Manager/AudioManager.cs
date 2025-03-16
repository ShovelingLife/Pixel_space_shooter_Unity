using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonGlobal<AudioManager>
{
    // BGM 관련
    public AudioSource  mainBgmSrc;
    public AudioSource  secondBgmSrc;

    // 이펙트 관련
    public AudioSource  effectSrc;
    public PlayerSound  playerSound;
    public EnemySound   enemySound;
    public PowerUpSound powerUpsound;
    

    private void Start()
    {
        playerSound  = GetComponent<PlayerSound>();
        enemySound   = GetComponent<EnemySound>();
        powerUpsound = GetComponent<PowerUpSound>();
    }

    // BGM 재생
    public void PlayBackgroundSound(AudioClip _clip)
    {
        mainBgmSrc.clip = _clip;
        mainBgmSrc.Play();
    }

    // 효과음 재생
    public void PlayEffectBgm(AudioClip _audio_clip) => effectSrc.PlayOneShot(_audio_clip);
}