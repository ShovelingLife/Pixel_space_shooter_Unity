using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 총알 관련
[CreateAssetMenu(fileName = "Enemy_bullet_scriptable_data", menuName = "Create_scriptable_bullet_data/Enemy_bullet_data", order = 0)]
public class Enemy_bullet_data : Global_bullet_data
{
    public int bullet_dmg = 1;
    public float bullet_timer = 0f;
    public float bullet_reload_time = 0.6f;
}