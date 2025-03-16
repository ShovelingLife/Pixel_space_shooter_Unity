using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(EnemyCore))]
public class EnemyTypeGreenOne : EnemyCore
{
    void Awake()
    {
        base.Init();
        Init();
    }

    protected override void Update()
    {
        if (!isDead) // Not dead
            EnemyAction();

        else // Dead
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
        {
            base.OnTriggerEnter2D(other);

            if (other.gameObject.tag == "Player_bullet")
                UI_manager.inst.curScore += 10;            
        }
    }

    protected override void OnEnable()
    {
        hp = 1f;
    }

    protected override void OnDisable()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnApplicationQuit()
    {
        
    }

    // 변수 초기화
    protected override void Init()
    {
        // 정보 초기화
        timer            = 0.75f;
        maxBulletCnt     = 2;
        lowEnemyStatData = StatManager.inst.enemyStatData;
        hp               = 1f;
        speed = 0.35f;

        // 위치 초기화
        bulletPos        = gameObject.transform.GetChild(0).gameObject.transform;
    }

    protected override IEnumerator IE_EnemyDeath()
    {
        float duration = 0.5f;

        // Bullet touched
        AudioManager.inst.enemySound.PlayDeath();
        explosionEffectObj.SetActive(true);
        yield return new WaitForSeconds(duration);
        GetComponent<SpriteRenderer>().color = Global.SpriteFadeColor;
        yield return new WaitForSeconds(duration);
        explosionEffectObj.SetActive(false);
        ObjectPoolingManager.inst.RemoveObj(typeof(EnemyTypeGreenOne), transform);
        base.ResetAfterDead();
    }

    // 적 움직임
    void EnemyAction()
    {
        EnemyInfoManager.inst.SetFirstEnemyInfo(this);
        Vector3 curPos = transform.localPosition;

        if (isReady)
        {
            PlaySound();
            playSound = false;

            range += (speed * Time.deltaTime);

            switch (waypoint)
            {
                case EEnemyWaypoint.FIRST:  transform.position = path.GetFirstPath(range); break;
                case EEnemyWaypoint.SECOND: transform.position = path.GetSecondPath(range); break;

                case EEnemyWaypoint.THIRD:
                    ResetAfterDead();
                    waypoint = 0;
                    break;
            }
            if (range > 1f)
            {
                range = 0f;
                waypoint++;
            }
        }
    }

    // 적 이동
    void Move(Vector3 _targetPos)
    {
        transform.DOLocalMove(_targetPos, lowEnemyStatData.moveSpeed);
    }

    // 스프라잇 변경
    public void ChangeSprite(EPlaneState _state)
    {
        GetComponent<SpriteRenderer>().sprite = planeSprites[(int)_state];
    }

    public override void EnemyInclining(EPlaneState _state)
    {
        base.EnemyInclining(_state);
    }
}