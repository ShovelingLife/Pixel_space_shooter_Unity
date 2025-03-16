using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 총알 관련
[CreateAssetMenu(fileName = "EnemyBulletScriptableData", menuName = "CreateScriptableBulletData/EnemyBulletData", order = 0)]
public class EnemyBulletData : GlobalBulletData
{
    public int dmg = 1;
    public float timer = 0f;
    public float reloadTime = 0.6f;
}