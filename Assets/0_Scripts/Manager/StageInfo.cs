using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class StageData
{
    partial void FirstLevel()
    {
        // 첫번째 웨이브
        {
            // 몬스터
            listData.monsterEventDatas
              .Add(new StageEventData()
              .SetEnemyType(typeof(EnemyTypeGreenOne))
              .SetEnemyPathType(EEnemyPathType.SECOND)
              .SetEventType(EStageEventType.ENEMY)
              .SetEnemyShootTime(1.25f)
              .SetEventIndex(2)
              .SetEventSec(1f)
              .SetCount(15)
              .SetEnemyObjType(EEnemyObjType.ENEMY_GREEN_TYPE_ONE));

            // 아이템
            listData.itemEventDatas
              .Add(new StageEventData()
              .SetEventType(EStageEventType.ITEM)
              .SetEventIndex(1)
              .SetEventSec(1f)
              .SetCount(5)
              .SetPoolingObjType(EPoolingObjType.PLAYER_HEALTH));
        }
        // 두번쨰 웨이브
        {
            listData.monsterEventDatas
              .Add(new StageEventData()
              .SetEnemyType(typeof(EnemyTypeGreenOne))
              .SetEnemyPathType(EEnemyPathType.FIFTH)
              .SetEventType(EStageEventType.ENEMY)
              .SetEnemyShootTime(1f)
              .SetEventIndex(3)
              .SetEventSec(2f)
              .SetCount(10)
              .SetEnemyObjType(EEnemyObjType.ENEMY_GREEN_TYPE_ONE));
        }
        // 세번째 웨이브
        {
            listData.monsterEventDatas
              .Add(new StageEventData()
              .SetEnemyType(typeof(EnemyTypeGreenOne))
              .SetEnemyPathType(EEnemyPathType.SECOND)
              .SetEventType(EStageEventType.ENEMY)
              .SetEnemyShootTime(1.25f)
              .SetEventIndex(2)
              .SetEventSec(1f)
              .SetCount(20)
              .SetEnemyObjType(EEnemyObjType.ENEMY_GREEN_TYPE_ONE));
        }
        // 네번째 웨이브
        {
            listData.monsterEventDatas
              .Add(new StageEventData()
              .SetEnemyType(typeof(EnemyTypeGreenOne))
              .SetEnemyPathType(EEnemyPathType.FOURTH)
              .SetEventType(EStageEventType.ENEMY)
              .SetEnemyShootTime(1.5f)
              .SetEventIndex(2)
              .SetEventSec(1.5f)
              .SetCount(10)
              .SetEnemyObjType(EEnemyObjType.ENEMY_GREEN_TYPE_ONE));

        }
        // 다섯번째 웨이브
        {
            listData.monsterEventDatas
              .Add(new StageEventData()
              .SetEnemyType(typeof(EnemyTypeGreenOne))
              .SetEnemyPathType(EEnemyPathType.THIRD)
              .SetEventType(EStageEventType.ENEMY)
              .SetEnemyShootTime(2f)
              .SetEventIndex(2)
              .SetEventSec(1.75f)
              .SetCount(15)
              .SetEnemyObjType(EEnemyObjType.ENEMY_GREEN_TYPE_ONE));
        }
        // 마지막 웨이브
        {
            listData.monsterEventDatas
              .Add(new StageEventData()
              .SetEventType(EStageEventType.BOSS_ENEMY)
              .SetEnemyShootTime(1.25f)
              .SetEventSec(1f));
        }
        maxWave = eventListDatas.Count;
        eventListDatas.Push(listData);
    }

    partial void SecondLevel()
    {

    }
};