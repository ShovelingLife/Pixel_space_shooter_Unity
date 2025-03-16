using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolingObjType
{
    public GameObject copyObj;
    public Transform  objContainer;
}

[Serializable]
public class PowerUpItemData
{
    public Type[] poolingObjTypes = new Type[]
    {
        typeof(BulletPowerUpItem),
        typeof(BulletSpeedUpItem),
        typeof(HealthRestoreItem),
        typeof(MissilePowerUpItem),
        typeof(ShieldPowerUpItem)
    };
}

public class StageEventData
{
    public Type            enemyType      = null;
    public EEnemyPathType  enemyPathType  = EEnemyPathType.FIRST;
    public EStageEventType eventType      = EStageEventType.MAX;
    public float           eventSec       = 0f;
    public float           enemyShootTime = 0f;
    public int             eventIndex     = 0;
    public int             count          = 0;

    // 아이템 데이터 관련
    public EPoolingObjType poolingObjType = 0;
    public EEnemyObjType   enemyObjType;


    public StageEventData SetEnemyType(Type _enemyType)
    {
        enemyType = _enemyType;
        return this;
    }

    public StageEventData SetEnemyPathType(EEnemyPathType _enemyPathType)
    {
        enemyPathType = _enemyPathType;
        return this;
    }

    public StageEventData SetEventType(EStageEventType _eventType)
    {
        eventType = _eventType;
        return this;
    }

    public StageEventData SetEventSec(float _sec)
    {
        eventSec = _sec;
        return this;
    }

    public StageEventData SetEnemyShootTime(float _shootTime)
    {
        enemyShootTime = _shootTime;
        return this;
    }

    public StageEventData SetEventIndex(int _index)
    {
        eventIndex= _index;
        return this;
    }

    public StageEventData SetCount(int _count)
    {
        count = _count;
        return this;
    }

    public StageEventData SetPoolingObjType(EPoolingObjType _poolingObjType)
    {
        poolingObjType = _poolingObjType;
        return this;
    }

    public StageEventData SetEnemyObjType(EEnemyObjType _enemyObjType)
    {
        enemyObjType = _enemyObjType;
        return this;
    }
}

public class StageEventDataList
{
    public float                eventSec = 0f;
    public List<StageEventData> monsterEventDatas = new List<StageEventData>();
    public List<StageEventData> itemEventDatas    = new List<StageEventData>();
}

partial class StageData
{
    public Stack<StageEventDataList> eventListDatas = new Stack<StageEventDataList>();
    StageEventDataList listData = new StageEventDataList();
    StageEventData   curEvent  = null;
    public string    stageName        = "스테이지01";
    float            eventSec         = 0;
    int              maxWave          = 0;
    int              monsterWave      = 0;
    int              itemWave         = 0;
    EnemyPath       tmpPath;
    EnemyPath       curPath;

    PowerUpItemData powerUpItemData = new PowerUpItemData();

    partial void FirstLevel();
    partial void SecondLevel();
    partial void ThirdLevel();
    partial void FourthLevel();
    partial void FifthLevel();

    public void AddEvent()
    {
        switch (stageName)
        {
            case "스테이지01": FirstLevel(); break;
        }
    }

    public void StageUpdate()
    {
        eventSec += Time.deltaTime;

        // 시간 경과 확인을 위해 리스트를 순회
        var peekData = eventListDatas.Peek().monsterEventDatas;

        if (itemWave >= peekData.Count)
            return;

        StageEventData data = peekData[monsterWave];

        if (eventSec >= peekData[monsterWave].eventSec)
        {
            var poolingObjTypes = LevelManager.inst.poolingObjectTypes;

            switch (data.eventType)
            {
                // 일반 적
                case EStageEventType.ENEMY:
                    {
                        // 오브젝트를 가져온 후 초기화
                        int index = (int)data.enemyObjType;
                        Transform tmpTrans = ObjectPoolingManager.inst.CreateObj(data.enemyType, 
                                                                                 poolingObjTypes[index].copyObj.transform, 
                                                                                 poolingObjTypes[index].objContainer);
                        EnemyTypeGreenOne enemy = tmpTrans.GetComponent<EnemyTypeGreenOne>();

                        // 적 정보 설정
                        curPath.gameObject.SetActive(true);
                        curPath          = EnemyPathManager.inst.paths[(int)data.enemyPathType];
                        enemy.path       = curPath;
                        enemy.shootTime = data.enemyShootTime;
                        enemy.isReady   = true;
                        enemy.RunAttackCoroutine();
                    }
                    break;

                // 보스
                case EStageEventType.BOSS_ENEMY:
                    {
                        poolingObjTypes[(int)data.eventType].copyObj.SetActive(true);
                        UI_manager.inst.SetAlertMsg(ELevelType.BOSS);
                    }
                    break;
            }
            data.count--;
            eventSec = 0f;

            // 페이즈 종료되면 다음꺼 진행
            if (data.count == 0)
            {
                tmpPath.gameObject.SetActive(false);
                tmpPath = curPath;
                monsterWave++;
            }
        }
    }

    public void UpdateItem()
    {
        eventSec += Time.deltaTime;

        // 시간 경과 확인을 위해 리스트를 순회
        var itemPeek = eventListDatas.Peek();

        if (itemWave >= itemPeek.itemEventDatas.Count)
            return;

        StageEventData data = itemPeek.itemEventDatas[itemWave];

        if (eventSec >= itemPeek.itemEventDatas[itemWave].eventSec)
        {
            var poolingObjTypes = LevelManager.inst.poolingObjectTypes;

            switch (data.eventType)
            {
                // 아이템) 0 = 치유,
                //        1 = 탄알 개수 증가
                //        2 = 탄알 속도 증가
                //        3 = 미사일
                //        4 = 보호막
                case EStageEventType.ITEM:
                    {
                        int       index    = (int)data.poolingObjType;
                        Transform tmpTrans = ObjectPoolingManager.inst.CreateObj(powerUpItemData.poolingObjTypes[index], poolingObjTypes[index].copyObj.transform, poolingObjTypes[index].objContainer);

                        // 위치 설정 후 키기
                        tmpTrans.localPosition = Utility.PowerUpPoss[Global.Rand(0, Utility.PowerUpPosArrayIndex)];
                        tmpTrans.gameObject.SetActive(true);
                    }
                    break;
            }
            data.count--;
            eventSec = 0f;

            // 페이즈 종료되면 다음꺼 진행
            if (data.count == 0)
            {
                itemWave++;
            }
        }
    }
}

public class LevelManager : SingletonLocal<LevelManager>
{
    StageData curStageData = new StageData();
    
    public PoolingObjType[] poolingObjectTypes;

    [Header("현재 레벨")]
    public ECurLvlType curLvl;


    // 초기화
    public void Init()
    {
        switch(curStageData.stageName)
        {

        }
        curStageData.AddEvent();
    }

    private void Update()
    {
        curStageData.StageUpdate();
        curStageData.UpdateItem();
    }
}