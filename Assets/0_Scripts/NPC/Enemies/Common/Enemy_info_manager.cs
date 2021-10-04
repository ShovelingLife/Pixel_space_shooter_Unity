using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_info_manager : Singleton_local<Enemy_info_manager>
{
    // ------- Monster variables -------
    public List<Enemy_core>                          l_enemy_info = new List<Enemy_core>();
    public Dictionary<e_enemy_path_type, Enemy_path> d_enemy_path = new Dictionary<e_enemy_path_type, Enemy_path>();


    public void Init()
    {
        foreach (var item in Resources.FindObjectsOfTypeAll<Enemy_path>())
        {
            Enemy_info_manager.instance.d_enemy_path.Add(item.enemy_path, item);
            item.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Log_screen_manager.instance.Insert_log($"Enemy list count : {enemy_info_list.Count}");
        Check_if_enemy_is_dead();
    }

    // Checking the list
    void Check_if_enemy_is_dead()
    {
        foreach (var item in l_enemy_info)
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

    // Set first enemy info
    public void Set_first_enemy_info(Enemy_type_green_one _enemy)
    {
        if (!l_enemy_info.Contains(_enemy))
            l_enemy_info.Add(_enemy);
    }

    // Delete from the list
    public void Delete_enemy_info(Enemy_core _core)
    {
        if (l_enemy_info.Count == 0)
            return;

        l_enemy_info.Remove(_core);
    }

    // Get from the list
    public Enemy_core Get_enemy_info()
    {
        Enemy_core tmp_enemy_core = null;

        foreach (var item in l_enemy_info)
        {
            if (item.current_hp > 0f)
            {
                item.current_hp -= Stat_manager.instance.player_power_up_stat.missile_power_up_data.missile_dmg;
                tmp_enemy_core = item;
                break;
            }
        }
        return tmp_enemy_core;
    }

    // 모든 경로 차단
    public void Turn_off_all_paths()
    {
        foreach (var item in d_enemy_path)
        {
            item.Value.gameObject.SetActive(false);
        }
    }
}