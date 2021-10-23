using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_info_manager : Singleton_local<Enemy_info_manager>
{
    // ------- Monster variables -------
    public List<Enemy_core>                          list_enemy_info = new List<Enemy_core>();
    public Dictionary<e_enemy_path_type, Enemy_path> dic_enemy_path = new Dictionary<e_enemy_path_type, Enemy_path>();

    // 오브젝트들
    public GameObject small_bullet_obj;
    public Transform  small_bullet_obj_container;

    public void Init()
    {
        foreach (var item in Resources.FindObjectsOfTypeAll<Enemy_path>())
        {
            Enemy_info_manager.instance.dic_enemy_path.Add(item.enemy_path, item);
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
        foreach (var item in list_enemy_info)
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
        if (!list_enemy_info.Contains(_enemy))
            list_enemy_info.Add(_enemy);
    }

    // Delete from the list
    public void Delete_enemy_info(Enemy_core _core)
    {
        if (list_enemy_info.Count == 0)
            return;

        list_enemy_info.Remove(_core);
    }

    // Get from the list
    public Enemy_core Get_enemy_info()
    {
        Enemy_core tmp_enemy_core = null;

        foreach (var item in list_enemy_info)
        {
            Enemy_type_green_one tmp_enemy1 = item.GetComponent<Enemy_type_green_one>();

            if (tmp_enemy1)
            {
                if(tmp_enemy1.is_ready)
                {
                    tmp_enemy_core = item;
                    break;
                }
            }
        }
        return tmp_enemy_core;
    }

    // 모든 경로 차단
    public void Turn_off_all_paths()
    {
        foreach (var item in dic_enemy_path)
        {
            item.Value.gameObject.SetActive(false);
        }
    }
}