using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 키 입력 함수들을 들고있는 클래스
[Serializable]
public class Player_key
{
    public Dictionary<KeyCode, Action> dic_single_key_fn =      new Dictionary<KeyCode, Action>();
    public Dictionary<KeyCode, Action> dic_continuous_key_fn =  new Dictionary<KeyCode, Action>();
    public Dictionary<KeyCode, Action> dic_single_key_up_fn =   new Dictionary<KeyCode, Action>();
}

// 플레이어 풀링 데이터 들고있음
[Serializable]
public class Player_pooling_data
{
    [Header("플레이어 총알")]
    public Transform  trans_bullet_obj_container;
    public GameObject bullet_obj;

    [Header("플레이어 미사일")]
    public Transform  trans_missile_obj_container;
    public GameObject missile_obj;
}

public class Player_manager : Singleton_local<Player_manager>
{
    // 플레이어 관련
    [Header("플레이어 관련")]
    public Sprite[]            arr_player_sprite = new Sprite[3];
    SpriteRenderer             m_current_sprite_renderer;
    Vector2                    player_move_pos;
    bool                       m_is_player_movable;
    public Player_pooling_data player_pooling_data;
    public Health_restore_data health_restore_data;

    // 플레이어 입력 관련
    [Header("플레이어 키 입력 관련")]
    public Player_key player_key;
    //Animator player_animator;

    // 플레이어 스텟 관련
    [Header("플레이어 파워업 정보 관련")]
    public Player_stat_data     player_stat_data;
    Player_power_up_stat        m_player_power_up_stat;
    float                       m_current_hp;
    public float current_hp_prop
    {
        get { return m_current_hp; }
        set { m_current_hp = value; }
    }
    // 플레이어 충돌 관련
    RaycastHit2D m_player_hit;
    Vector2      m_ray_direction;
    GameObject   m_player_death_anim_obj;
    bool         is_player_dead;

    // 플레이어 총알 관련
    [Header("플레이어 총알 관련")]
    public Player_bullet_data player_bullet_data;
    public Transform          trans_bullet_pos;

    // UI 관련
    float m_current_time_count;
    float m_timer = 2f;
    bool  m_is_first_time = true;
    bool  m_is_on_pause;


    void Start()
    {
        Init_player_settings();
        Init_player_input_key_settings();
    }

    void Update()
    {
        player_bullet_data.bullet_timer += Time.deltaTime;
        // 처음이고 캐릭터가 안움직일시
        First_time();

        if (!is_player_dead )
        {
            // Moved by mouse
            //Move_player_by_mouse();

            // Moved by keyboard
            Player_input_single_key_up();
            Player_input_single_key();
            Player_input_continuous_key();

            // Power up
            Create_shield();
            Create_missile(m_player_power_up_stat.missile_level);

            // Exception
            Check_player_in_screen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (m_player_power_up_stat.is_shield_created)
        {
            if (other.gameObject.tag == "Enemy_bullet"|| 
                other.gameObject.tag == "Enemy") 
                Audio_manager.instance.player_sound.Play_shield_receive_dmg_sound();
        }
    }

    // 플레이어 이탈 방지
    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3 tmp_pos = transform.position;

        if(other.gameObject.tag == "Wall")
        {
            if (transform.position.y >= 17.5f)
                tmp_pos.y-= 0.1f;
        }
        transform.position = tmp_pos;
    }

    private void OnEnable()
    {
        
    }

    // 드래그 시작할시
    private void OnMouseDrag()
    {
        Debug.Log("Is_dragging");
    }

    // 캐릭터 초기화
    void Init_player_settings()
    {
        m_player_power_up_stat            = Stat_manager.instance.player_power_up_stat;
        m_current_sprite_renderer         = GetComponent<SpriteRenderer>();
        m_player_death_anim_obj             = this.gameObject.transform.GetChild(1).gameObject;
        m_player_power_up_stat.shield_obj = this.gameObject.transform.GetChild(2).gameObject;
        player_stat_data                  = Stat_manager.instance.player_stat_data;
        m_current_hp                      = player_stat_data.max_hp;
    }

    void First_time()
    {
        if (m_is_first_time)
        {
            m_current_time_count += Time.deltaTime;

            if (m_current_time_count > m_timer)
            {
                m_current_time_count = 0f;
                m_is_first_time = false;
                m_is_player_movable = true;
            }
        }
    }

    // 플레이어 키 입력 관련 초기화
    void Init_player_input_key_settings()
    {
        // 키를 한 번 눌렀을 시
        player_key.dic_single_key_fn[KeyCode.P]              = Show_pause_menu; // P키 (정지/재개)

        // 키를 연속적으로 눌렀을 시
        player_key.dic_continuous_key_fn[KeyCode.UpArrow]    = Player_moving_up;    // 윗키
        player_key.dic_continuous_key_fn[KeyCode.LeftArrow]  = Player_moving_left;  // 왼쪽키
        player_key.dic_continuous_key_fn[KeyCode.DownArrow]  = Player_moving_down;  // 아랫키
        player_key.dic_continuous_key_fn[KeyCode.RightArrow] = Player_moving_right; // 오른쪽키
        player_key.dic_continuous_key_fn[KeyCode.Space]      = Player_shoot_bullet; // 발사키

        // 키를 눌렀다 땠을 시
        player_key.dic_single_key_up_fn[KeyCode.UpArrow]     = Player_stopped_moving; // 윗키
        player_key.dic_single_key_up_fn[KeyCode.LeftArrow]   = Player_stopped_moving; // 왼쪽키
        player_key.dic_single_key_up_fn[KeyCode.DownArrow]   = Player_stopped_moving; // 아랫키
        player_key.dic_single_key_up_fn[KeyCode.RightArrow]  = Player_stopped_moving; // 오른쪽키
    }

    // 일시정지 혹 재개 P키
    void Show_pause_menu()
    {
        if (!m_is_on_pause)
        {
            Pause_game();
            m_is_on_pause = true;
        }
        else
        {
            Continue_game();
            m_is_on_pause = false;
        }
    }

    // 일시정지 하기
    void Pause_game()
    {
        Time.timeScale = 0f;
    }

    // 재개 하기
    void Continue_game()
    {
        Time.timeScale = 1f;
    }

    // 플레이어 윗쪽 방향키 (움직임)
    void Player_moving_up()
    {
        player_move_pos.y += player_stat_data.move_speed * Time.deltaTime;
        m_ray_direction = Vector2.up;
    }

    // 플레이어 아랫쪽 방향키 (움직임)
    void Player_moving_down()
    {
        player_move_pos.y -= player_stat_data.move_speed * Time.deltaTime;
        m_ray_direction = Vector2.down;
    }

    // 플레이어 왼쪽 방향키 (움직임)
    void Player_moving_left()
    {
        m_current_sprite_renderer.sprite = arr_player_sprite[2]; // 기울이기
        player_move_pos.x -= player_stat_data.move_speed * Time.deltaTime;
        m_ray_direction = Vector2.left;
    }

    // 플레이어 오른쪽 방향키 (움직임)
    void Player_moving_right()
    {
        m_current_sprite_renderer.sprite = arr_player_sprite[2]; // 기울이기
        player_move_pos.x += player_stat_data.move_speed * Time.deltaTime;
        this.transform.rotation = Global.half_rotation;
        m_ray_direction = Vector2.right;
    }

    // 플레이어 전 방향키 땠을 시 (멈춤)
    void Player_stopped_moving()
    {
        this.transform.rotation = Global.zero_rotation;
        m_current_sprite_renderer.sprite = arr_player_sprite[0];
    }

    // Check if player gets out from screen
    void Check_player_in_screen()
    {
        Vector2 tmp_pos = this.transform.position;

        if (transform.position.y <= -15.6f)
            tmp_pos.y += 0.01f;

        this.transform.position = tmp_pos;
    }
    
    // 캐릭터 이동 모션
    void Player_moving_motion(Vector3 _moving_pos)
    {
        // 왼쪽
        if (_moving_pos.x < 0)
        {
            transform.rotation = Global.zero_rotation;
            m_current_sprite_renderer.sprite = arr_player_sprite[2];
        }
        else
        {
            transform.rotation = Global.half_rotation;
            m_current_sprite_renderer.sprite = arr_player_sprite[2];
        }
    }

    // 캐릭터 마우스로 이동
    void Move_player_by_mouse()
    {
        if (!m_is_player_movable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_is_player_movable = true;
                m_is_first_time = false;
            }
        }
        else
        {
            Vector3 move_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            move_pos.z = 0f;
            Player_moving_motion(move_pos);
            this.transform.position = move_pos;
            Player_shoot_bullet();
        }
    }

    // 플레이어가 키를 단일적으로 입력시
    void Player_input_single_key()
    {
        foreach (var input_key in player_key.dic_single_key_fn)
        {
            if (Input.GetKeyDown(input_key.Key)) 
                input_key.Value.Invoke();
        }
    }

    // 플레이어가 키를 연속적으로 입력시
    void Player_input_continuous_key()
    {
        int layer_mask  = Global.Get_raycast_layermask_index("Wall");
        player_move_pos = this.transform.position;

        foreach (var input_key in player_key.dic_continuous_key_fn)
        {
            if (Input.GetKey(input_key.Key))
            {
                input_key.Value.Invoke();

                if (m_is_first_time)
                {
                    m_is_first_time     = false;
                    m_is_player_movable = true;
                }
            }
        }
        // 이동 제한
        m_player_hit = Physics2D.Raycast(transform.position, m_ray_direction, 0.5f, layer_mask);

        if (m_player_hit.collider != null) 
            return;

        this.transform.position = player_move_pos;
    }

    // 플레이어가 키를 눌렀다가 땠을 시
    void Player_input_single_key_up()
    {
        foreach (var key_input in player_key.dic_single_key_up_fn)
        {
            if (Input.GetKeyUp(key_input.Key)) 
                key_input.Value.Invoke();
        }
    }

    // 캐릭터 총알 발사
    void Player_shoot_bullet()
    {
        float bullet_reload_time = Activate_player_bullet_booster(m_player_power_up_stat.is_booster_on);

        if (player_bullet_data.bullet_timer > bullet_reload_time)
        {
            Activate_player_power_up(m_player_power_up_stat.power_up_level);
            player_bullet_data.bullet_timer = 0f;
        }
    }

    // 보호막 파워 업 관련
    void Create_shield()
    {
        if (m_player_power_up_stat.is_shield_created)
            m_player_power_up_stat.shield_obj.SetActive(true);

        else
            m_player_power_up_stat.shield_obj.SetActive(false);
    }

    // 미사일 생성
    void Create_missile(int _missile_level)
    {
        if (_missile_level == 0) 
            return;

        m_player_power_up_stat.missile_current_time += Time.deltaTime;

        if (m_player_power_up_stat.missile_current_time > m_player_power_up_stat.missile_reload_time)
        {
            GameObject[] missile_obj_arr = new GameObject[player_stat_data.max_missile_level];
            Audio_manager.instance.player_sound.Play_player_missile_sound();

            for (int i = 0; i < _missile_level; i++)
            {
                if (i == player_stat_data.max_missile_level) 
                    return;

                missile_obj_arr[i] = Object_pooling_manager.instance.Create_obj(typeof(Player_missile), player_pooling_data.missile_obj.transform, player_pooling_data.trans_missile_obj_container).gameObject;
                Player_missile player_missile = missile_obj_arr[i].GetComponent<Player_missile>();

                if (missile_obj_arr[i] != null)
                {
                    if (i == 0) // 왼쪽 방향
                    {
                        missile_obj_arr[i].transform.position = m_player_power_up_stat.missile_first_level_trans.position;
                        player_missile.current_direction      = "Left_direction";
                    }
                    if (i == 1) // 오른쪽 방향    
                    {
                        missile_obj_arr[i].transform.position = m_player_power_up_stat.missile_second_level_trans.position;
                        player_missile.current_direction      = "Right_direction";
                    }
                }
            }
            m_player_power_up_stat.missile_current_time = 0f;
        }
    }

    // 캐릭터 부스터 활성화
    float Activate_player_bullet_booster(bool _is_booster_on)
    {
        float bullet_reload_time = 0;

        if (_is_booster_on) 
            bullet_reload_time = player_bullet_data.bullet_reload_time / m_player_power_up_stat.bullet_speed_up_data.increase_speed;

        else 
            bullet_reload_time = player_bullet_data.bullet_reload_time;

        return bullet_reload_time;
    }

    // 캐릭터 공격력 증가 활성화
    void Activate_player_power_up(int _power_up_level)
    {
        GameObject[] bullet_obj_arr = new GameObject[player_stat_data.max_power_up_level];
        Audio_manager.instance.player_sound.Play_player_laser_sound();

        for (int i = 0; i < _power_up_level+1; i++)
        {
            if (i == player_stat_data.max_power_up_level) 
                return;

            bullet_obj_arr[i] = Object_pooling_manager.instance.Create_obj(typeof(Player_bullet), player_pooling_data.bullet_obj.transform, player_pooling_data.trans_bullet_obj_container).gameObject;

            if (bullet_obj_arr[i] != null)
            {
                if      (i == 0) 
                         bullet_obj_arr[i].transform.position = trans_bullet_pos.position;

                else if (i == 1) 
                         bullet_obj_arr[i].transform.position = m_player_power_up_stat.second_bullet_trans.position;

                else if (i == 2) 
                         bullet_obj_arr[i].transform.position = m_player_power_up_stat.third_bullet_trans.position;
            }
        }
    }
    
    // 캐릭터 죽음 관련
    public void Character_dmg_dealed(string _death_type)
    {
        // 적 발사체랑 충돌
        if (_death_type == "enemy_bullet")
            Audio_manager.instance.player_sound.Play_bullet_death_sound();

        // 적이랑 충돌
        if (_death_type == "enemy_collision") 
            Audio_manager.instance.player_sound.Play_collision_death_sound();

        this.transform.rotation = Global.zero_rotation;
        m_current_sprite_renderer.sprite = arr_player_sprite[0];

        if (current_hp_prop <= 0) 
            StartCoroutine(IE_player_death_anim()); // 죽음
    }

    // 캐릭터 죽음 애니메이션 코루틴
    IEnumerator IE_player_death_anim()
    {
        m_player_death_anim_obj.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_player_death_anim_obj.SetActive(false);        
        gameObject.GetComponent<SpriteRenderer>().color = Global.sprite_fade_color;
        is_player_dead = true;
        UI_manager.instance.Set_alert_text(e_level_type.END);
    }
}