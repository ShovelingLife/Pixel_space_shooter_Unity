using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines the core of each enemy
public class Enemy_core : MonoBehaviour
{
    // Position variables
    public Enemy_path path;

    // Plane information variables
    protected Low_enemy_stat_data m_low_enemy_stat_data;
    protected Transform           m_bullet_pos;
    protected float               m_hp;
    public float current_hp
    {
        get { return m_hp; }
        set { m_hp = value; }
    }

    // Plane effects variables
    public    Sprite[]       a_plane_sprite;
    protected GameObject     m_explosion_effect_obj;
    protected SpriteRenderer m_current_sprite_rend;

    // Another variables
    protected float          m_current_time;
    protected float          m_timer;
    protected int            m_max_bullet_count;
    protected bool           m_is_first_time;
    protected bool           m_play_sound = true;
    public    bool           is_dead;
    public    bool           is_test;


    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
        Reset_after_dead();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        bool is_sound_on = false;

        if (!is_dead)
        {
            if (other.gameObject.tag == "Shield")
            {
                Player_manager.instance.Character_dmg_dealed("enemy_collision");
                is_sound_on = true;
            }
            if (other.gameObject.tag == "Player_bullet")
            {
                is_dead = true;
                is_sound_on = true;
            }
            if (other.gameObject.tag == "Player")
            {
                if (Player_manager.instance.GetComponent<SpriteRenderer>().color.a != 0)
                {
                    Player_manager.instance.current_hp_prop -= Stat_manager.instance.enemy_stat_data.collision_dmg;
                    Player_manager.instance.Character_dmg_dealed("enemy_collision");
                    is_sound_on = true;
                }
            }
            if (is_sound_on)
                StartCoroutine(IE_enemy_death());
        }
    }

    // Resources initialization
    protected virtual void Init_resources()
    {
        // 이펙트 초기화
        m_explosion_effect_obj = gameObject.transform.GetChild(1).gameObject;
        m_current_sprite_rend  = GetComponent<SpriteRenderer>();
    }

    // Initialization of waypoint
    protected virtual void Init_enemy_way_point()
    {

    }

    // 죽은 후 재설정
    protected virtual void Reset_after_dead()
    {
        m_is_first_time       = false;
        m_play_sound          = true;
        is_dead               = false;
        StopCoroutine(IE_enemy_death());

        if (GetComponent<Enemy_core>()) // Not null
            Enemy_info_manager.instance.Delete_enemy_info(GetComponent<Enemy_core>());
    }

    // Play sound
    protected void Play_sound()
    {
        if (m_play_sound)
            Audio_manager.instance.enemy_sound.Play_enemy_spawn_sound();
    }

    // 비행기 기울이기
    protected void Enemy_inclining(e_plane_state _plane_state)
    {
        switch (_plane_state)
        {
            case e_plane_state.RIGHT: // 오른쪽
                this.transform.rotation = Global.half_rotation;
                m_current_sprite_rend.sprite = a_plane_sprite[2];
                break;

            case e_plane_state.LEFT: // 왼쪽
                this.transform.rotation = Global.zero_rotation;
                m_current_sprite_rend.sprite = a_plane_sprite[2];
                break;

            case e_plane_state.IDLE: // 정지 상태
                this.transform.rotation = Global.zero_rotation;
                m_current_sprite_rend.sprite = a_plane_sprite[0];
                break;
        }
    }

    // 죽음 코루틴
    protected virtual IEnumerator IE_enemy_death()
    {
        yield return null;
    }
}