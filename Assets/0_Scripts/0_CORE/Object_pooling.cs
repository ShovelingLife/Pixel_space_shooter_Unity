using System;
using System.Collections.Generic;
using UnityEngine;

public class Object_pooling : Singleton_local<Object_pooling>
{
    // 플레이어 파워업 아이템 관련
    [Header("플레이어 파워업 아이템")]
    public Player_power_up_pooling      player_power_up_pooling;

    // 플레이어 총알 관련
    [Header("플레이어 무기")]
    public Player_bullet_pooling        player_bullet_pooling;

    // 적 총알 관련
    [Header("적 총알")]
    public Enemy_bullet_pooling         enemy_bullet_pooling;

    // 적 관련 
    [Header("적")]
    public Enemy_pooling                enemy_pooling;

    // 운석 관련
    [Header("운석")]
    public Meteorite_pooling            meteorite_pooling;


    void Start()
    {
        Init_class_members();
        Init_obj_pooling();
    }

    // 클래스 변수 초기화
    void Init_class_members()
    {
        player_power_up_pooling = GetComponent<Player_power_up_pooling>();
        player_bullet_pooling   = GetComponent<Player_bullet_pooling>();
        enemy_bullet_pooling    = GetComponent<Enemy_bullet_pooling>();
        enemy_pooling           = GetComponent<Enemy_pooling>();
        meteorite_pooling       = GetComponent<Meteorite_pooling>();
    }

    // 풀링 오브젝트 초기화
    void Init_obj_pooling()
    {
        player_power_up_pooling.Init_player_power_up();
        player_bullet_pooling.Init_player_bullet();
        enemy_bullet_pooling.Init_enemy_bullet();
        enemy_pooling.Init_first_normal_enemy_settings();
        meteorite_pooling.Init_meteorite();
    }
}