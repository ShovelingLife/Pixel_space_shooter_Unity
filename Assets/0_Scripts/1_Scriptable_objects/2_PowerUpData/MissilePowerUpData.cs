using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 미사일 아이템 데이터
[CreateAssetMenu(fileName = "PlayerMissilePowerUpScriptableData", menuName = "CreateScriptablePowerUpData/MissilePowerUpData", order = 2)]
public class MissilePowerUpData : GlobalPowerUpData
{
    public float dmg;
    public float speed = 3f;
    
    // IDLE 관련
    public float idleSpeed = 0.025f;
    public float idleDist  = 1f;
}