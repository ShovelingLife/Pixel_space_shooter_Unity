using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerUpItem : PowerUpItemCore
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        fallSpeed    = statInst.playerPowerUpStat.shieldPowerUpData.fallSpeed;
        rotateDegree = statInst.playerPowerUpStat.shieldPowerUpData.rotateDegree;
        type         = typeof(BulletPowerUpItem);
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
            if (StatManager.inst.playerPowerUpStat.powerUpLvl == 2)
                return;

            statInst.playerPowerUpStat.SetBulletPowerUpTime();
            AudioManager.inst.powerUpsound.PlayGetPowerUpItem();
            UI_manager.inst.bulletPowerUpUI.TurnOn();
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "SouthWall")
            base.OnTriggerEnter2D(other);
    }
}