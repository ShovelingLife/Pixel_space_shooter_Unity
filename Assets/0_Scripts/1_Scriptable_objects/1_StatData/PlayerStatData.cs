using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 스탯
[CreateAssetMenu(fileName = "PlayerStatScriptableData", menuName = "CreateScriptableStatData/PlayerStatData", order = 1)]
public class PlayerStatData : GlobalStatData
{
    // 플레이어 HP 관련
    public float maxHp         = 100;
    public int   maxPowerUpLvl = 3;
    public int   maxMissileLvl = 2;
}