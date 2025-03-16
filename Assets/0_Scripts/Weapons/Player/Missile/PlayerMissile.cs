using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    PlayerMissileAttack   missileAttack;
    PlayerMissileIdle     missileIdle;
    Vector3               curPos;
    [SerializeField] bool isAnotherMissile = false;

    // 미사일 겹침 방지
    Vector2      detectRayPos;
    RaycastHit2D anotherMissileHit;
    int          anotherMissileLayermask;

    // IDLE 관련
    public string curDir;
    public bool   isLostTarget;
    [SerializeField] bool isTargeted;

    public EnemyCore     enemyCore;


    private void Start()
    {
        InitSettings();
    }

    void Update()
    {
        if (enemyCore &&
            !enemyCore.isActiveAndEnabled)
            enemyCore = null;

        SetTarget();
        UpdateState();
    }

    private void OnDisable()
    {
        ObjectPoolingManager.inst.RemoveObj(typeof(PlayerMissile), transform);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        isTargeted = false;

        if (missileIdle)
            Destroy(missileIdle);

        if (missileAttack)
            Destroy(missileAttack);

        if (enemyCore)
            enemyCore = null;
    }

    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = true;

    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        string collidedObjTag = other.gameObject.tag;

        if (collidedObjTag == "Wall")
            gameObject.SetActive(false);
    }

    void InitSettings()
    {
        // 미사일 충돌 방지
        anotherMissileLayermask = Global.GetLayermaskIndex("Item");

        // 미사일끼리 충돌 방지
        detectRayPos.x = 0f; detectRayPos.y = 1f; // |
    }

    // 미사일 목표물 지정
    void SetTarget()
    {
        if (enemyCore)
        {
            // need fix
            float distance = Vector3.Distance(transform.localPosition, enemyCore.transform.localPosition);
            LogScreenManager.inst.Insert($"미사일과 적의 거리 차이 : {distance}");

            //if(distance<=100f)
                SetAttackProperty();
        }
        else
            SetIdleProperty();
    }

    // Set missile state
    void UpdateState()
    {
        if (isTargeted) // 이동 중일 시
            missileAttack.AttackEnemy();

        else // 대기일 시
            enemyCore = EnemyInfoManager.inst.GetEnemyInfo();
    }

    bool CheckForAnotherMissile()
    {
        anotherMissileHit = Physics2D.Raycast(curPos, detectRayPos, Mathf.Infinity, anotherMissileLayermask);

        if (anotherMissileHit.collider) // 타겟이 있을시
        {
            if (anotherMissileHit.collider.tag == "Player_missile")
            {
                SetIdleProperty();
                return true;
            }
        }
        return false;
    }

    void SetIdleProperty()
    {
        if (missileAttack)
        {
            Destroy(missileAttack);
            isLostTarget = true;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        if (!missileIdle)
            missileIdle = gameObject.AddComponent<PlayerMissileIdle>();

        isTargeted = false;
    }

    void SetAttackProperty()
    {
        if (missileIdle)
            Destroy(missileIdle);

        if (!missileAttack)
            missileAttack = gameObject.AddComponent<PlayerMissileAttack>();

        isTargeted   = true;
        isLostTarget = false;
    }

    // 미사일들끼리 충돌 방지
    IEnumerator IE_TouchingAnotherMissile()
    {
        isAnotherMissile = true;
        yield return new WaitForSeconds(0.5f);
        isAnotherMissile = false;
    }
}