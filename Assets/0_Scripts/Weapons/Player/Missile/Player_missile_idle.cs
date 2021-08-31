using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_missile_idle : MonoBehaviour
{
    // ------- Variables about idle -------
    Vector2 m_center_pos;
    float m_rotation_radius = 1.5f, 
          m_angular_speed   = 2.5f;
    float m_posX, 
          m_posY, 
          m_angle = 0f;

    float m_current_time = 0f;
    float m_cooltime     = 2.5f;
    int   m_count        = 0;


    private void OnEnable()
    {
        m_center_pos = new Vector2(transform.localPosition.x-1.5f, transform.localPosition.y);
    }

    private void OnDisable()
    {
        m_count = 0;
    }

    // Rotate in circle
    private void Update()
    {
        Player_missile missile = GetComponent<Player_missile>();

        // 두바퀴 돌 시 사라짐
        if (m_count == 2 ||
            !missile.is_lost_target)
            No_enemy();

        else
            Checking_enemy();
    }

    // Go up when no enemy detected or lost
    void No_enemy()
    {
        Vector2 current_pos     = transform.localPosition;
        current_pos.y           += Stat_manager.instance.power_up_stat.missile_power_up_data.missile_speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = current_pos;
    }

    // Idle movement
    void Checking_enemy()
    {
        m_posX                  = m_center_pos.x + Mathf.Cos(m_angle) * m_rotation_radius;
        m_posY                  = m_center_pos.y + Mathf.Sin(m_angle) * m_rotation_radius;
        transform.localPosition = new Vector2(m_posX, m_posY);
        m_angle                 += m_angular_speed * Time.deltaTime;

        // Rotate
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + (Vector3.forward * 140f * Time.deltaTime));

        m_current_time += Time.deltaTime;

        if (m_current_time >= m_cooltime)
        {
            m_current_time = 0f;
            m_count++;
        }
        if (m_angle >= 360f)
            m_angle = 0f;        
    }
}