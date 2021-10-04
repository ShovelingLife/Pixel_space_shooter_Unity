using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missile_power_up_UI : MonoBehaviour
{
    // 미사일 아이템 활성화
    public void Turn_on_missile_power_up_UI()
    {
        UI_manager.instance.power_up_UI_data.timer_missile_obj.SetActive(true);
        StartCoroutine(Set_missile_power_up_timer());
        Stat_manager.instance.player_power_up_stat.missile_level++;
    }

    // 미사일 아이템 비활성화
    public void Turn_off_missile_power_up_UI()
    {
        UI_manager.instance.power_up_UI_data.timer_missile_obj.SetActive(false);
        StopCoroutine(Set_missile_power_up_timer());
        Stat_manager.instance.player_power_up_stat.missile_level = 0;
    }

    // 미사일 활성화 시간
    IEnumerator Set_missile_power_up_timer()
    {
        Stat_manager stat_manager = Stat_manager.instance;

        while (true)
        {
            UI_manager.instance.power_up_UI_data.timer_missile_obj.GetComponent<Image>().fillAmount = 
                stat_manager.player_power_up_stat.current_missile_power_up_time / stat_manager.player_power_up_stat.missile_power_up_data.power_up_time;

            stat_manager.player_power_up_stat.current_missile_power_up_time -= (Global.default_power_up_ui_time * Time.deltaTime);

            if (stat_manager.player_power_up_stat.current_missile_power_up_time <= 0f) 
                break;

            yield return null;
        }
        Turn_off_missile_power_up_UI();
    }
}