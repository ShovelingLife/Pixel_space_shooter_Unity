using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoreItem : PowerUpItemCore
{
    protected override void Start()
    {
        // 아이템 변수 초기화
        base.Start();
        var data     = playerInst.healthRestoreData;
        fallSpeed    = data.fallSpeed;
        rotateDegree = data.rotateDegree;
        type         = typeof(HealthRestoreItem);
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
            playerInst.CheckRestoreHealth();
            AudioManager.inst.powerUpsound.PlayGetHealthItem();
            base.OnTriggerEnter2D(other);
        }
        if (other.gameObject.name == "South_wall")
            base.OnTriggerEnter2D(other);
    }
}