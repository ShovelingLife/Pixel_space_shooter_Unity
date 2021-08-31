using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 보호막 아이템 데이터
[CreateAssetMenu(fileName = "Player_shield_power_up_scriptable_data", menuName = "Create_scriptable_power_up_data/Shield_power_up_data", order = 2)]
public class Shield_power_up_data : Global_power_up_data
{
    public float protection_value = 1f;
}