using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_path_manager : Singleton_local<Enemy_path_manager>
{
    public Enemy_path[] arr_path = null;

    void Start()
    {
        arr_path = new Enemy_path[transform.childCount];

        for (int i = 0; i < arr_path.Length ; i++)
        {
            arr_path[i] = transform.GetChild(i).GetComponent<Enemy_path>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
