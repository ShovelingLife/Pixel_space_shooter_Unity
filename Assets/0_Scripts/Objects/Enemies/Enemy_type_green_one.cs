using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Enemy_core))]
public class Enemy_type_green_one : Enemy_core
{
    void Start()
    {
        base.Init_resources();
        Init_resources();
        Enemy_info_manager.instance.Set_first_enemy_info(this);
    }

    void Update()
    {
        if (is_test) // Do not activate when test
            return;

        if (!is_dead) // Not dead
            Enemy_action();

        else // Dead
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (this.transform.localPosition == m_target_pos_arr[3])
            gameObject.SetActive(false);
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
        Reset_after_dead();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    // 변수 초기화
    protected override void Init_resources()
    {
        // 정보 초기화
        m_target_pos_arr       = new Vector3[4];
        m_timer                = 0.75f;
        m_max_bullet_count     = 2;
        m_current_point_index  = 0;
        m_is_first_time        = false;
        m_low_enemy_stat_data  = Stat_manager.instance.enemy_stat_data;
        m_hp                   = 1f;

        // 위치 초기화
        int spawn_pos_arr_size = Spawn_manager.instance.monster_spawn_pos.spawn_pos_list.Count;
        m_bullet_pos           = gameObject.transform.GetChild(0).gameObject.transform;
        m_spawn_pos_arr        = new Vector3[spawn_pos_arr_size];

        for (int i = 0; i < spawn_pos_arr_size; i++)
        {
            m_spawn_pos_arr[i] = Spawn_manager.instance.monster_spawn_pos.spawn_pos_list[i];
        }
    }

    // 적 웨이포인트 초기화
    protected override void Init_enemy_way_point()
    {
        Vector3 current_pos = this.transform.localPosition;
        //    ㅇ<2    ㅇ<3
        //  ㅇ<1        ㅇ<4

        // 첫번째 지점 
        if (current_pos == m_spawn_pos_arr[0])
        {
            m_target_pos_arr[0] = new Vector3(-2f, 10f);
            m_target_pos_arr[1] = new Vector3(-5f, 1f);
            m_target_pos_arr[2] = new Vector3(-9.4f, 7f);
            m_target_pos_arr[3] = new Vector3(-1.8f, 19f);
        }
        // 두번째 지점 
        if (current_pos == m_spawn_pos_arr[1])
        {

        }
        // 세번째 지점 
        if (current_pos == m_spawn_pos_arr[2])
        {

        }
        // 네번째 지점 
        if (current_pos == m_spawn_pos_arr[3])
        {

        }
    }

    // 적 움직임
    void Enemy_action()
    {
        if (m_current_point_index > 3) 
            return;

        Enemy_info_manager.instance.Set_first_enemy_info(this);

        Vector3 current_pos = this.transform.localPosition;
        Init_enemy_way_point();

        if (!m_is_first_time)
        {
            Play_sound();
            m_play_sound = false;
            this.transform.DOLocalMove(m_target_pos_arr[m_current_point_index], m_low_enemy_stat_data.move_speed);
            Enemy_inclining(e_plane_state.RIGHT);

            if (current_pos.x > -6f)
            {
                StartCoroutine(IE_enemy_shoot_bullet());
                m_is_first_time = true;
            }
        }
        if (current_pos == m_target_pos_arr[m_current_point_index])
        {
            Enemy_inclining(e_plane_state.IDLE);
            m_current_time += Time.deltaTime;

            if (m_current_time > m_timer)
            {
                m_current_point_index++;
                Enemy_move(m_target_pos_arr[m_current_point_index]);
                m_current_time = 0f;
            }
        }
    }

    // 적 이동
    void Enemy_move(Vector3 _target_pos)
    {
        if (_target_pos.x > 0) // Moving right
            Enemy_inclining(e_plane_state.RIGHT);

        else // Moving left
            Enemy_inclining(e_plane_state.LEFT);

        this.transform.DOLocalMove(_target_pos, m_low_enemy_stat_data.move_speed);
        StartCoroutine(IE_enemy_shoot_bullet());
    }
   
    // 총알 발사 코루틴
    IEnumerator IE_enemy_shoot_bullet()
    {
        for (int i = 0; i < m_max_bullet_count; i++)
        {
            GameObject tmp_obj = Pooling_manager.instance.Get_obj(e_pooling_obj_type.ENEMY_SMALL_BULLET);

            if (tmp_obj != null)
            {
                tmp_obj.transform.position = m_bullet_pos.position;
                tmp_obj.SetActive(true);
                Audio_manager.instance.enemy_sound.Play_enemy_small_laser_sound();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    protected override IEnumerator IE_enemy_death()
    {
        float duration = 0.5f;

        // Bullet touched
        Audio_manager.instance.enemy_sound.Play_enemy_death_sound();
        m_explosion_effect_obj.SetActive(true);
        yield return new WaitForSeconds(duration);
        m_current_sprite_rend.color = Global.sprite_fade_color;
        yield return new WaitForSeconds(duration);
        m_explosion_effect_obj.SetActive(false);
        gameObject.SetActive(false);
    }
}