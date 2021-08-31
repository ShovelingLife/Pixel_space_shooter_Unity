using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet_core : MonoBehaviour
{
    protected float m_dmg = 0f;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Shield")
            gameObject.SetActive(false);

        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Player_manager.instance.current_hp_prop -= m_dmg;
            Player_manager.instance.Character_dmg_dealed("enemy_bullet");
        }
    }
}