using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield_power_up_UI : MonoBehaviour
{
    // 보호막 아이템 활성화
    public void Turn_on_shield_power_up_UI()
    {
        UI_manager.instance.power_up_UI.timer_shield_obj.SetActive(true);
        StartCoroutine(Set_shield_power_up_timer());
        Player_manager.instance.player_power_up_info.shield_level = 1;
        Player_manager.instance.player_power_up_info.is_shield_created = true;
    }

    // 보호막 아이템 비활성화
    public void Turn_off_shield_power_up_UI()
    {
        UI_manager.instance.power_up_UI.timer_shield_obj.SetActive(false);
        StopCoroutine(Set_shield_power_up_timer());
        Player_manager.instance.player_power_up_info.shield_level = 0;
        Player_manager.instance.player_power_up_info.is_shield_created = false;
    }

    // 보호막 아이템 활성화 시간
    IEnumerator Set_shield_power_up_timer()
    {
        Stat_manager stat_manager = Stat_manager.instance;

        while (true)
        {
            UI_manager.instance.power_up_UI.timer_shield_obj.GetComponent<Image>().fillAmount =
                stat_manager.power_up_stat.current_shield_power_up_time / stat_manager.power_up_stat.shield_power_up_data.power_up_time;

            stat_manager.power_up_stat.current_shield_power_up_time -= (Global.default_power_up_ui_time * Time.deltaTime);

            if (stat_manager.power_up_stat.current_shield_power_up_time <= 0f) 
                break;

            yield return null;
        }
        Turn_off_shield_power_up_UI();
    }
}