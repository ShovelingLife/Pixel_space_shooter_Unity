using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_restore_item : Power_up_item_core
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        m_fall_speed    = Player_manager.instance.health_restore_data.power_up_fall_speed;
        m_rotate_degree = Player_manager.instance.health_restore_data.rotate_degree;
        m_type          = typeof(Health_restore_item);
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
            Player_manager player_manager = Player_manager.instance;

            if (player_manager.current_hp_prop < player_manager.player_stat_data.max_hp)
                player_manager.current_hp_prop += player_manager.health_restore_data.restore_health;

            Audio_manager.instance.power_up_sound.Play_get_health_item_sound();
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "South_wall")
            base.OnTriggerEnter2D(other);
    }
}