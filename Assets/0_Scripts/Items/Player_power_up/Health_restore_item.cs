using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_restore_item : MonoBehaviour
{
    public Health_restore_data health_restore_data;
    Vector3 m_rotation_pos = new Vector3();
    public bool test;

    private void Update()
    {
        if (!test) 
            Move_power_up_item();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Player_manager.instance.current_hp_prop < Player_manager.instance.player_stat_data.max_hp)
                Player_manager.instance.current_hp_prop += health_restore_data.restore_health;

            gameObject.SetActive(false);
            Audio_manager.instance.power_up_sound.Play_get_health_item_sound();
        }
    }

    // 파워업 아이템 움직임
    void Move_power_up_item()
    {
        Vector3 current_pos = this.transform.position;
        Quaternion current_rotation = this.transform.rotation;

        current_pos.y-=health_restore_data.power_up_fall_speed*Time.deltaTime;
        m_rotation_pos.x += health_restore_data.rotate_degree * Time.deltaTime;
        current_rotation.eulerAngles = m_rotation_pos;
        this.transform.rotation = current_rotation;
        this.transform.position = current_pos;

        if (this.transform.position.y < -16.5f) gameObject.SetActive(false);
    }
}