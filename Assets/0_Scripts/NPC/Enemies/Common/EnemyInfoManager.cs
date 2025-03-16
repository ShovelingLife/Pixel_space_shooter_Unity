using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoManager : SingletonLocal<EnemyInfoManager>
{
    public List<EnemyCore>                       enemyInfos = new List<EnemyCore>();
    public Dictionary<EEnemyPathType, EnemyPath> enemyPaths = new Dictionary<EEnemyPathType, EnemyPath>();

    // 오브젝트들
    public GameObject smallBulletObj;
    public Transform  smallBulletObjContainer;

    public void Init()
    {
        foreach (var item in Resources.FindObjectsOfTypeAll<EnemyPath>())
        {
            enemyPaths.Add(item.enemyPath, item);
            item.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Log_screen_manager.instance.Insert_log($"Enemy list count : {enemy_info_list.Count}");
        CheckIfEnemyIsDead();
    }

    void CheckIfEnemyIsDead()
    {
        foreach (var item in enemyInfos)
        {
            if (!item.isActiveAndEnabled)
                Delete(item);
        }
    }

    public void SetFirstEnemyInfo(EnemyTypeGreenOne _enemy)
    {
        if (!enemyInfos.Contains(_enemy))
            enemyInfos.Add(_enemy);
    }

    public void Delete(EnemyCore _core)
    {
        if (enemyInfos.Count == 0)
            return;

        enemyInfos.Remove(_core);
    }

    public EnemyCore GetEnemyInfo()
    {
        EnemyCore tmpEnemyCore = null;

        foreach (var item in enemyInfos)
        {
            EnemyTypeGreenOne tmpEnemy1 = item.GetComponent<EnemyTypeGreenOne>();

            if (tmpEnemy1)
            {
                if(tmpEnemy1.isReady)
                {
                    tmpEnemyCore = item;
                    break;
                }
            }
        }
        return tmpEnemyCore;
    }

    // 모든 경로 차단
    public void TurnOffAllPaths()
    {
        foreach (var item in enemyPaths)
        {
            item.Value.gameObject.SetActive(false);
        }
    }
}