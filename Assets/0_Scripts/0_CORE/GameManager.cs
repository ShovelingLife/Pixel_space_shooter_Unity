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
        Global.Init_arr_power_up_pos();
        Enemy_info_manager.instance.Init(); // 경로
        UI_manager.instance.Init(); // UI
        Level_manager.instance.Init(); // 레벨 
    }
}
