using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 공격력 증가 아이템 데이터
[CreateAssetMenu(fileName = "Player_ Bullet_power_up_scriptable_data", menuName = "Create_scriptable_power_up_data/Bullet_power_up_data", order = 2)]
public class Bullet_power_up_data : Global_power_up_data
{
    public int increase_dmg = 2;
}