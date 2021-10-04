using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level_core, i_level_functions
{
    public override void Init()
    {
        level_functions = this;
    }

    public override void Run_level()
    {
        base.Run_level();
    }

    public void Run_phase1()
    {
        for (int i = 0; i < count; i++)
        {
            if (i < count / 2)
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.FIRST];
                ma_enemies[i].spawn_time = 0.25f;
            }
            else
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.SECOND];

                if (i == count / 2)
                    ma_enemies[i].spawn_time = 10f;

                else
                    ma_enemies[i].spawn_time = 0.5f;
            }
        }
    }

    public void Run_phase2()
    {
        for (int i = 0; i < count; i++)
        {
            if (i < count / 2) // 10마리일 때 쿨타임 적용
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.FIFTH];
                ma_enemies[i].spawn_time = 0.2f;
            }
            else
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.FIRST];

                if (i == count / 2)
                    ma_enemies[i].spawn_time = 10f;

                else
                    ma_enemies[i].spawn_time = 2.5f;
            }
        }
    }

    public void Run_phase3()
    {
        for (int i = 0; i < count; i++)
        {
            if (i < count / 2)
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.THIRD];
                ma_enemies[i].spawn_time = 0.25f;
            }
            else
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.FIFTH];

                if (i == count / 2)
                    ma_enemies[i].spawn_time = 10f;

                else
                    ma_enemies[i].spawn_time = 3f;
            }
        }
    }

    public void Run_phase4()
    {
        for (int i = 0; i < count; i++)
        {
            if (i < count / 2)
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.FOURTH];
                ma_enemies[i].spawn_time = 1f;
            }
            else
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.SECOND];

                if (i == count / 2)
                    ma_enemies[i].spawn_time = 10f;

                else
                    ma_enemies[i].spawn_time = 2f;
            }
        }
    }

    public void Run_phase5()
    {
        for (int i = 0; i < count; i++)
        {
            if (i < count / 2)
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.FOURTH];
                ma_enemies[i].spawn_time = 0.5f;
            }
            else
            {
                ma_enemies[i].path = Enemy_info_manager.instance.d_enemy_path[e_enemy_path_type.THIRD];

                if (i == count / 2)
                    ma_enemies[i].spawn_time = 10f;

                else
                    ma_enemies[i].spawn_time = 1f;
            }
        }
    }

    public void Run_boss_phase()
    {
        throw new System.NotImplementedException();
    }
}
