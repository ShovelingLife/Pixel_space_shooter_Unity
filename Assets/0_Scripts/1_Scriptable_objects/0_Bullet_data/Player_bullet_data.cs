using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 총알 데이터
[CreateAssetMenu(fileName = "Player_bullet_scriptable_data", menuName = "Create_scriptable_bullet_data/Player_bullet_data", order = 0)]
public class Player_bullet_data : Global_bullet_data
{
    public int bullet_dmg = 1;
    public float bullet_timer = 0f;
    public float bullet_reload_time = 0.6f;
}