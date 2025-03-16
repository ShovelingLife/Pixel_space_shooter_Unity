using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissilePowerUpUI : MonoBehaviour
{
    // 미사일 아이템 활성화
    public void TurnOn()
    {
        UI_manager.inst.powerUpUI_data.timer_missile_obj.SetActive(true);
        StartCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.missileLvl++;
    }

    // 미사일 아이템 비활성화
    public void TurnOff()
    {
        UI_manager.inst.powerUpUI_data.timer_missile_obj.SetActive(false);
        StopCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.missileLvl = 0;
    }

    // 미사일 활성화 시간
    IEnumerator SetTimer()
    {
        StatManager statManager = StatManager.inst;

        while (true)
        {
            UI_manager.inst.powerUpUI_data.timer_missile_obj.GetComponent<Image>().fillAmount = 
                statManager.playerPowerUpStat.curMissilePowerUpTime / statManager.playerPowerUpStat.missilePowerUpData.time;

            statManager.playerPowerUpStat.curMissilePowerUpTime -= (Global.DefaultPowerUpTime * Time.deltaTime);

            if (statManager.playerPowerUpStat.curMissilePowerUpTime <= 0f) 
                break;

            yield return null;
        }
        TurnOff();
    }
}