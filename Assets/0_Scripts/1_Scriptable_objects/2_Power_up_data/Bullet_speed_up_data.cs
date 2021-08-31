using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 공격 속도 증가 아이템 데이터
[CreateAssetMenu(fileName = "Player_ Bullet_speed_up_scriptable_data", menuName = "Create_scriptable_power_up_data/Bullet_speed_up_data", order = 2)]
public class Bullet_speed_up_data : Global_power_up_data
{
    public float increase_speed = 1.5f;
}