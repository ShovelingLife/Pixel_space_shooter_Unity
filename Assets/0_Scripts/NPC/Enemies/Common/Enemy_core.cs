using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines the core of each enemy
public class Enemy_core : MonoBehaviour
{

    public int Index = 0;

    // Position variables
    public    Enemy_path       path;
    public    e_enemy_waypoint waypoint;
    public    float            spawn_time;
    protected float            m_speed;
    protected float            m_range;

    // Plane information variables
    protected Low_enemy_stat_data m_low_enemy_stat_data;
    protected Transform           m_bullet_pos;
    protected float               m_hp;
    protected float               m_current_shoot_time = 0f;
    public    float               bullet_shoot_time = 0f;

    public float current_hp
    {
        get { return m_hp; }
        set { m_hp = value; }
    }

    // Plane effects variables
    public    Sprite[]       arr_plane_sprite;
    protected GameObject     m_explosion_effect_obj;

    // Another variables
    protected float          m_current_time;
    protected float          m_timer;
    protected int            m_max_bullet_count;
    protected bool           m_play_sound = true;
    public    bool           is_dead;
    public    bool           is_ready;
    public    bool           is_activated = false;


    protected virtual void Update()
    {
        //Enemy_attack();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        bool   is_sound_on  = false;
        string collided_tag = other.gameObject.tag;

        if (!is_dead)
        {
            if (collided_tag == "Shield")
            {
                Player_manager.instance.Character_dmg_dealed("enemy_collision");
                is_sound_on = true;
                current_hp = 0f;
            }
            else if (collided_tag == "Player_bullet")
            {
                is_dead = true;
                is_sound_on = true;

                // 미사일로부터 공격받음
                Player_missile player_missile = other.GetComponent<Player_missile>();

                if (player_missile)
                {
                    player_missile.GetComponent<BoxCollider2D>().enabled = false;
                    current_hp -= Stat_manager.instance.player_power_up_stat.missile_power_up_data.missile_dmg;
                    player_missile.gameObject.SetActive(false);
                }

                else
                    current_hp -= Player_manager.instance.player_bullet_data.bullet_dmg;
            }
            else if (collided_tag == "Player")
            {
                if (Player_manager.instance.GetComponent<SpriteRenderer>().color.a != 0)
                {
                    Player_manager.instance.current_hp_prop -= Stat_manager.instance.enemy_stat_data.collision_dmg;
                    Player_manager.instance.Character_dmg_dealed("enemy_collision");
                    is_sound_on = true;
                    current_hp = 0f;
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
        StopAllCoroutines();

        Enemy_info_manager.instance.Delete_enemy_info(this);
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
        GetComponent<SpriteRenderer>().sprite = arr_plane_sprite[(int)_plane_state];
    }

    // 적이 공격함
    IEnumerator IE_enemy_attack()
    {
        while (true)
        {
            Enemy_info_manager inst = Enemy_info_manager.instance;
            Transform tmp_trans = Object_pooling_manager.instance.Create_obj(typeof(Enemy_small_bullet), inst.small_bullet_obj.transform, inst.small_bullet_obj_container);
            tmp_trans.position = m_bullet_pos.position;
            Audio_manager.instance.enemy_sound.Play_enemy_small_laser_sound();
            m_current_shoot_time = 0f;
            yield return new WaitForSeconds(bullet_shoot_time);
        }
    }

    // 죽음 코루틴
    protected virtual IEnumerator IE_enemy_death()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
    }

    // 공격 코루틴 실행
    public void Run_attack_coroutine()
    {
        StartCoroutine(IE_enemy_attack());
    }
}