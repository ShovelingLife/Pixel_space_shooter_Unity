using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_small_bullet : EnemyBulletCore
{
    public EnemyBulletData bullet_data;


    void Update()
    {
        Move();
    }

    // Moving down
    void Move()
    {
        Vector2 new_pos = this.transform.position;
        new_pos.y -= bullet_data.speed * Time.deltaTime;
        this.transform.position = new_pos;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        dmg = StatManager.inst.enemyBulletStatData.dmg;
        base.OnTriggerEnter2D(other);
    }
}