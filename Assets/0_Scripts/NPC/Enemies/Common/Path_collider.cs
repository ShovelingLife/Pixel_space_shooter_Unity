using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_collider : MonoBehaviour
{
    public e_plane_state plane_state;


    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy_core core = collision.GetComponent<Enemy_core>();
                Enemy_type_green_one enemy_type_green_one = collision.GetComponent<Enemy_type_green_one>();

                if (plane_state != e_plane_state.SHOOTING)
                    core.Enemy_inclining(plane_state);

                else // 총알 트리거 발동
                {
                    if (enemy_type_green_one != null)
                        StartCoroutine(enemy_type_green_one.IE_enemy_shoot_bullet());
                }
            }
        }
    }
}
