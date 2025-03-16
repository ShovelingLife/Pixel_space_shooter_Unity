using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 키 입력 함수들을 들고있는 클래스
[Serializable]
public class PlayerKey
{
    public Dictionary<KeyCode, Action> singleKeyFn     =      new Dictionary<KeyCode, Action>();
    public Dictionary<KeyCode, Action> continuousKeyFn =  new Dictionary<KeyCode, Action>();
    public Dictionary<KeyCode, Action> singleKeyUpFn   =   new Dictionary<KeyCode, Action>();
}

// 플레이어 풀링 데이터 들고있음
[Serializable]
public class PlayerPoolingData
{
    [Header("플레이어 총알")]
    public Transform  transBulletObjContainer;
    public GameObject bulletObj;

    [Header("플레이어 미사일")]
    public Transform  transMissileObjContainer;
    public GameObject missileObj;
}

public class PlayerManager : SingletonLocal<PlayerManager>
{
    // 플레이어 관련
    [Header("플레이어 관련")]
    public Sprite[]          sprites = new Sprite[3];
    SpriteRenderer           curSpriteRenderer;
    Vector2                  movePos;
    bool                     isMovable;
    public PlayerPoolingData playerPoolingData;
    public HealthRestoreData healthRestoreData;

    // 플레이어 입력 관련
    [Header("플레이어 키 입력 관련")]
    public PlayerKey playerKey;
    //Animator player_animator;

    // 플레이어 스텟 관련
    [Header("플레이어 파워업 정보 관련")]
    public PlayerStatData statData;
    PlayerPowerUpStat     powerUpStat;
    float                 curHp;
    
    public float CurHp
    {
        get => curHp;
        set => curHp = value;
    }
    // 플레이어 충돌 관련
    RaycastHit2D hit;
    Vector2      rayDir;
    GameObject   deathAnimObj;
    bool         isDead;

    // 플레이어 총알 관련
    [Header("플레이어 총알 관련")]
    public PlayerBulletData bulletData;
    public Transform        bulletPos;

    // UI 관련
    float curTimeCnt;
    float timer = 2f;
    bool  isFirstTime = true;
    bool  isOnPause;


    void Start()
    {
        InitPlayerSettings();
        InitPlayerInputKeySettings();
    }

    void Update()
    {
        bulletData.timer += Time.deltaTime;
        // 처음이고 캐릭터가 안움직일시
        FirstTime();

        if (!isDead )
        {
            // Moved by mouse
            //Move_player_by_mouse();

            // Moved by keyboard
            InputSingleKeyUp();
            InputSingleKey();
            InputContinuousKey();

            // Power up
            CreateShield();
            CreateMissile(powerUpStat.missileLvl);

            // Exception
            CheckIfInScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (powerUpStat.isShieldCreated)
        {
            if (other.gameObject.tag == "EnemyBullet"|| 
                other.gameObject.tag == "Enemy") 
                AudioManager.inst.playerSound.PlayShieldReceiveDmg();
        }
    }

    // 플레이어 이탈 방지
    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 tmpPos = transform.position;

        if(other.gameObject.tag == "Wall")
        {
            if (transform.position.y >= 17.5f)
                tmpPos.y-= 0.1f;
        }
        transform.position = tmpPos;
    }

    private void OnEnable()
    {
        
    }

    // 드래그 시작할시
    private void OnMouseDrag()
    {
        Debug.Log("IsDragging");
    }

    // 캐릭터 초기화
    void InitPlayerSettings()
    {
        powerUpStat           = StatManager.inst.playerPowerUpStat;
        curSpriteRenderer     = GetComponent<SpriteRenderer>();
        deathAnimObj          = transform.GetChild(1).gameObject;
        powerUpStat.shieldObj = transform.GetChild(2).gameObject;
        statData              = StatManager.inst.playerStatData;
        curHp                 = statData.maxHp;
    }

    void FirstTime()
    {
        if (isFirstTime)
        {
            curTimeCnt += Time.deltaTime;

            if (curTimeCnt > timer)
            {
                curTimeCnt = 0f;
                isFirstTime = false;
                isMovable = true;
            }
        }
    }

    #region 입력 관련

    // 플레이어 키 입력 관련 초기화
    void InitPlayerInputKeySettings()
    {
        // 키를 한 번 눌렀을 시
        playerKey.singleKeyFn[KeyCode.P]              = ShowPauseMenu; // P키 (정지/재개)

        // 키를 연속적으로 눌렀을 시
        playerKey.continuousKeyFn[KeyCode.UpArrow]    = MovingUp;    // 윗키
        playerKey.continuousKeyFn[KeyCode.LeftArrow]  = MovingLeft;  // 왼쪽키
        playerKey.continuousKeyFn[KeyCode.DownArrow]  = MovingDown;  // 아랫키
        playerKey.continuousKeyFn[KeyCode.RightArrow] = MovingRight; // 오른쪽키
        playerKey.continuousKeyFn[KeyCode.Space]      = ShootBullet; // 발사키

        // 키를 눌렀다 땠을 시
        playerKey.singleKeyUpFn[KeyCode.UpArrow]     = StoppedMoving; // 윗키
        playerKey.singleKeyUpFn[KeyCode.LeftArrow]   = StoppedMoving; // 왼쪽키
        playerKey.singleKeyUpFn[KeyCode.DownArrow]   = StoppedMoving; // 아랫키
        playerKey.singleKeyUpFn[KeyCode.RightArrow]  = StoppedMoving; // 오른쪽키
    }

    // 일시정지 혹 재개 P키
    void ShowPauseMenu()
    {
        isOnPause = !isOnPause;
        Time.timeScale = (float)Convert.ToDouble(isOnPause);
    }

    void MovingUp()
    {
        movePos.y += statData.moveSpeed * Time.deltaTime;
        rayDir = Vector2.up;
    }

    void MovingDown()
    {
        movePos.y -= statData.moveSpeed * Time.deltaTime;
        rayDir = Vector2.down;
    }

    void MovingLeft()
    {
        curSpriteRenderer.sprite = sprites[2]; // 기울이기
        movePos.x -= statData.moveSpeed * Time.deltaTime;
        rayDir = Vector2.left;
    }

    void MovingRight()
    {
        curSpriteRenderer.sprite = sprites[2]; // 기울이기
        movePos.x += statData.moveSpeed * Time.deltaTime;
        transform.rotation = Global.HalfRotation;
        rayDir = Vector2.right;
    }

    void StoppedMoving()
    {
        transform.rotation = Global.ZeroRotation;
        curSpriteRenderer.sprite = sprites[0];
    }

    void CheckIfInScreen()
    {
        Vector2 tmpPos = transform.position;

        if (transform.position.y <= -15.6f)
            tmpPos.y += 0.01f;

        transform.position = tmpPos;
    }
    
    // 캐릭터 이동 모션
    void MovingMotion(Vector3 _movingPos)
    {
        // 왼쪽
        if (_movingPos.x < 0)
        {
            transform.rotation = Global.ZeroRotation;
            curSpriteRenderer.sprite = sprites[2];
        }
        else
        {
            transform.rotation = Global.HalfRotation;
            curSpriteRenderer.sprite = sprites[2];
        }
    }

    // 캐릭터 마우스로 이동
    void MoveByMouse()
    {
        if (!isMovable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMovable = true;
                isFirstTime = false;
            }
        }
        else
        {
            Vector3 movePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            movePos.z = 0f;
            MovingMotion(movePos);
            this.transform.position = movePos;
            ShootBullet();
        }
    }

    void InputSingleKey()
    {
        foreach (var inputKey in playerKey.singleKeyFn)
        {
            if (Input.GetKeyDown(inputKey.Key)) 
                inputKey.Value.Invoke();
        }
    }

    void InputContinuousKey()
    {
        int layerMask  = Global.GetLayermaskIndex("Wall");
        movePos = transform.position;

        foreach (var inputKey in playerKey.continuousKeyFn)
        {
            if (Input.GetKey(inputKey.Key))
            {
                inputKey.Value.Invoke();

                if (isFirstTime)
                {
                    isFirstTime     = false;
                    isMovable = true;
                }
            }
        }
        // 이동 제한
        hit = Physics2D.Raycast(transform.position, rayDir, 0.5f, layerMask);

        if (hit.collider != null) 
            return;

        transform.position = movePos;
    }

    // 플레이어가 키를 눌렀다가 땠을 시
    void InputSingleKeyUp()
    {
        foreach (var input in playerKey.singleKeyUpFn)
        {
            if (Input.GetKeyUp(input.Key)) 
                input.Value.Invoke();
        }
    }

    // 캐릭터 총알 발사
    void ShootBullet()
    {
        float bulletReloadTime = ActivateBulletBooster(powerUpStat.isBoosterOn);

        if (bulletData.timer > bulletReloadTime)
        {
            ActivatePowerUp(powerUpStat.powerUpLvl);
            bulletData.timer = 0f;
        }
    }

    #endregion

    #region 플레이어 파워업 관련

    // 보호막
    void CreateShield() => powerUpStat.shieldObj.SetActive(powerUpStat.isShieldCreated);

    void CreateMissile(int _missileLvl)
    {
        if (_missileLvl == 0) 
            return;

        powerUpStat.missileCurTime += Time.deltaTime;

        if (powerUpStat.missileCurTime > powerUpStat.missileReloadTime)
        {
            GameObject[] missileObjs = new GameObject[statData.maxMissileLvl];
            AudioManager.inst.playerSound.PlayPlayerMissile();

            for (int i = 0; i < _missileLvl; i++)
            {
                if (i == statData.maxMissileLvl) 
                    return;

                missileObjs[i] = ObjectPoolingManager.inst.CreateObj(typeof(PlayerMissile), playerPoolingData.missileObj.transform, playerPoolingData.transMissileObjContainer).gameObject;
                PlayerMissile playerMissile = missileObjs[i].GetComponent<PlayerMissile>();

                if (missileObjs[i] != null)
                {
                    if (i == 0) // 왼쪽 방향
                    {
                        missileObjs[i].transform.position = powerUpStat.missileFirstLvlTrans.position;
                        playerMissile.curDir      = "LeftDirection";
                    }
                    if (i == 1) // 오른쪽 방향    
                    {
                        missileObjs[i].transform.position = powerUpStat.missileSecondLvlTrans.position;
                        playerMissile.curDir      = "RightDirection";
                    }
                }
            }
            powerUpStat.missileCurTime = 0f;
        }
    }

    float ActivateBulletBooster(bool _isBoosterOn)
    {
        return (_isBoosterOn) ? bulletData.reloadTime / powerUpStat.bulletSpeedUpData.increaseSpeed :
                                bulletData.reloadTime;
    }

    void ActivatePowerUp(int _powerUpLvl)
    {
        GameObject[] bulletObjs = new GameObject[statData.maxPowerUpLvl];
        AudioManager.inst.playerSound.PlayLaser();

        for (int i = 0; i < _powerUpLvl + 1; i++)
        {
            if (i == statData.maxPowerUpLvl) 
                return;

            bulletObjs[i] = ObjectPoolingManager.inst.CreateObj(typeof(Player_bullet), playerPoolingData.bulletObj.transform, playerPoolingData.transBulletObjContainer).gameObject;

            if (bulletObjs[i] != null)
            {
                if      (i == 0) 
                         bulletObjs[i].transform.position = bulletPos.position;

                else if (i == 1) 
                         bulletObjs[i].transform.position = powerUpStat.secondBulletTrans.position;

                else if (i == 2) 
                         bulletObjs[i].transform.position = powerUpStat.thirdBulletTrans.position;
            }
        }
    }

    public void CheckRestoreHealth()
    {
        if (CurHp < statData.maxHp)
            CurHp += healthRestoreData.restoreHealth;
    }

    #endregion

    #region 캐릭터 죽음 관련

    public void CharacterDmgDealed(string _deathType)
    {
        // 적 발사체랑 충돌
        if (_deathType == "enemyBullet")
            AudioManager.inst.playerSound.PlayBulletDeath();

        // 적이랑 충돌
        if (_deathType == "enemyCollision") 
            AudioManager.inst.playerSound.PlayCollisionDeath();

        transform.rotation = Global.ZeroRotation;
        curSpriteRenderer.sprite = sprites[0];

        if (CurHp <= 0) 
            StartCoroutine(IE_PlayerDeathAnim()); // 죽음
    }

    IEnumerator IE_PlayerDeathAnim()
    {
        deathAnimObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        deathAnimObj.SetActive(false);        
        gameObject.GetComponent<SpriteRenderer>().color = Global.SpriteFadeColor;
        isDead = true;
        UI_manager.inst.SetAlertMsg(ELevelType.END);
    }

    #endregion
}