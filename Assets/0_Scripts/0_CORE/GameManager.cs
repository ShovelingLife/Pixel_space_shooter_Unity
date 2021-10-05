using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    // 초기화
    private void Init()
    {
        // 풀링
        Pooling_manager.instance.Init_class_members();
        Pooling_manager.instance.Init_obj_pooling();

        // 경로
        Enemy_info_manager.instance.Init();

        // UI
        UI_manager.instance.Init();

        // 아이템
        StartCoroutine(Spawn_manager.instance.IE_spawn_player_power_up_items());

        // 레벨 
        Level_manager.instance.Init();
        Level_manager.instance.core.Init();
        Level_manager.instance.Run_current_level();
    }
}
