using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    // 플레이어 총알 관련
    public Player_bullet_data player_bullet_data;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(Vector2.up * player_bullet_data.bullet_speed * Time.deltaTime);
    }

    // 충돌시 총알 반환
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall" ||
            other.gameObject.tag == "Enemy")
            Object_pooling_manager.instance.Remove_obj(typeof(Player_bullet), transform);
    }
}