using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 총알 데이터
[CreateAssetMenu(fileName = "PlayerBulletScriptableData", menuName = "CreateScriptableBulletData/PlayerBulletData", order = 0)]
public class PlayerBulletData : GlobalBulletData
{
    public int   dmg        = 1;
    public float timer      = 0f;
    public float reloadTime = 0.6f;
}