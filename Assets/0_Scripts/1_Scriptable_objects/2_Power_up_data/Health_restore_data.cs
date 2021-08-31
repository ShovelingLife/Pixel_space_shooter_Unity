using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 체력 회복 아이템 데이터
[CreateAssetMenu(fileName = "Player_health_restore_scriptable_data", menuName = "Create_scriptable_power_up_data/Health_restore_data", order = 2)]
public class Health_restore_data : Global_power_up_data
{
    public float restore_health = 2.5f;
}