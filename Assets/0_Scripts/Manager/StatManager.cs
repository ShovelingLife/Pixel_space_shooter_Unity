using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : SingletonLocal<StatManager>
{
    [Header("플레이어 스텟 관련")]
    public PlayerStatData playerStatData;

    [Header("소형 적 스텟 관련")]
    public LowEnemyStatData enemyStatData;

    [Header("적 발사체 스텟 관련")]
    public EnemyBulletData enemyBulletStatData;

    [Header("플레이어 파워업 관련")]
    public PlayerPowerUpStat playerPowerUpStat;
}

[System.Serializable]
public class PlayerPowerUpStat
{
    [Header("플레이어 총알 증가 관련")]
    public BulletPowerUpData bulletPowerUpData;
    public Transform         secondBulletTrans;
    public Transform         thirdBulletTrans;
    public int               powerUpLvl = 0;
    public float             curBulletPowerUpTime;

    [Header("플레이어 총알 속도 관련")]
    public BulletSpeedUpData bulletSpeedUpData;
    public int               speedUpLvl;
    public bool              isBoosterOn;
    public float             curBulletSpeedUpTime;

    [Header("플레이어 미사일 관련")]
    public MissilePowerUpData missilePowerUpData;
    public Transform          missileFirstLvlTrans;
    public Transform          missileSecondLvlTrans;
    public int                missileLvl = 0;
    public float              missileCurTime = 0f;
    public float              missileReloadTime = 2f; //2f
    public float              curMissilePowerUpTime;

    [Header("플레이어 보호막 관련")]
    public ShieldPowerUpData shieldPowerUpData;
    public GameObject        shieldObj;
    public int               shieldLvl;
    public bool              isShieldCreated = false;
    public float             curShieldPowerUpTime;


    // 총알 파워업 시간 설정
    public void SetBulletPowerUpTime() => curBulletPowerUpTime = bulletPowerUpData.time;

    // 총알 스피드업 시간 설정
    public void SetBulletSpeedUpTime() => curBulletSpeedUpTime = bulletSpeedUpData.time;

    // 미사일 시간 설정
    public void SetMissilePowerUpTime() => curMissilePowerUpTime = missilePowerUpData.time;

    // 보호막 시간 설정
    public void SetShieldPowerUpTime() => curShieldPowerUpTime = shieldPowerUpData.time;
}