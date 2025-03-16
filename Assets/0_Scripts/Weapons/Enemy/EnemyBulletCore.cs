using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCore : MonoBehaviour
{
    protected float dmg = 0f;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Shield")
            gameObject.SetActive(false);

        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerManager.inst.CurHp -= dmg;
            PlayerManager.inst.CharacterDmgDealed("enemyBullet");
        }
    }
}