using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 보호막 아이템 데이터
[CreateAssetMenu(fileName = "PlayerShieldPowerUpScriptableData", menuName = "CreateScriptablePowerUpData/ShieldPowerUpData", order = 2)]
public class ShieldPowerUpData : GlobalPowerUpData
{
    public float protectionVal = 1f;
}