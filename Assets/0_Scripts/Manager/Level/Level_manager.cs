using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Level_manager : Singleton_local<Level_manager>
{
    public e_pooling_obj_type[] a_pooling_obj = new e_pooling_obj_type[6];
    public int[]                a_obj_quantity = new int[6];
    public Level_core   core;

    [Title("현재 레벨")]
    public e_current_level_type current_level;


    // 초기화
    public void Init()
    {
        UI_manager.instance.Set_alert_text((e_level_type)current_level);

        // 레벨 초기화
        switch (current_level)
        {
            case e_current_level_type.FIRST:  core = gameObject.AddComponent<Level1>(); break;
            case e_current_level_type.SECOND: core = gameObject.AddComponent<Level2>(); break;
            case e_current_level_type.THIRD:  core = gameObject.AddComponent<Level3>(); break;
            case e_current_level_type.FOURTH: core = gameObject.AddComponent<Level4>(); break;
            case e_current_level_type.FIFTH:  core = gameObject.AddComponent<Level5>(); break;
        }
    }

    // 레벨 실행
    public void Run_current_level()
    {
        core.Run_level();
    }
}
