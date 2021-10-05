using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines the core of each enemy
public class Enemy_core : MonoBehaviour
{
    // Position variables
    public Enemy_path path;
    public e_enemy_waypoint waypoint;
    public float      spawn_time;
    protected float m_speed;
    protected float m_range;

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

    // Another variables
    protected float          m_current_time;
    protected float          m_timer;
    protected int            m_max_bullet_count;
    protected bool           m_play_sound = true;
    public    bool           is_dead;
    public    bool           is_ready;
    public    bool           is_activated = false;


    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
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
            else if (other.gameObject.tag == "Player_bullet")
            {
                is_dead = true;
                is_sound_on = true;
            }
            else if (other.gameObject.tag == "Player")
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
    protected virtual void Init()
    {
        // 이펙트 초기화
        m_explosion_effect_obj = gameObject.transform.GetChild(1).gameObject;
    }

    // 죽은 후 재설정
    protected virtual void Reset_after_dead()
    {
        m_play_sound            = true;
        is_dead                 = false;
        is_ready                = false;
        transform.localPosition = new Vector3(1000f, 1000f);
        StopCoroutine(IE_enemy_death());

        Enemy_info_manager.instance.Delete_enemy_info(this);
        gameObject.SetActive(false);
    }

    // Play sound
    protected void Play_sound()
    {
        if (m_play_sound)
            Audio_manager.instance.enemy_sound.Play_enemy_spawn_sound();
    }

    // 비행기 기울이기
    public virtual void Enemy_inclining(e_plane_state _plane_state)
    {
        Quaternion[] a_tmp_quaternion = new Quaternion[]
        {
            Global.zero_rotation,
            Global.half_rotation,
            Global.zero_rotation
        }; 
        transform.rotation           = a_tmp_quaternion[(int)_plane_state];
        GetComponent<SpriteRenderer>().sprite = a_plane_sprite[(int)_plane_state];
    }

    // 죽음 코루틴
    protected virtual IEnumerator IE_enemy_death()
    {
        yield return null;
    }

    public virtual IEnumerator IE_enemy_shoot_bullet()
    {
        yield return null;
    }
}