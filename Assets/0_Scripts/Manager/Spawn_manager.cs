using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item_spawn_data
{
    [Header("파워업 아이템")]
    public Type       power_up_type;
    public GameObject power_up_obj;
    public Transform  power_up_obj_container;
}

[Serializable]
public class Item_spawn_data_list
{
    public List<Item_spawn_data> list_item_spawn_data = new List<Item_spawn_data>();

    Type[] m_arr_pooling_obj_type = new Type[]
    {
            typeof(Bullet_power_up_item),
            typeof(Bullet_speed_up_item),
            typeof(Health_restore_item),
            typeof(Missile_power_up_item),
            typeof(Shield_power_up_item)
    };

    public Item_spawn_data this[int _index]
    {
        // 타입 설정 후 멤버를 가져옴
        get 
        {
            list_item_spawn_data[_index].power_up_type = m_arr_pooling_obj_type[_index];
            return list_item_spawn_data[_index]; 
        }
    }
}

[Serializable]
public class Item_spawn_pos_data
{
    Vector3[]      m_arr_power_up_init_pos;
    readonly float pos_y = 25.3f;


    // 랜덤 위치를 가지고 옴
    public Vector3 this[int _index]
    {
        get { return m_arr_power_up_init_pos[_index]; }
    }

    // 값 초기화
    public void Init_values()
    {
        // Position array initialization
        m_arr_power_up_init_pos = new Vector3[Global.power_up_position_array_index];
        float pos_x = -9.75f;

        for (int i = 0; i < Global.power_up_position_array_index; i++)
        {
            m_arr_power_up_init_pos[i].x = pos_x;
            m_arr_power_up_init_pos[i].y = pos_y;
            m_arr_power_up_init_pos[i].z = 3f;
            pos_x += 0.75f;
        }
    }
}

public class Spawn_manager : Singleton_local<Spawn_manager>
{
    public Item_spawn_data_list item_spawn_data_list;
    public Item_spawn_pos_data  item_spawn_pos_data;
    float                       m_item_spawn_time = 1f;


    void Start()
    {
        item_spawn_pos_data.Init_values();
        //StartCoroutine(IE_spawn_player_power_up_items());
    }

    private void Update()
    {
        
    }

    // 아이템을 소환해줌
    public IEnumerator IE_spawn_player_power_up_items()
    {
        while (true)
        {
            //Debug.Log(pooling_obj_type_arr[Global.Rand(0, Global.power_up_item_array_index)]);
            Object_pooling_manager pooling_inst            = Object_pooling_manager.instance;
            int                    rand_item_index         = Global.Rand(0, Global.power_up_item_array_index);
            int                    rand_power_up_pos_index = Global.Rand(0, Global.power_up_position_array_index);
            int                    rand_pos_index          = Global.Rand(0, Global.power_up_position_array_index);
            Item_spawn_data        tmp_spawn_data          = item_spawn_data_list[rand_item_index];
            Transform              tmp_trans               = pooling_inst.Create_obj(tmp_spawn_data.power_up_type, tmp_spawn_data.power_up_obj.transform, tmp_spawn_data.power_up_obj_container);

            // 위치 설정 후 키기
            tmp_trans.localPosition = item_spawn_pos_data[rand_pos_index];
            tmp_trans.gameObject.SetActive(true);

            yield return new WaitForSeconds(m_item_spawn_time); // 10 seconds
        }
    }

    // 적을 소환해줌
    public IEnumerator IE_spawn_monsters(Enemy_core[] _a_enemy)
    {
        Enemy_path before_path = _a_enemy[0].path;
        Enemy_path after_path  = null;

        before_path.gameObject.SetActive(true);

        for (int i = 0; i < _a_enemy.Length; i++)
        {
            yield return new WaitForSeconds(_a_enemy[i].spawn_time);

            after_path = _a_enemy[i].path;

            if (_a_enemy[i] != null)
                _a_enemy[i].is_ready = true;

            // 경로 변경
            if (before_path != after_path)
            {
                before_path.gameObject.SetActive(false);
                after_path.gameObject.SetActive(true);
            }
        }
        yield return new WaitForSeconds(10f);
        Enemy_info_manager.instance.Turn_off_all_paths();
    } 
}