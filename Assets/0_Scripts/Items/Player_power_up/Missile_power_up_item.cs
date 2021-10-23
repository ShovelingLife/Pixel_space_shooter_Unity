using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_power_up_item : Power_up_item_core
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        m_fall_speed    = m_stat_inst.player_power_up_stat.missile_power_up_data.power_up_fall_speed;
        m_rotate_degree = m_stat_inst.player_power_up_stat.missile_power_up_data.rotate_degree;
        m_type          = typeof(Missile_power_up_item);
        m_parent        = transform.parent;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_stat_inst.player_power_up_stat.missile_level == 2)
                return;

            m_stat_inst.player_power_up_stat.Set_missile_power_up_time();
            Audio_manager.instance.power_up_sound.Play_get_missile_item_sound();
            UI_manager.instance.missile_power_up_ui.Turn_on_missile_power_up_UI();
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "South_wall")
            base.OnTriggerEnter2D(other);
    }
}