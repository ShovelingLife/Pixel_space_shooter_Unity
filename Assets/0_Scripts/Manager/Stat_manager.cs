using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat_manager : Singleton_local<Stat_manager>
{
    [Header("플레이어 스텟 관련")]
    public Player_stat_data     player_stat_data;

    [Header("소형 적 스텟 관련")]
    public Low_enemy_stat_data  enemy_stat_data;

    [Header("적 발사체 스텟 관련")]
    public Enemy_bullet_data    enemy_bullet_stat_data;

    [Header("플레이어 파워업 관련")]
    public Player_power_up_stat player_power_up_stat;
}

[System.Serializable]
public class Player_power_up_stat
{
    [Header("플레이어 총알 증가 관련")]
    public Bullet_power_up_data bullet_power_up_data;
    public Transform second_bullet_trans;
    public Transform third_bullet_trans;
    public int power_up_level = 0;
    public float current_bullet_power_up_time;

    [Header("플레이어 총알 속도 관련")]
    public Bullet_speed_up_data bullet_speed_up_data;
    public int speed_up_level;
    public bool is_booster_on;
    public float current_bullet_speed_up_time;

    [Header("플레이어 미사일 관련")]
    public Missile_power_up_data missile_power_up_data;
    public Transform missile_first_level_trans;
    public Transform missile_second_level_trans;
    public int missile_level = 0;
    public float missile_current_time = 0f;
    public float missile_reload_time = 2f; //2f
    public float current_missile_power_up_time;

    [Header("플레이어 보호막 관련")]
    public Shield_power_up_data shield_power_up_data;
    public GameObject shield_obj;
    public int shield_level;
    public bool is_shield_created = false;
    public float current_shield_power_up_time;


    // 총알 파워업 시간 설정
    public void Set_bullet_power_up_time()
    {
        current_bullet_power_up_time = bullet_power_up_data.power_up_time;
    }

    // 총알 스피드업 시간 설정
    public void Set_bullet_speed_up_time()
    {
        current_bullet_speed_up_time = bullet_speed_up_data.power_up_time;
    }

    // 미사일 시간 설정
    public void Set_missile_power_up_time()
    {
        current_missile_power_up_time = missile_power_up_data.power_up_time;
    }

    // 보호막 시간 설정
    public void Set_shield_power_up_time()
    {
        current_shield_power_up_time = shield_power_up_data.power_up_time;
    }
}