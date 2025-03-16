using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_bullet_type1 : MonoBehaviour
{
    public Vector3 current_pos;
    public Vector3 direction_pos;
    [SerializeField] float m_move_speed=7.5f;
    [SerializeField] float m_frequency=5f;
    [SerializeField] float m_magnitude=1f;
    float m_missile_dmg = 2f;

    private void Update()
    {
        Move_down();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Boss_enemy_core boss_enemy_core = transform.parent.GetComponent<Boss_enemy_core>();

        if (collision.tag == "Wall")
            ObjectPoolingManager.inst.RemoveObj(typeof(Boss_bullet_type1), transform);

        else if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerManager>().CurHp -= 2f;
            ObjectPoolingManager.inst.RemoveObj(typeof(Boss_bullet_type1), transform);
        }
    }

    // Moving down
    void Move_down()
    {
        current_pos += (direction_pos * Time.deltaTime * m_move_speed);
        transform.localPosition = current_pos + Vector3.left * Mathf.Sin(Time.time * m_frequency) * m_magnitude;
    }
}