using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePowerUpItem : PowerUpItemCore
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        var data     = statInst.playerPowerUpStat.missilePowerUpData;
        fallSpeed    = data.fallSpeed;
        rotateDegree = data.rotateDegree;
        type         = typeof(MissilePowerUpItem);
        parent       = transform.parent;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (statInst.playerPowerUpStat.missileLvl == 2)
                return;

            statInst.playerPowerUpStat.SetMissilePowerUpTime();
            AudioManager.inst.powerUpsound.PlayGetMissileItemSound();
            UI_manager.inst.missilePowerUpUI.TurnOn();
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "South_wall")
            base.OnTriggerEnter2D(other);
    }
}