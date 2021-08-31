using UnityEngine;

public class Player_missile_attack : MonoBehaviour
{
    float m_turn_speed    = 10f;
    float m_missile_speed = 5f;


    // 미사일로 적을 공격   
    public void Attack_enemy_with_missile()
    {
        Vector2 current_pos   = transform.localPosition;
        Vector2 enemy_pos     = GetComponent<Player_missile>().enemy_core.transform.localPosition;
        Vector2 direction_pos = current_pos - enemy_pos;

        // Rotate
        float angle             = (Mathf.Atan2(direction_pos.y, direction_pos.x) * Mathf.Rad2Deg) + 90f;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f, 0f, angle), m_turn_speed * Time.deltaTime);

        // Move
        transform.localPosition = Vector2.MoveTowards(current_pos, enemy_pos, m_missile_speed * Time.deltaTime);
    }
}