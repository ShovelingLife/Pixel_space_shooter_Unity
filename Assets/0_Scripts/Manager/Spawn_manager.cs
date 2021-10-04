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

public class Spawn_manager : Singleton_local<Spawn_manager>
{
    public Item_spawn_info item_spawn_info;
    float m_item_spawn_time = 1f;


    void Start()
    {
        //StartCoroutine(IE_spawn_player_power_up_items());\
    }

    // 아이템을 소환해줌
    IEnumerator IE_spawn_player_power_up_items()
    {
        e_pooling_obj_type[] pooling_obj_type_arr = new e_pooling_obj_type[]
        {
            e_pooling_obj_type.PLAYER_HEALTH,
            e_pooling_obj_type.PLAYER_BULLET_POWER_UP,
            e_pooling_obj_type.PLAYER_BULLET_SPEED_UP,
            e_pooling_obj_type.PLAYER_MISSILE_POWER_UP,
            e_pooling_obj_type.PLAYER_SHIELD
        };
        while (true)
        {
            //Debug.Log(pooling_obj_type_arr[Global.Rand(0, Global.power_up_item_array_index)]);
            GameObject tmp_obj = Pooling_manager.instance.Get_obj(pooling_obj_type_arr[Global.Rand(0, Global.power_up_item_array_index)]);
            Vector3    tmp_pos = Pooling_manager.instance.player_power_up_pooling_data[Global.Rand(0, Global.power_up_position_array_index)];

            tmp_obj.transform.localPosition = tmp_pos;
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
            if(before_path != after_path)
            {
                before_path.gameObject.SetActive(false);
                after_path.gameObject.SetActive(true);
            }
        }
        yield return new WaitForSeconds(10f);
        Enemy_info_manager.instance.Turn_off_all_paths();
        Level_manager.instance.Run_current_level();
    } 
}