using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 사운드 상속 클래스
public class EnemySound : MonoBehaviour
{
    [Header("적 사운드 데이터")]
    public EnemySoundData enemySoundData;


    // 적의 스폰 사운드 이펙트 재생
    public void PlaySpawn() => AudioManager.inst.PlayEffectBgm(enemySoundData.spawnSound);

    // 적의 작은 레이저 이펙트 재생
    public void PlaySmallLaser() => AudioManager.inst.PlayEffectBgm(enemySoundData.smallLaserSound);

    // 적의 중간 크기의 레이저 이펙트 재생
    public void PlayMediumLaser() => AudioManager.inst.PlayEffectBgm(enemySoundData.mediumLaserSound);

    // 적의 큰 레이저 이펙트 재생
    public void PlayBigLaser() => AudioManager.inst.PlayEffectBgm(enemySoundData.bigLaserSound);

    // 적 비행기 죽음 이펙트 재생
    public void PlayDeath() => AudioManager.inst.PlayEffectBgm(enemySoundData.deathSound);
}

[System.Serializable]
public class EnemySoundData
{
    public AudioClip spawnSound;
    public AudioClip smallLaserSound;
    public AudioClip mediumLaserSound;
    public AudioClip bigLaserSound;
    public AudioClip deathSound;
}