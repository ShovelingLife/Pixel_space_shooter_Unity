using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 공격력 증가 아이템 데이터
[CreateAssetMenu(fileName = "PlayerBulletPowerUpScriptableData", menuName = "CreateScriptablePowerUpData/BulletPowerUpData", order = 2)]
public class BulletPowerUpData : GlobalPowerUpData
{
    public int increaseDmg = 2;
}