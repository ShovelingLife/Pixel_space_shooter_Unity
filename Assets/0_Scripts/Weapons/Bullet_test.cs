using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_test : MonoBehaviour
{
    Vector3 m_current_pos;
    [SerializeField] float m_move_speed=7.5f;
    [SerializeField] float m_frequency=5f;
    [SerializeField] float m_magnitude=1f;
    float m_missile_dmg = 2f;

    private void Start()
    {
        m_current_pos = transform.localPosition;
    }

    private void Update()
    {
        Move_down();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
            gameObject.SetActive(false);

        else if (collision.tag == "Player")
        {
            collision.GetComponent<Player_manager>().current_hp_prop -= m_missile_dmg;
            gameObject.SetActive(false);
        }
    }

    // Moving down
    void Move_down()
    {
        m_current_pos += Vector3.down * Time.deltaTime * m_move_speed;
        transform.localPosition = m_current_pos + Vector3.left * Mathf.Sin(Time.time * m_frequency) * m_magnitude;
    }
}