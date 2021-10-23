using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_pattern_core : MonoBehaviour
{
    // 무기 관련
    public    Boss_enemy_core boss_enemy_core;
    protected Vector3         m_bullet_pos;
    protected float           m_current_reloading_time = 0f;
    protected float           m_reload_time            = 0f;
    protected float           m_current_move_time      = 0f;
    protected float           m_move_time              = 0f;

    // 패턴 관련
    protected Sequence        m_sequence;
    protected float           m_current_pattern_time;
    protected float           m_max_pattern_time;
    protected bool            m_is_done = false;


    protected virtual void Update()
    {
        m_current_pattern_time += Time.deltaTime;

        if(m_current_pattern_time >= m_max_pattern_time)
        {
            m_current_pattern_time = 0f;
            m_is_done = true;
        }
    }

    public virtual IEnumerator IE_run_pattern()
    {
        while (!m_is_done)
        {
            // 공격 후 일정 시간 대기
            Init();
            Attack();
            yield return new WaitForSeconds(m_reload_time);

            m_current_move_time += Time.deltaTime;

            // 움직일 시간이 됐다면 움직임
            if (m_current_move_time >= m_move_time)
            {
                Move();
                m_current_move_time = 0f;
            }
        }
        boss_enemy_core.End_pattern();
    }

    // 값 초기화
    public virtual void Init()
    {

    }

    // 공격
    protected virtual void Attack()
    {
        
    }

    // 움직임
    protected virtual void Move()
    {

    }
}