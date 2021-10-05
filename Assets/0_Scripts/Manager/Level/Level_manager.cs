using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Level_manager : Singleton_local<Level_manager>
{
    GameObject[]        ma_level_prefab;
    public Level_core   core;

    [Title("현재 레벨")]
    public e_current_level_type current_level;


    // 초기화
    public void Init()
    {
        UI_manager.instance.Set_alert_text((e_level_type)current_level);

        // 자식 오브젝트 초기화
        ma_level_prefab = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            ma_level_prefab[i] = transform.GetChild(i).gameObject;

        // 레벨 초기화
        switch (current_level)
        {
            case e_current_level_type.FIRST:  core = ma_level_prefab[0].GetComponent<Level1>(); break;
            case e_current_level_type.SECOND: core = ma_level_prefab[1].GetComponent<Level2>(); break;
            case e_current_level_type.THIRD:  core = ma_level_prefab[2].GetComponent<Level3>(); break;
            case e_current_level_type.FOURTH: core = ma_level_prefab[3].GetComponent<Level4>(); break;
            case e_current_level_type.FIFTH:  core = ma_level_prefab[4].GetComponent<Level5>(); break;
        }
    }

    // 레벨 실행
    public void Run_current_level()
    {
        core.Run_level();
    }
}
