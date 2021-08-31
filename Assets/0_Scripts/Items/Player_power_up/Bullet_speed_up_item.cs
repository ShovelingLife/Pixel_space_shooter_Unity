using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_speed_up_item : MonoBehaviour
{
    Vector3 m_rotation_pos = new Vector3();
    public bool test;


    private void Update()
    {
        Move_power_up_item();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Player_manager.instance.player_power_up_info.speed_up_level > 0)
                return;

            gameObject.SetActive(false);
            Audio_manager.instance.power_up_sound.Play_get_speed_up_item_sound();
            Stat_manager.instance.power_up_stat.Set_bullet_speed_up_time();
            UI_manager.instance.bullet_speed_up_ui.Turn_on_bullet_speed_up_UI();
        }
    }

    // 파워업 아이템 움직임
    void Move_power_up_item()
    {
        Vector3 current_pos         = this.transform.position;
        Quaternion current_rotation = this.transform.rotation;

        current_pos.y                -= Stat_manager.instance.power_up_stat.bullet_speed_up_data.power_up_fall_speed * Time.deltaTime;
        m_rotation_pos.x             += Stat_manager.instance.power_up_stat.bullet_speed_up_data.rotate_degree * Time.deltaTime;
        current_rotation.eulerAngles = m_rotation_pos;
        this.transform.rotation      = current_rotation;
        this.transform.position      = current_pos;

        if (this.transform.position.y < Global.game_bottom_pos) 
            gameObject.SetActive(false);
    }
}