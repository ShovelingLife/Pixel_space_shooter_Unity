using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 공격 속도 증가 아이템 데이터
[CreateAssetMenu(fileName = "PlayerBulletSpeedUpScriptableData", menuName = "CreateScriptablePowerUpData/BulletSpeedUpData", order = 2)]
public class BulletSpeedUpData : GlobalPowerUpData
{
    public float increaseSpeed = 1.5f;
}