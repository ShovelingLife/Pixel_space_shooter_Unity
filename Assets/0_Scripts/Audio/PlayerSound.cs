using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 사운드 관련
public class PlayerSound : MonoBehaviour
{
    [Header("플레이어 사운드 데이터")]
    public PlayerSoundData player_sound_data;


    // 플레이어 레이저 이펙트 재생
    public void PlayLaser() => AudioManager.inst.PlayEffectBgm(player_sound_data.laserSound);

    // 보호막 데미지 받음
    public void PlayShieldReceiveDmg() => AudioManager.inst.PlayEffectBgm(player_sound_data.shieldProtect);

    // 플레이어 미사일 이펙트 재생
    public void PlayPlayerMissile() => AudioManager.inst.PlayEffectBgm(player_sound_data.missileSound);

    // 플레이어 죽음 이펙트 재생
    public void PlayBulletDeath() => AudioManager.inst.PlayEffectBgm(player_sound_data.deathSound);

    // 적 플레이어간 충돌 시 효과음
    public void PlayCollisionDeath() => AudioManager.inst.PlayEffectBgm(player_sound_data.monsterCollisionSound);
}

[System.Serializable]
public class PlayerSoundData
{
    public AudioClip laserSound;
    public AudioClip missileSound;
    public AudioClip shieldProtect;
    public AudioClip deathSound;
    public AudioClip monsterCollisionSound;
}