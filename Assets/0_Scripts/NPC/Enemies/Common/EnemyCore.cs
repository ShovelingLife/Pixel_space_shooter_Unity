using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines the core of each enemy
public class EnemyCore : MonoBehaviour
{

    public int Index = 0;

    // Position variables
    public    EnemyPath      path;
    public    EEnemyWaypoint waypoint;
    public    float          spawnTime;
    protected float          speed;
    protected float          range;

    // Plane information variables
    protected LowEnemyStatData lowEnemyStatData;
    protected Transform        bulletPos;
    protected float            hp;
    protected float            curShootTime = 0f;
    public    float            shootTime = 0f;

    public float curHp
    {
        get => hp;
        set => hp = value;
    }

    public    Sprite[]   planeSprites;
    protected GameObject explosionEffectObj;

    protected float curTime;
    protected float timer;
    protected int   maxBulletCnt;
    protected bool  playSound = true;
    public    bool  isDead;
    public    bool  isReady;
    public    bool  isActivated = false;


    protected virtual void Update()
    {
        //Enemy_attack();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        bool   isSoundOn   = false;
        string collidedTag = other.gameObject.tag;

        if (!isDead)
        {
            if (collidedTag == "Shield")
            {
                PlayerManager.inst.CharacterDmgDealed("enemyCollision");
                isSoundOn = true;
                curHp = 0f;
            }
            else if (collidedTag == "PlayerBullet")
            {
                isDead = true;
                isSoundOn = true;

                // 미사일로부터 공격받음
                PlayerMissile playerMissile = other.GetComponent<PlayerMissile>();

                if (playerMissile)
                {
                    playerMissile.GetComponent<BoxCollider2D>().enabled = false;
                    curHp -= StatManager.inst.playerPowerUpStat.missilePowerUpData.dmg;
                    playerMissile.gameObject.SetActive(false);
                }

                else
                    curHp -= PlayerManager.inst.bulletData.dmg;
            }
            else if (collidedTag == "Player")
            {
                if (PlayerManager.inst.GetComponent<SpriteRenderer>().color.a != 0)
                {
                    PlayerManager.inst.CurHp -= StatManager.inst.enemyStatData.collisionDmg;
                    PlayerManager.inst.CharacterDmgDealed("enemyCollision");
                    isSoundOn = true;
                    curHp = 0f;
                }
            }
            if (isSoundOn)
                StartCoroutine(IE_EnemyDeath());
        }
    }

    // Resources initialization
    protected virtual void Init()
    {
        // 이펙트 초기화
        explosionEffectObj = gameObject.transform.GetChild(1).gameObject;
    }

    // 죽은 후 재설정
    protected virtual void ResetAfterDead()
    {
        playSound               = true;
        isDead                  = false;
        isReady                 = false;
        transform.localPosition = new Vector3(1000f, 1000f);
        StopAllCoroutines();

        EnemyInfoManager.inst.Delete(this);
    }

    // Play sound
    protected void PlaySound()
    {
        if (playSound)
            AudioManager.inst.enemySound.PlaySpawn();
    }

    // 비행기 기울이기
    public virtual void EnemyInclining(EPlaneState _planeState)
    {
        Quaternion[] tmpRotations = new Quaternion[]
        {
            Global.ZeroRotation,
            Global.HalfRotation,
            Global.ZeroRotation
        }; 
        transform.rotation           = tmpRotations[(int)_planeState];
        GetComponent<SpriteRenderer>().sprite = planeSprites[(int)_planeState];
    }

    // 적이 공격함
    IEnumerator IE_EnemyAttack()
    {
        while (true)
        {
            EnemyInfoManager inst = EnemyInfoManager.inst;
            Transform tmpTrans = ObjectPoolingManager.inst.CreateObj(typeof(Enemy_small_bullet), inst.smallBulletObj.transform, inst.smallBulletObjContainer);
            tmpTrans.position = bulletPos.position;
            AudioManager.inst.enemySound.PlaySmallLaser();
            curShootTime = 0f;
            yield return new WaitForSeconds(shootTime);
        }
    }

    // 죽음 코루틴
    protected virtual IEnumerator IE_EnemyDeath()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }

    // 공격 코루틴 실행
    public void RunAttackCoroutine() => StartCoroutine(IE_EnemyAttack());
}