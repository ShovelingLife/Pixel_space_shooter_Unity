using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSound : MonoBehaviour
{
    [Header("파워업 아이템 사운드 데이터")]
    public PowerUpSoundData data;


    // 체력 아이템 획득
    public void PlayGetHealthItem() => AudioManager.inst.PlayEffectBgm(data.healthPickUp);

    // 공격력 증가 아이템 획득
    public void PlayGetPowerUpItem() => AudioManager.inst.PlayEffectBgm(data.powerUpPickUp);

    // 공격 속도 증가 아이템 획득
    public void PlayGetSpeedUpItem() => AudioManager.inst.PlayEffectBgm(data.speedUpPickUp);

    // 미사일 아이템 획득
    public void PlayGetMissileItemSound() => AudioManager.inst.PlayEffectBgm(data.missilePickUp);

    // 보호막 아이템 획득
    public void PlayGetShieldItemSound() => AudioManager.inst.PlayEffectBgm(data.shieldPickUp);
}

[System.Serializable]
public class PowerUpSoundData
{
    public AudioClip powerUpPickUp;
    public AudioClip speedUpPickUp;
    public AudioClip healthPickUp;
    public AudioClip missilePickUp;
    public AudioClip shieldPickUp;
}