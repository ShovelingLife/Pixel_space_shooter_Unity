using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Enemy_core))]
public class Enemy_type_green_one : Enemy_core
{
    void Awake()
    {
        base.Init();
        Init();
    }

    void Update()
    {
        if (!is_dead) // Not dead
            Enemy_action();

        else // Dead
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        //if (this.transform.localPosition == m_target_pos_arr[3])
        //    gameObject.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!is_dead)
        {
            base.OnTriggerEnter2D(other);

            if (other.gameObject.tag == "Player_bullet")
                UI_manager.instance.current_score += 10;            
        }
    }

    protected override void OnEnable()
    {
        m_hp = 1f;
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
        m_timer                = 0.75f;
        m_max_bullet_count     = 2;
        m_low_enemy_stat_data  = Stat_manager.instance.enemy_stat_data;
        m_hp                   = 1f;
        m_speed = 0.35f;

        // 위치 초기화
        m_bullet_pos           = gameObject.transform.GetChild(0).gameObject.transform;
    }

    protected override IEnumerator IE_enemy_death()
    {
        float duration = 0.5f;

        // Bullet touched
        Audio_manager.instance.enemy_sound.Play_enemy_death_sound();
        m_explosion_effect_obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        GetComponent<SpriteRenderer>().color = Global.sprite_fade_color;
        yield return new WaitForSeconds(duration);
        m_explosion_effect_obj.SetActive(false);
        gameObject.SetActive(false);
        base.Reset_after_dead();
    }

    // 적 움직임
    void Enemy_action()
    {
        Enemy_info_manager.instance.Set_first_enemy_info(this);
        Vector3 current_pos = this.transform.localPosition;

        if (is_ready)
        {
            Play_sound();
            m_play_sound = false;

            m_range += (m_speed * Time.deltaTime);

            switch (waypoint)
            {
                case e_enemy_waypoint.FIRST: transform.position = path.Get_first_path(m_range); break;

                case e_enemy_waypoint.SECOND: transform.position = path.Get_second_path(m_range); break;

                case e_enemy_waypoint.THIRD:
                    Reset_after_dead();
                    waypoint = 0;
                    break;
            }
            if (m_range > 1f)
            {
                m_range = 0f;
                waypoint++;
            }
        }
    }

    // 적 이동
    void Enemy_move(Vector3 _target_pos)
    {
        this.transform.DOLocalMove(_target_pos, m_low_enemy_stat_data.move_speed);
    }
   
    // 총알 발사 코루틴
    public override IEnumerator IE_enemy_shoot_bullet()
    {
        for (int i = 0; i < m_max_bullet_count; i++)
        {
            GameObject tmp_obj = Pooling_manager.instance.Get_obj(typeof(Enemy_small_bullet));

            if (tmp_obj != null)
            {
                tmp_obj.transform.position = m_bullet_pos.position;
                tmp_obj.SetActive(true);
                Audio_manager.instance.enemy_sound.Play_enemy_small_laser_sound();
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    // 스프라잇 변경
    public void Change_sprite(e_plane_state _state)
    {
        GetComponent<SpriteRenderer>().sprite = a_plane_sprite[(int)_state];
    }

    public override void Enemy_inclining(e_plane_state _state)
    {
        base.Enemy_inclining(_state);
    }
}