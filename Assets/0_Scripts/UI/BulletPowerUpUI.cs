using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPowerUpUI : MonoBehaviour
{
    // 발사 공격 증가 아이템 활성화
    public void TurnOn()
    {
        UI_manager.inst.powerUpUI_data.timer_bullet_power_up_obj.SetActive(true);
        StartCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.powerUpLvl++;
    }

    // 발사 공격 증가 아이템 비활성화
    public void TurnOff()
    {
        UI_manager.inst.powerUpUI_data.timer_bullet_power_up_obj.SetActive(false);
        StopCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.powerUpLvl = 0;
    }

    // 보호막 아이템 활성화 시간
    IEnumerator SetTimer()
    {
        StatManager statManager = StatManager.inst;

        while (true)
        {
            UI_manager.inst.powerUpUI_data.timer_bullet_power_up_obj.GetComponent<Image>().fillAmount = 
                statManager.playerPowerUpStat.curBulletPowerUpTime / statManager.playerPowerUpStat.bulletPowerUpData.time;

            statManager.playerPowerUpStat.curBulletPowerUpTime -= (Global.DefaultPowerUpTime * Time.deltaTime);

            if (statManager.playerPowerUpStat.curBulletPowerUpTime <= 0f) 
                break;

            yield return null;
        }
        TurnOff();
    }
}