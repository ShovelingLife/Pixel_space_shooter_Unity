using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_speed_up_UI : MonoBehaviour
{
    // 발사 속도 증가 아이템 활성화
    public void Turn_on_bullet_speed_up_UI()
    {
        UI_manager.instance.power_up_UI.timer_bullet_speed_up_obj.SetActive(true);
        Player_manager.instance.player_power_up_info.is_booster_on = true;
        StartCoroutine(Set_bullet_speed_up_timer());
        Player_manager.instance.player_power_up_info.speed_up_level = 1;
    }

    // 발사 속도 증가 아이템 비활성화
    public void Turn_off_bullet_speed_up_UI()
    {
        UI_manager.instance.power_up_UI.timer_bullet_speed_up_obj.SetActive(false);
        StopCoroutine(Set_bullet_speed_up_timer());
        Player_manager.instance.player_power_up_info.is_booster_on = false;
        Player_manager.instance.player_power_up_info.speed_up_level = 0;
    }

    // 발사 속도 아이템 활성화 시간
    IEnumerator Set_bullet_speed_up_timer()
    {
        Stat_manager stat_manager = Stat_manager.instance;

        while (true)
        {
            UI_manager.instance.power_up_UI.timer_bullet_speed_up_obj.GetComponent<Image>().fillAmount = 
                stat_manager.power_up_stat.current_bullet_speed_up_time / stat_manager.power_up_stat.bullet_speed_up_data.power_up_time;

            stat_manager.power_up_stat.current_bullet_speed_up_time -= (Global.default_power_up_ui_time * Time.deltaTime);

            if (stat_manager.power_up_stat.current_bullet_speed_up_time <= 0f) 
                break;

            yield return null;
        }
        Turn_off_bullet_speed_up_UI();
    }
}