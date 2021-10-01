using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Pooling_manager : Singleton_local<Pooling_manager>
{
    Dictionary<e_pooling_obj_type, List<GameObject>> m_obj_list_dic = new Dictionary<e_pooling_obj_type, List<GameObject>>();

    [Header("Player_pooling_data")]
    public Player_bullet_pooling_data   player_bullet_pooling_data;

    [Header("Player_power_up_pooling_data")]
    public Player_power_up_pooling_data player_power_up_pooling_data;

    [Header("Enemy_bullet_pooling_data")]
    public Enemy_bullet_pooling_data    enemy_bullet_pooling_data;

    [Header("Enemy_pooling_data")]
    public Enemy_pooling_data           enemy_pooling_data;

    [Header("Meteorite_pooling_data")]
    public Meteorite_pooling_data       meteorite_pooling_data;


    void Start()
    {
        Init_class_members();
        Init_obj_pooling();
    }

    // 클래스 변수 초기화
    void Init_class_members()
    {
        for (int i = 0; i < (int)e_pooling_obj_type.MAX; i++)
             m_obj_list_dic[(e_pooling_obj_type)i] = new List<GameObject>();

        player_power_up_pooling_data.Init_values();
    }

    // 풀링 오브젝트 초기화
    void Init_obj_pooling()
    {
        Init_player_bullet_pooling_data();
        Init_player_power_up_pooling_data();
        Init_enemy_bullet_pooling_data();
        Init_enemy_pooling_data();
        Init_meteorite_pooling_data();
    }

    // Player bullet initialization
    void Init_player_bullet_pooling_data()
    {
        // Player_bullet
        for (int i = 0; i < player_bullet_pooling_data.max_bullet_count; i++)
        {
            // Player bullet
            GameObject bullet_obj = GameObject.Instantiate(player_bullet_pooling_data.player_bullet_prefab, player_bullet_pooling_data.player_bullet_container, true);
            bullet_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_BULLET].Add(bullet_obj);

            // Player missile
            GameObject missile_obj = GameObject.Instantiate(player_bullet_pooling_data.player_missile_prefab, player_bullet_pooling_data.player_missile_container, true);
            missile_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_MISSILE].Add(missile_obj);
        }
    }

    // 플레이어 파워업 리스트 초기화
    public void Init_player_power_up_pooling_data()
    {

        for (int i = 0; i < player_power_up_pooling_data.max_power_up_count; i++)
        {
            // 체력 회복 아이템
            GameObject health_obj = Instantiate(player_power_up_pooling_data.health_prefab, player_power_up_pooling_data.health_container);
            health_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_HEALTH].Add(health_obj);

            // 공격력 강화 아이템
            GameObject bullet_power_up_obj = Instantiate(player_power_up_pooling_data.bullet_power_up_prefab, player_power_up_pooling_data.bullet_power_up_container);
            bullet_power_up_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_BULLET_POWER_UP].Add(bullet_power_up_obj);

            // 공격 속도 증가 아이템
            GameObject bullet_speed_up_obj = Instantiate(player_power_up_pooling_data.bullet_speed_up_prefab, player_power_up_pooling_data.bullet_speed_up_container);
            bullet_speed_up_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_BULLET_SPEED_UP].Add(bullet_speed_up_obj);

            // 미사일 아이템
            GameObject missile_obj = Instantiate(player_power_up_pooling_data.missile_prefab, player_power_up_pooling_data.missile_container);
            missile_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_MISSILE_POWER_UP].Add(missile_obj);

            // 보호막 아이템
            GameObject shield_obj = Instantiate(player_power_up_pooling_data.shield_prefab, player_power_up_pooling_data.shield_container);
            shield_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.PLAYER_SHIELD].Add(shield_obj);
        }
    }

    // 적 총알 초기화
    public void Init_enemy_bullet_pooling_data()
    {
        for (int i = 0; i < enemy_bullet_pooling_data.max_enemy_bullet_count; i++)
        {
            // 소형 총알
            GameObject tmp_small_bullet_obj = Instantiate(enemy_bullet_pooling_data.small_enemy_bullet_prefab, enemy_bullet_pooling_data.small_enemy_bullet_container, true);
            tmp_small_bullet_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.ENEMY_SMALL_BULLET].Add(tmp_small_bullet_obj);

            // 중형 총알
            GameObject tmp_medium_bullet_obj = Instantiate(enemy_bullet_pooling_data.medium_enemy_bullet_prefab, enemy_bullet_pooling_data.medium_enemy_bullet_container, true);
            tmp_medium_bullet_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.ENEMY_MEDIUM_BULLET].Add(tmp_medium_bullet_obj);

            // 대형 총알
            GameObject tmp_big_bullet_obj = Instantiate(enemy_bullet_pooling_data.big_enemy_bullet_prefab, enemy_bullet_pooling_data.big_enemy_bullet_container, true);
            tmp_big_bullet_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.ENEMY_BIG_BULLET].Add(tmp_medium_bullet_obj);
        }
    }

    // First enemy pooling initialization
    public void Init_enemy_pooling_data()
    {
        for (int i = 0; i < enemy_pooling_data.max_first_enemy_count; i++)
        {
            GameObject first_enemy_obj = Instantiate(enemy_pooling_data.first_normal_enemy_prefab, enemy_pooling_data.first_normal_enemy_container, true);
            first_enemy_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.ENEMY_GREEN_TYPE_ONE].Add(first_enemy_obj);
        }
    }

    // 운석 초기화
    public void Init_meteorite_pooling_data()
    {
        for (int i = 0; i < meteorite_pooling_data.max_meteorite_count; i++)
        {
            // 작은 크기의 운석
            GameObject small_meteorite_obj = Instantiate(meteorite_pooling_data.small_meteorite_prefab, meteorite_pooling_data.small_meteorite_container);
            small_meteorite_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.SMALL_METEORITE].Add(small_meteorite_obj);

            // 중간 크기의 운석
            GameObject medium_meteorite_obj = Instantiate(meteorite_pooling_data.medium_meteorite_prefab, meteorite_pooling_data.medium_meteorite_container);
            medium_meteorite_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.MEDIUM_METEORITE].Add(medium_meteorite_obj);

            // 큰 크기의 운석
            GameObject big_meteorite_obj = Instantiate(meteorite_pooling_data.big_meteorite_prefab, meteorite_pooling_data.big_meteorite_container);
            big_meteorite_obj.SetActive(false);
            m_obj_list_dic[e_pooling_obj_type.BIG_METEORITE].Add(big_meteorite_obj);
        }
    }

    // Get pooling obj
    public GameObject Get_obj(e_pooling_obj_type _obj_type)
    {
        foreach (var item in m_obj_list_dic[_obj_type])
        {
            if(!item.activeInHierarchy)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Player_bullet_pooling_data // 플레이어 총알 클래스
{
    // 플레이어 총알 관련
    [Header("플레이어 총알")]
    public int max_bullet_count = 100;
    public GameObject player_bullet_prefab;
    public Transform player_bullet_container;

    // 플레이어 미사일
    [Header("플레이어 미사일")]
    public GameObject player_missile_prefab;
    public Transform player_missile_container;
}

[System.Serializable]
public class Player_power_up_pooling_data
{
    readonly int max_power_up_item_count = 5;
    public int   max_power_up_count = 100;

    [Header("플레이어 탄 공격력 증가 아이템")]
    public GameObject bullet_power_up_prefab;
    public Transform  bullet_power_up_container;

    [Header("플레이어 탄 속도 증가 아이템")]
    public GameObject bullet_speed_up_prefab;
    public Transform  bullet_speed_up_container;

    [Header("플레이어 체력 회복 아이템")]
    public GameObject health_prefab;
    public Transform  health_container;

    [Header("플레이어 미사일 아이템")]
    public GameObject missile_prefab;
    public Transform  missile_container;

    [Header("플레이어 보호막 아이템")]
    public GameObject shield_prefab;
    public Transform  shield_container;

    Vector3[] m_player_power_up_init_pos_arr;
    readonly float pos_y = 25.3f;

    public Vector3 this[int _index]
    {
        get { return m_player_power_up_init_pos_arr[_index]; }
    }

    // Initialize values
    public void Init_values()
    {
        // Position array initialization
        m_player_power_up_init_pos_arr = new Vector3[Global.power_up_position_array_index];
        float pos_x = -9.75f;

        for (int i = 0; i < Global.power_up_position_array_index; i++)
        {
            m_player_power_up_init_pos_arr[i].x = pos_x;
            m_player_power_up_init_pos_arr[i].y = pos_y;
            m_player_power_up_init_pos_arr[i].z = 3f;
            pos_x += 0.75f;
        }
    }
}

[System.Serializable]
public class Enemy_bullet_pooling_data // 적 총알 클래스
{
    public int max_enemy_bullet_count = 200;

    // 적 (소형)총알 관련
    [Header("소형 총알")]
    public GameObject small_enemy_bullet_prefab;
    public Transform small_enemy_bullet_container;

    // 적 (중형)총알 관련
    [Header("중형 총알")]
    public GameObject medium_enemy_bullet_prefab;
    public Transform medium_enemy_bullet_container;

    // 적 (대형)총알 관련
    [Header("대형 총알")]
    public GameObject big_enemy_bullet_prefab;
    public Transform big_enemy_bullet_container;
}

[System.Serializable]
public class Enemy_pooling_data // 적 클래스
{
    // 소형 적 비행기 관련
    [Header("소형 적 비행기")]
    public int max_first_enemy_count = 50;
    public GameObject first_normal_enemy_prefab;
    public Transform first_normal_enemy_container;
}

[System.Serializable]
public class Meteorite_pooling_data // 운석 클래스
{
    public int max_meteorite_count = 100;

    // 소형 운석 관련
    [Header("소형 운석")]
    public GameObject small_meteorite_prefab;
    public Transform small_meteorite_container;

    // 중형 운석 관련
    [Header("중형 운석")]
    public GameObject medium_meteorite_prefab;
    public Transform medium_meteorite_container;

    // 대형 운석 관련
    [Header("대형 운석")]
    public GameObject big_meteorite_prefab;
    public Transform big_meteorite_container;
}