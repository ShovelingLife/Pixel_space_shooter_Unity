using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최하위 적 스탯
[CreateAssetMenu(fileName = "LowMonsterStatScriptableData", menuName = "CreateScriptableStatData/LowMonsterStatData", order = 1)]
public class LowEnemyStatData : GlobalStatData
{
    public int collisionDmg;
}