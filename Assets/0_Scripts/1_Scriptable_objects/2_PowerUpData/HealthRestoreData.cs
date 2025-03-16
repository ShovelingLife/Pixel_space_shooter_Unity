using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 체력 회복 아이템 데이터
[CreateAssetMenu(fileName = "PlayerHealthRestoreScriptableData", menuName = "CreateScriptablePowerUpData/HealthRestoreData", order = 2)]
public class HealthRestoreData : GlobalPowerUpData
{
    public float restoreHealth = 2.5f;
}