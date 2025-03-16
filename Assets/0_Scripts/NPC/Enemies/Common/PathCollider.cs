using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollider : MonoBehaviour
{
    public EPlaneState planeState;


    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyCore core = collision.GetComponent<EnemyCore>();
                EnemyTypeGreenOne enemyTypeGreenOne = collision.GetComponent<EnemyTypeGreenOne>();

                if (planeState != EPlaneState.SHOOTING)
                    core.EnemyInclining(planeState);
            }
        }
    }
}
