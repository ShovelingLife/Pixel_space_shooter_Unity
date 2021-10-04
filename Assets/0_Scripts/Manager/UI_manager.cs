using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_manager : Singleton_local<UI_manager>
{
    [Header("파워 업 UI 데이터")]
    public Power_up_UI_data power_up_UI_data;

    [Header("파워 업 UI")]
    public Bullet_power_up_UI bullet_power_up_ui;
    public Bullet_speed_up_UI bullet_speed_up_ui;
    public Missile_power_up_UI missile_power_up_ui;
    public Shield_power_up_UI shield_power_up_ui;

    [Header("안내 메시지")]
    public GameObject game_start_text_obj;
    Text              mtxt_game_start;
    float             m_current_time_count;
    float             m_timer = 0.5f;
    int               m_display_count = 0;
    bool              m_display_alert_text;
    bool              m_is_text_fading;

    [Header("플레이어")]
    public Text    player_score_txt;
    public Image   player_hp_img;
    public int     current_score = 0;


    void Update()
    {
        Show_alert_text();
        Show_player_info();
    }

    // 변수 초기화
    public void Init()
    {
        mtxt_game_start      = game_start_text_obj.GetComponent<Text>();
        bullet_power_up_ui  = GetComponent<Bullet_power_up_UI>();
        bullet_speed_up_ui  = GetComponent<Bullet_speed_up_UI>();
        shield_power_up_ui  = GetComponent<Shield_power_up_UI>();
        missile_power_up_ui = GetComponent<Missile_power_up_UI>();
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
                    mtxt_game_start.color = Global.original_color;
                    m_is_text_fading = true;
                }
                else // 사라지고 있으면
                {
                    mtxt_game_start.color = Global.sprite_fade_color;
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
        player_hp_img.fillAmount          = Player_manager.instance.current_hp_prop / player_stat_data.max_hp;

        // 파워업 레벨
        power_up_UI_data.bullet_power_up_txt.text  = Stat_manager.instance.player_power_up_stat.power_up_level.ToString();
        power_up_UI_data.bullet_speed_up_txt.text  = Stat_manager.instance.player_power_up_stat.speed_up_level.ToString();
        power_up_UI_data.missile_power_up_txt.text = Stat_manager.instance.player_power_up_stat.missile_level.ToString();
        power_up_UI_data.shield_power_up_txt.text  = Stat_manager.instance.player_power_up_stat.shield_level.ToString();
    }

    // 안내 메시지 메시지 바꿈
    public void Set_alert_text(e_level_type _current_level)
    {
        Dictionary<e_level_type, string> d_level = new Dictionary<e_level_type, string>()
        {
            {e_level_type.END,"게임 종료" },        {e_level_type.FIRST,"첫번째 스테이지" },{e_level_type.SECOND,"두번째 스테이지" },
            {e_level_type.THIRD,"세번째 스테이지" },{e_level_type.FOURTH,"네번째 스테이지" },{e_level_type.FIFTH,"다섯번째 스테이지" }
        };
        mtxt_game_start.text = d_level[_current_level];
        m_display_alert_text = true;
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