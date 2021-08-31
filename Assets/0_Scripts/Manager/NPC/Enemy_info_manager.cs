using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_info_manager : Singleton_local<Enemy_info_manager>
{
    // ------- Monster variables -------
    public List<Enemy_core> enemy_info_list = new List<Enemy_core>();


    void Start()
    {
    }

    void Update()
    {
        //Log_screen_manager.instance.Insert_log($"Enemy list count : {enemy_info_list.Count}");
        Check_if_enemy_is_dead();
    }

    // Set first enemy info
    public void Set_first_enemy_info(Enemy_type_green_one _enemy)
    {
        if (!enemy_info_list.Contains(_enemy))
            enemy_info_list.Add(_enemy);
    }

    // Delete from the list
    public void Delete_enemy_info(Enemy_core _core)
    {
        enemy_info_list.Remove(_core);
    }

    // Get from the list
    public Enemy_core Get_enemy_info()
    {
        Enemy_core tmp_enemy_core = null;

        foreach (var item in enemy_info_list)
        {
            if (item.current_hp > 0f)
            {
                item.current_hp -= Stat_manager.instance.power_up_stat.missile_power_up_data.missile_dmg;
                tmp_enemy_core = item;
                break;
            }
        }
        return tmp_enemy_core;
    }

    // Checking the list
    void Check_if_enemy_is_dead()
    {
        foreach (var item in enemy_info_list)
        {
            if (!item.isActiveAndEnabled)
            {
                Delete_enemy_info(item);
                break;
            }
            else
                break;
        }
    }
}
