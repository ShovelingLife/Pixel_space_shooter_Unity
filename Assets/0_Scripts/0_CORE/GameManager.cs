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
        Utility.InitPowerUpPos();
        EnemyInfoManager.inst.Init(); // 경로
        UI_manager.inst.Init(); // UI
        LevelManager.inst.Init(); // 레벨 
    }
}