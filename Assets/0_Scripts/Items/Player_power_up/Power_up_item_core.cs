using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 파워업 아이템 코어
public class Power_up_item_core : MonoBehaviour
{
    protected Stat_manager m_stat_inst        = null;
    protected Type         m_type             = null;
    protected Transform    m_parent           = null;
    protected Vector3      m_current_pos      = Vector3.zero;
    protected Quaternion   m_current_rotation = Quaternion.identity;
    protected float        m_fall_speed       = 0f;
    protected float        m_rotate_degree    = 0f;
    public    bool         is_test            = false;


    protected virtual void Start()
    {
        m_stat_inst        = Stat_manager.instance;
        m_current_rotation = transform.rotation;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!is_test) 
            Move_item();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 혹은 남쪽 벽과 충돌 시 제거
        Object_pooling_manager.instance.Remove_obj(m_type, transform, m_parent);
    }

    // 아이템을 움직임
    void Move_item()
    {
        m_current_pos         = transform.localPosition;
        m_current_rotation    = transform.localRotation;
        m_current_rotation.y -= m_rotate_degree * Time.deltaTime;
        m_current_pos.y      -= m_fall_speed * Time.deltaTime;

        transform.localRotation = m_current_rotation;
        transform.localPosition = m_current_pos;
    }
}
