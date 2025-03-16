using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_manager : SingletonLocal<UI_manager>
{
    [Header("파워 업 UI 데이터")]
    public Power_up_UI_data powerUpUI_data;

    // 수정 
    [Header("파워 업 UI")]
    public BulletPowerUpUI bulletPowerUpUI;
    public BulletSpeedUpUI bulletSpeedUpUI;
    public MissilePowerUpUI missilePowerUpUI;
    public ShieldPowerUpUI shieldPowerUpUI;

    [Header("안내 메시지")]
    public GameObject gameStartTxtObj;
    Text              txtGameStart;
    float             curTimeCnt;
    float             timer = 0.5f;
    int               displayCnt = 0;
    bool              displayAlertTxt;
    bool              isTextFading;

    [Header("플레이어")]
    public Text    playerScoreTxt;
    public Image   playerHpImg;
    public int     curScore = 0;

    [Header("보스 몬스터")]
    public GameObject bossHpObj;

    void Update()
    {
        ShowAlertTxt();
        ShowPlayerInfo();
    }

    // 변수 초기화
    public void Init()
    {
        txtGameStart     = gameStartTxtObj.GetComponent<Text>();
        bulletPowerUpUI  = GetComponent<BulletPowerUpUI>();
        bulletSpeedUpUI  = GetComponent<BulletSpeedUpUI>();
        shieldPowerUpUI  = GetComponent<ShieldPowerUpUI>();
        missilePowerUpUI = GetComponent<MissilePowerUpUI>();
    }

    // 게임 시작 문구
    void ShowAlertTxt()
    {
        if (!displayAlertTxt) 
            return;

        else // 띄울 메시지가 있으면
        {
            curTimeCnt += Time.deltaTime;
            
            if (displayCnt > 3) // 최대 횟수
            {
                displayCnt = 0;
                displayAlertTxt = false;
            }
            if (curTimeCnt > timer) // 타이머
            {
                if (!isTextFading) // 사라지지 않고 있으면
                {
                    txtGameStart.color = Global.OriginalColor;
                    isTextFading = true;
                }
                else // 사라지고 있으면
                {
                    txtGameStart.color = Global.SpriteFadeColor;
                    isTextFading = false;
                }
                curTimeCnt = 0f;
                displayCnt++;
            }
        }
    }

    void ShowPlayerInfo()
    {
        PlayerStatData playerStatData = StatManager.inst.playerStatData;
        playerScoreTxt.text           = "현재 점수 : " + curScore;
        playerHpImg.fillAmount        = PlayerManager.inst.CurHp / playerStatData.maxHp;

        // 파워업 레벨
        powerUpUI_data.bullet_power_up_txt.text  = StatManager.inst.playerPowerUpStat.powerUpLvl.ToString();
        powerUpUI_data.bullet_speed_up_txt.text  = StatManager.inst.playerPowerUpStat.speedUpLvl.ToString();
        powerUpUI_data.missile_power_up_txt.text = StatManager.inst.playerPowerUpStat.missileLvl.ToString();
        powerUpUI_data.shield_power_up_txt.text  = StatManager.inst.playerPowerUpStat.shieldLvl.ToString();
    }

    // 안내 메시지 메시지 바꿈
    public void SetAlertMsg(ELevelType _curLvl)
    {
        Dictionary<ELevelType, string> levels = new Dictionary<ELevelType, string>()
        {
            {ELevelType.END,"게임 종료" },        {ELevelType.FIRST,"첫번째 스테이지" },{ELevelType.SECOND,"두번째 스테이지" },
            {ELevelType.THIRD,"세번째 스테이지" },{ELevelType.FOURTH,"네번째 스테이지" },{ELevelType.FIFTH,"다섯번째 스테이지" },
            {ELevelType.BOSS,"보스 출몰" }
        };
        txtGameStart.text = levels[_curLvl];
        displayAlertTxt = true;
    }
}

[System.Serializable]
public class Power_up_UI_data
{
    [Header("플레이어 탄알 공격력 증가")]
    public GameObject original_bullet_power_up_obj;
    public GameObject timer_bullet_power_up_obj;
    public Text bullet_power_up_txt;

    [Header("플레이어 탄알 공격 속도 증가")]
    public GameObject original_bullet_speed_up_obj;
    public GameObject timer_bullet_speed_up_obj;
    public Text bullet_speed_up_txt;

    [Header("플레이어 유도 미사일 생성")]
    public GameObject original_missile_obj;
    public GameObject timer_missile_obj;
    public Text missile_power_up_txt;

    [Header("플레이어 보호막 생성")]
    public GameObject original_shield_obj;
    public GameObject timer_shield_obj;
    public Text shield_power_up_txt;
}