using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Boss_enemy_core : MonoBehaviour
{
    public GameObject  bullet_obj;
    public Transform   bullet_container;
    public Transform[] arr_bullet_pos;

    // 보스 패턴 관련
    protected Boss_pattern_core[] m_arr_boss_pattern = new Boss_pattern_core[]
       {
        new Boss_pattern1(),
        new Boss_pattern2(),
        new Boss_pattern3(),
        new Boss_pattern4(),
        new Boss_pattern5()
       };
    protected e_boss_pattern_type m_pattern_type; 
              Boss_pattern_core   m_boss_pattern_core = null;
              bool                m_is_ready = true;

    // hp바 관련
    protected Image hp_bar_image;
    protected float m_hp = 1f;
              float m_current_hp_bar_time;
              float m_max_hp_bar_time = 2.5f;

    // 현재 hp
    public float current_hp
    {
        get { return m_hp; }
        set { m_hp = value; }
    }

    void Start()
    {
        hp_bar_image     = UI_manager.instance.boss_hp_obj.transform.GetChild(1).GetComponent<Image>();
        bullet_obj       = transform.GetChild(0).gameObject;
        bullet_container = transform.GetChild(1).transform;
    }

    void Update()
    {
        // HP바 
        Check_hp_bar();

        if (m_is_ready)
        {
            Run_pattern();
            m_is_ready = false;
        }
        Log_screen_manager.instance.Insert_log(m_pattern_type.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 보스몬스터 hp바 표시
        if (collision.name == "Player" ||
            collision.tag == "Player_bullet")
        {
            m_current_hp_bar_time = 0f;

            // 피격시 체력 소모 
            m_hp -= 0.05f;
            hp_bar_image.fillAmount = m_hp;
            UI_manager.instance.boss_hp_obj.SetActive(true);

            // 플레이어 총알일 시
            Player_bullet bullet = collision.GetComponent<Player_bullet>();
            
            if (bullet)
                Object_pooling_manager.instance.Remove_obj(bullet.GetType(), bullet.transform);

            // 사망
            if (m_hp <= 0f)
                Dead();
        }
    }

    // HP바 감시
    void Check_hp_bar()
    {
        m_current_hp_bar_time += Time.deltaTime;

        if (m_current_hp_bar_time > m_max_hp_bar_time)
        {
            m_current_hp_bar_time = 0f;
            UI_manager.instance.boss_hp_obj.SetActive(false);
        }
    }

    // 패턴 실행
    void Run_pattern()
    {
        // 컴포넌트 추가후 실행
        Type pattern_type = m_arr_boss_pattern[(int)m_pattern_type].GetType();
        gameObject.AddComponent(pattern_type);
        m_boss_pattern_core = gameObject.GetComponent<Boss_pattern_core>();
        m_boss_pattern_core.boss_enemy_core = this;
        StartCoroutine(m_boss_pattern_core.IE_run_pattern());
    }

    // 패턴 끝나고 다음 패턴 준비
    public void End_pattern()
    {
        // 패턴 종료
        if (m_pattern_type == e_boss_pattern_type.MAX)
            return;

        m_is_ready = true;
        m_pattern_type++;
    }

    // 죽으면 안내 후 hp바 제거
    void Dead()
    {
        UI_manager.instance.Set_alert_text(e_level_type.END);
        UI_manager.instance.boss_hp_obj.SetActive(false);
        Destroy(gameObject);
    }
}