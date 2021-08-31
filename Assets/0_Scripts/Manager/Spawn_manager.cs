using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item_spawn_info
{
    public int life_up_quantity;
    public int player_power_up_quantity;
    public int player_speed_up_quantity;
    public int player_shield_quantity;
};

[Serializable]
public class Monster_spawn_pos
{
    public List<Vector3> spawn_pos_list;
}

public class Spawn_manager : Singleton_local<Spawn_manager>
{
    // 몬스터 소환 위치 관련
    [Header("몬스터 소환 위치")]
    public Monster_spawn_pos monster_spawn_pos;
           bool              m_is_first_time;
    //int m_test_count = 0;

    public Item_spawn_info item_spawn_info;
    float m_item_spawn_time = 10f;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(IE_spawn_player_power_up_items());
        StartCoroutine(IE_spawn_first_monsters());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A function that spawns player power up items
    IEnumerator IE_spawn_player_power_up_items()
    {
        Player_power_up_pooling player_power_up_pooling = Object_pooling.instance.player_power_up_pooling;
        
        while (true)
        {
            GameObject tmp_obj = player_power_up_pooling.Get_player_power_up_item();
            Vector3    tmp_pos = player_power_up_pooling[Global.Rand(0, Global.power_up_item_array_index)];

            tmp_obj.transform.localPosition = tmp_pos;
            tmp_obj.SetActive(true);
            yield return new WaitForSeconds(m_item_spawn_time); // 10 seconds
        }
    }

    // 첫 적을 소환해주는 함수
    IEnumerator IE_spawn_first_monsters()
    {
        while (true)
        {
            if (!m_is_first_time) 
                yield return new WaitForSeconds(2.5f);

            GameObject tmp_obj = Object_pooling.instance.enemy_pooling.Get_first_enemy_obj();
            int rand_pos_arr_index = UnityEngine.Random.Range(0, 4);

            if (tmp_obj != null)
            {
                tmp_obj.transform.position = monster_spawn_pos.spawn_pos_list[0];
                tmp_obj.GetComponent<SpriteRenderer>().color = Global.original_color;
                tmp_obj.SetActive(true);
            }
            m_is_first_time = true;
            yield return new WaitForSeconds(5f); //5f
        }
    }
}