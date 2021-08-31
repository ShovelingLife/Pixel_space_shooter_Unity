using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최하위 적 스탯
[CreateAssetMenu(fileName = "Low_monster_stat_scriptable_data", menuName = "Create_scriptable_stat_data/Low_monster_stat_data", order = 1)]
public class Low_enemy_stat_data : Global_stat_data
{
    public int collision_dmg;
}