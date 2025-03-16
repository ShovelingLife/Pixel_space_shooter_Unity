using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPowerUpUI : MonoBehaviour
{
    // 보호막 아이템 활성화
    public void TurnOn()
    {
        UI_manager.inst.powerUpUI_data.timer_shield_obj.SetActive(true);
        StartCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.shieldLvl = 1;
        StatManager.inst.playerPowerUpStat.isShieldCreated = true;
    }

    // 보호막 아이템 비활성화
    public void TurnOff()
    {
        UI_manager.inst.powerUpUI_data.timer_shield_obj.SetActive(false);
        StopCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.shieldLvl = 0;
        StatManager.inst.playerPowerUpStat.isShieldCreated = false;
    }

    // 보호막 아이템 활성화 시간
    IEnumerator SetTimer()
    {
        StatManager statManager = StatManager.inst;

        while (true)
        {
            UI_manager.inst.powerUpUI_data.timer_shield_obj.GetComponent<Image>().fillAmount =
                statManager.playerPowerUpStat.curShieldPowerUpTime / statManager.playerPowerUpStat.shieldPowerUpData.time;

            statManager.playerPowerUpStat.curShieldPowerUpTime -= (Global.DefaultPowerUpTime * Time.deltaTime);

            if (statManager.playerPowerUpStat.curShieldPowerUpTime <= 0f) 
                break;

            yield return null;
        }
        TurnOff();
    }
}