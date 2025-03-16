using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSpeedUpUI : MonoBehaviour
{
    // 발사 속도 증가 아이템 활성화
    public void TurnOn()
    {
        UI_manager.inst.powerUpUI_data.timer_bullet_speed_up_obj.SetActive(true);
        StatManager.inst.playerPowerUpStat.isBoosterOn = true;
        StartCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.speedUpLvl = 1;
    }

    // 발사 속도 증가 아이템 비활성화
    public void TurnOff()
    {
        UI_manager.inst.powerUpUI_data.timer_bullet_speed_up_obj.SetActive(false);
        StopCoroutine(SetTimer());
        StatManager.inst.playerPowerUpStat.isBoosterOn = false;
        StatManager.inst.playerPowerUpStat.speedUpLvl = 0;
    }

    // 발사 속도 아이템 활성화 시간
    IEnumerator SetTimer()
    {
        StatManager statManager = StatManager.inst;

        while (true)
        {
            UI_manager.inst.powerUpUI_data.timer_bullet_speed_up_obj.GetComponent<Image>().fillAmount = 
                statManager.playerPowerUpStat.curBulletSpeedUpTime / statManager.playerPowerUpStat.bulletSpeedUpData.time;

            statManager.playerPowerUpStat.curBulletSpeedUpTime -= (Global.DefaultPowerUpTime * Time.deltaTime);

            if (statManager.playerPowerUpStat.curBulletSpeedUpTime <= 0f) 
                break;

            yield return null;
        }
        TurnOff();
    }
}