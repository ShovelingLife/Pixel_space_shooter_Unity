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
            Debug.Log($"충돌 {plane_state}");
        }
    }
}
