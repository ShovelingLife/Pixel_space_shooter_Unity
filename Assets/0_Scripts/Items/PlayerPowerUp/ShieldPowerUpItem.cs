using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpItem : PowerUpItemCore
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        var powerUpStatData = statInst.playerPowerUpStat.shieldPowerUpData;
        fallSpeed    = powerUpStatData.fallSpeed;
        rotateDegree = powerUpStatData.rotateDegree;
        type         = GetType();
        parent       = transform.parent;
        // 리스트 초기화
        //m_list_fn.Add(m_stat_inst.player_power_up_stat.Set_shield_power_up_time);
        //m_list_fn.Add(Audio_manager.instance.power_up_sound.Play_get_shield_item_sound);
        //m_list_fn.Add(UI_manager.instance.shield_power_up_ui.Turn_on_shield_power_up_UI);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (statInst.playerPowerUpStat.shieldLvl > 0)
                return;

            statInst.playerPowerUpStat.SetShieldPowerUpTime();
            AudioManager.inst.powerUpsound.PlayGetShieldItemSound();
            UI_manager.inst.shieldPowerUpUI.TurnOn();
            statInst.playerPowerUpStat.isShieldCreated = true;
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "South_wall")
            base.OnTriggerEnter2D(other);
    }
}