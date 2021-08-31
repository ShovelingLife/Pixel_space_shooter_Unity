using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 미사일 아이템 데이터
[CreateAssetMenu(fileName = "Player_missile_power_up_scriptable_data", menuName = "Create_scriptable_power_up_data/Missile_power_up_data", order = 2)]
public class Missile_power_up_data : Global_power_up_data
{
    public float missile_dmg;
    public float missile_speed = 3f;
    // IDLE 관련
    public float idle_speed = 0.025f;
    public float idle_distance = 1f;
}