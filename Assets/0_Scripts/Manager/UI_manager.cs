using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_manager : Singleton_local<UI_manager>
{
    // 안내 메시지 관련
    public GameObject game_start_text_obj;
    Text              game_start_txt;
    float             m_current_time_count;
    float             m_timer = 0.5f;
    int               m_display_count = 0;
    string            m_alert_text_type;
    bool              m_display_alert_text;
    bool              m_is_text_fading;

    // 플레이어 관련
    Player_manager player_manager;
    public Text    player_score_txt;
    public Image   player_hp_img;
    public int     current_score = 0;

    // 파워업 UI 관련
    [Header("파워 업 UI")]
    public Power_up_UI         power_up_UI;
    public Bullet_power_up_UI  bullet_power_up_ui;
    public Bullet_speed_up_UI  bullet_speed_up_ui;
    public Missile_power_up_UI missile_power_up_ui;
    public Shield_power_up_UI  shield_power_up_ui;


    void Start()
    {
        Init_settings();
    }

    void Update()
    {
        Change_alert_text();
        Show_alert_text();
        Show_player_info();
    }

    private void OnEnable()
    {
        
    }    

    // 변수 초기화
    void Init_settings()
    {
        game_start_txt      = game_start_text_obj.GetComponent<Text>();
        player_manager      = GameObject.FindObjectOfType<Player_manager>();
        bullet_power_up_ui  = GetComponent<Bullet_power_up_UI>();
        bullet_speed_up_ui  = GetComponent<Bullet_speed_up_UI>();
        shield_power_up_ui  = GetComponent<Shield_power_up_UI>();
        missile_power_up_ui = GetComponent<Missile_power_up_UI>();
    }

    // 안내 메시지 메시지 바꿈
    void Change_alert_text()
    {
        switch(m_alert_text_type)
        {
            case "first_time": game_start_txt.text = "게임을 시작합니다"; break;

            case "death":      game_start_txt.text = "게임 오버"; break;
        }
    }

    // 게임 시작 메시지 띄움
    public void Run_start_text_obj()
    {
        m_alert_text_type = "first_time";
        m_display_alert_text = true;
    }
    
    // 플레이어 죽음 메시지 띄움
    public void Game_over()
    {
        m_alert_text_type = "death";
        m_display_alert_text = true;
    }

    // 게임 시작 문구
    void Show_alert_text()
    {
        if (!m_display_alert_text) 
            return;

        else // 띄울 메시지가 있으면
        {
            m_current_time_count += Time.deltaTime;
            
            if (m_display_count > 3) // 최대 횟수
            {
                m_display_count = 0;
                m_display_alert_text = false;
            }
            if (m_current_time_count > m_timer) // 타이머
            {
                if (!m_is_text_fading) // 사라지지 않고 있으면
                {
                    game_start_txt.color = Global.original_color;
                    m_is_text_fading = true;
                }
                else // 사라지고 있으면
                {
                    game_start_txt.color = Global.sprite_fade_color;
                    m_is_text_fading = false;
                }
                m_current_time_count = 0f;
                m_display_count++;
            }
        }
    }

    // 플레이어 정보 띄움
    void Show_player_info()
    {
        Player_stat_data player_stat_data = Stat_manager.instance.player_stat_data;
        player_score_txt.text             = "현재 점수 : " + current_score;
        player_hp_img.fillAmount          = player_manager.current_hp_prop / player_stat_data.max_hp;

        // 파워업 레벨
        power_up_UI.bullet_power_up_txt.text  = Player_manager.instance.player_power_up_info.power_up_level.ToString();
        power_up_UI.bullet_speed_up_txt.text  = Player_manager.instance.player_power_up_info.speed_up_level.ToString();
        power_up_UI.missile_power_up_txt.text = Player_manager.instance.player_power_up_info.missile_level.ToString();
        power_up_UI.shield_power_up_txt.text  = Player_manager.instance.player_power_up_info.shield_level.ToString();
    }
}

[System.Serializable]
public class Power_up_UI
{
    [Header("플레이어 탄알 공격력 증가")]
    // 공격력 증가 UI
    public GameObject original_bullet_power_up_obj;
    public GameObject timer_bullet_power_up_obj;
    public Text bullet_power_up_txt;

    // 공격 속도 증가 UI
    [Header("플레이어 탄알 공격 속도 증가")]
    public GameObject original_bullet_speed_up_obj;
    public GameObject timer_bullet_speed_up_obj;
    public Text bullet_speed_up_txt;

    // 미사일 UI
    [Header("플레이어 유도 미사일 생성")]
    public GameObject original_missile_obj;
    public GameObject timer_missile_obj;
    public Text missile_power_up_txt;

    // 보호막 UI
    [Header("플레이어 보호막 생성")]
    public GameObject original_shield_obj;
    public GameObject timer_shield_obj;
    public Text shield_power_up_txt;
}