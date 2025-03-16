using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedUpItem : PowerUpItemCore
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        fallSpeed    = statInst.playerPowerUpStat.bulletSpeedUpData.fallSpeed;
        rotateDegree = statInst.playerPowerUpStat.bulletSpeedUpData.rotateDegree;
        type         = typeof(BulletSpeedUpItem);
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
            if (statInst.playerPowerUpStat.speedUpLvl > 0)
                return;

            statInst.playerPowerUpStat.SetBulletSpeedUpTime();
            AudioManager.inst.powerUpsound.PlayGetSpeedUpItem();
            UI_manager.inst.bulletSpeedUpUI.TurnOn();
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "SouthWall")
            base.OnTriggerEnter2D(other);
    }
}