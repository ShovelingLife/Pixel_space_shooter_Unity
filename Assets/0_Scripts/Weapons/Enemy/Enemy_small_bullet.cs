using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_small_bullet : Enemy_bullet_core
{
    public Enemy_bullet_data bullet_data;


    void Update()
    {
        Move();
    }

    // Moving down
    void Move()
    {
        Vector2 new_pos = this.transform.position;
        new_pos.y -= bullet_data.bullet_speed * Time.deltaTime;
        this.transform.position = new_pos;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        m_dmg = Stat_manager.instance.enemy_bullet_stat_data.bullet_dmg;
        base.OnTriggerEnter2D(other);
    }
}