using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_missile : MonoBehaviour
{
    // ------- Missile variables -------
    Player_missile_attack m_missile_attack;
    Player_missile_idle   m_missile_idle;
    Vector3               m_current_pos;
    [SerializeField] bool m_is_another_missile = false;

    // ------- Prevent nested missiles -------
    Vector2 m_missile_detect_ray_pos;
    RaycastHit2D m_another_missile_hit;
    int m_another_missile_layermask;

    // IDLE 관련
    public string current_direction;
    public bool   is_lost_target;
    [SerializeField] bool m_is_targeted;

    // ------- Enemy variables -------
    public Enemy_core     enemy_core;

    private void Start()
    {
        Init_settings();
    }

    void Update()
    {
        Set_target();
        Update_missile_state();
    }

    private void OnDisable()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        m_is_targeted = false;

        if (m_missile_idle)
            Destroy(m_missile_idle);

        if (m_missile_attack)
            Destroy(m_missile_attack);

        if (enemy_core)
            enemy_core = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string collided_obj_tag = other.gameObject.tag;

        if (collided_obj_tag == "Enemy" ||
            collided_obj_tag == "Wall")
            gameObject.SetActive(false);
    }

    // 변수 초기화
    void Init_settings()
    {
        // 미사일 충돌 방지
        m_another_missile_layermask = Global.Get_raycast_layermask_index("Item");

        // 미사일끼리 충돌 방지
        m_missile_detect_ray_pos.x = 0f; m_missile_detect_ray_pos.y = 1f; // |
    }

    // 미사일 목표물 지정
    void Set_target()
    {
        if (enemy_core &&
            enemy_core.isActiveAndEnabled)
            Set_attack_property();

        else
            Set_idle_property();
    }

    // Set missile state
    void Update_missile_state()
    {
        if (m_is_targeted) // 이동 중일 시
            m_missile_attack.Attack_enemy_with_missile();

        else // 대기일 시
            enemy_core = Enemy_info_manager.instance.Get_enemy_info();
    }

    // Checking for another missile
    bool Check_for_another_missile()
    {
        m_another_missile_hit = Physics2D.Raycast(m_current_pos, m_missile_detect_ray_pos, Mathf.Infinity, m_another_missile_layermask);

        if (m_another_missile_hit.collider) // 타겟이 있을시
        {
            if (m_another_missile_hit.collider.tag == "Player_missile")
            {
                Set_idle_property();
                return true;
            }
        }
        return false;
    }

    // Set idle resource
    void Set_idle_property()
    {
        if (m_missile_attack)
        {
            Destroy(m_missile_attack);
            is_lost_target = true;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        if (!m_missile_idle)
            m_missile_idle = gameObject.AddComponent<Player_missile_idle>();

        m_is_targeted = false;
    }

    // Set attack resource
    void Set_attack_property()
    {
        if (m_missile_idle)
            Destroy(m_missile_idle);

        if (!m_missile_attack)
            m_missile_attack = gameObject.AddComponent<Player_missile_attack>();

        m_is_targeted = true;
        is_lost_target = false;
    }

    // 미사일들끼리 충돌 방지
    IEnumerator IE_touching_another_missile()
    {
        m_is_another_missile = true;
        yield return new WaitForSeconds(0.5f);
        m_is_another_missile = false;
    }
}