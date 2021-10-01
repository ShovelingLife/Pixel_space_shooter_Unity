using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_manager : Singleton_local<Position_manager>
{
    public Dictionary<e_enemy_path_type, Enemy_path> d_enemy_path = new Dictionary<e_enemy_path_type, Enemy_path>();


    void Start()
    {
        // 겹침 방지용
        foreach (var item in d_enemy_path)
        {
            item.Value.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
