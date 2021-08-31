using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 스탯
[CreateAssetMenu(fileName = "Player_stat_scriptable_data", menuName = "Create_scriptable_stat_data/Player_stat_data", order = 1)]
public class Player_stat_data : Global_stat_data
{
    // 플레이어 HP 관련
    public float max_hp = 100;
    public int max_power_up_level = 3;
    public int max_missile_level = 2;
}