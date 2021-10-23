using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_power_up_item : Power_up_item_core
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        m_fall_speed    = m_stat_inst.player_power_up_stat.shield_power_up_data.power_up_fall_speed;
        m_rotate_degree = m_stat_inst.player_power_up_stat.shield_power_up_data.rotate_degree;
        m_type          = typeof(Shield_power_up_item);
        m_parent        = transform.parent;
        // 리스트 초기화
        //m_list_fn.Add(m_stat_inst.player_power_up_stat.Set_shield_power_up_time);
        //m_list_fn.Add(Audio_manager.instance.power_up_sound.Play_get_shield_item_sound);
        //m_list_fn.Add(UI_manager.instance.shield_power_up_ui.Turn_on_shield_power_up_UI);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_stat_inst.player_power_up_stat.shield_level > 0)
                return;

            m_stat_inst.player_power_up_stat.Set_shield_power_up_time();
            Audio_manager.instance.power_up_sound.Play_get_shield_item_sound();
            UI_manager.instance.shield_power_up_ui.Turn_on_shield_power_up_UI();
            m_stat_inst.player_power_up_stat.is_shield_created = true;
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "South_wall")
            base.OnTriggerEnter2D(other);
    }
}