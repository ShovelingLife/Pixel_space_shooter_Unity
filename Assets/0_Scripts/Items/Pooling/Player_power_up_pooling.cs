using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_power_up_pooling : MonoBehaviour
{
    public Player_power_up_pooling_data player_power_up_pooling_data;
    Vector3[]      m_player_power_up_init_pos_arr;
    readonly float pos_y = 25.3f;

    public Vector3 this[int _index]
    {
        get { return m_player_power_up_init_pos_arr[_index]; }
    }


    private void Start()
    {
        Init_values();
    }

    // Initialize values
    void Init_values()
    {
        // Position array initialization
        m_player_power_up_init_pos_arr = new Vector3[Global.power_up_item_array_index];
        float pos_x = -9.75f;
        
        for (int i = 0; i < Global.power_up_item_array_index; i++)
        {
            m_player_power_up_init_pos_arr[i].x = pos_x;
            m_player_power_up_init_pos_arr[i].y = pos_y;
            m_player_power_up_init_pos_arr[i].z = 3f;
            pos_x += 0.75f;
        }
    }

    // 플레이어 파워업 리스트 초기화
    public void Init_player_power_up()
    {
        for (int i = 0; i < player_power_up_pooling_data.max_power_up_count; i++)
        {
            // 체력 회복 아이템
            GameObject health_obj = Instantiate(player_power_up_pooling_data.health_prefab, player_power_up_pooling_data.health_container);
            health_obj.SetActive(false);
            player_power_up_pooling_data.health_obj_list.Add(health_obj);

            // 공격력 강화 아이템
            GameObject bullet_power_up_obj = Instantiate(player_power_up_pooling_data.bullet_power_up_prefab, player_power_up_pooling_data.bullet_power_up_container);
            bullet_power_up_obj.SetActive(false);
            player_power_up_pooling_data.bullet_power_up_obj_list.Add(bullet_power_up_obj);

            // 공격 속도 증가 아이템
            GameObject bullet_speed_up_obj = Instantiate(player_power_up_pooling_data.bullet_speed_up_prefab, player_power_up_pooling_data.bullet_speed_up_container);
            bullet_speed_up_obj.SetActive(false);
            player_power_up_pooling_data.bullet_speed_up_obj_list.Add(bullet_speed_up_obj);

            // 미사일 아이템
            GameObject missile_obj = Instantiate(player_power_up_pooling_data.missile_prefab, player_power_up_pooling_data.missile_container);
            missile_obj.SetActive(false);
            player_power_up_pooling_data.missile_obj_list.Add(missile_obj);

            // 보호막 아이템
            GameObject shield_obj = Instantiate(player_power_up_pooling_data.shield_prefab, player_power_up_pooling_data.shield_container);
            shield_obj.SetActive(false);
            player_power_up_pooling_data.shield_obj_list.Add(shield_obj);
        }
    }

    // Get player power up randomly
    public GameObject Get_player_power_up_item()
    {
        foreach (var item in player_power_up_pooling_data.Get_random_list())
        {
            if (!item.activeInHierarchy)
                return item;
        }
        return null;
    }
}

[System.Serializable]
public class Player_power_up_pooling_data
{
    readonly int max_power_up_item_count = 5;
    public int max_power_up_count = 100;

    [Header("플레이어 탄 공격력 증가 아이템")]
    public List<GameObject> bullet_power_up_obj_list = new List<GameObject>();
    public GameObject       bullet_power_up_prefab;
    public Transform        bullet_power_up_container;

    [Header("플레이어 탄 속도 증가 아이템")]
    public List<GameObject> bullet_speed_up_obj_list = new List<GameObject>();
    public GameObject       bullet_speed_up_prefab;
    public Transform        bullet_speed_up_container;

    [Header("플레이어 체력 회복 아이템")]
    public List<GameObject> health_obj_list = new List<GameObject>();
    public GameObject       health_prefab;
    public Transform        health_container;

    [Header("플레이어 미사일 아이템")]
    public List<GameObject> missile_obj_list = new List<GameObject>();
    public GameObject       missile_prefab;
    public Transform        missile_container;

    [Header("플레이어 보호막 아이템")]
    public List<GameObject> shield_obj_list = new List<GameObject>();
    public GameObject       shield_prefab;
    public Transform        shield_container;


    // Returns random obj list
    public List<GameObject> Get_random_list()
    {
        List<GameObject> tmp_obj_list = new List<GameObject>();
        int rand_value = Global.Rand(0, max_power_up_item_count);

        switch (rand_value)
        {
            // Heal item
            case 0: tmp_obj_list = health_obj_list; break;

            // Bullet power up item
            case 1: tmp_obj_list = bullet_power_up_obj_list; break;

            // Bullet speed up item
            case 2: tmp_obj_list = bullet_speed_up_obj_list; break;

            // Missile item
            case 3: tmp_obj_list = missile_obj_list; break;

            // Shield item
            case 4: tmp_obj_list = shield_obj_list; break;
        }
        return tmp_obj_list;
    }
}