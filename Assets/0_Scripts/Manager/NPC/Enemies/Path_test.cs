using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_test : MonoBehaviour
{
    public e_enemy_path_type path_type;
    Enemy_path               path;
    public e_enemy_waypoint  waypoint;
    float                    m_range;
    public float             speed;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        path = Position_manager.instance.d_enemy_path[path_type];
        m_range += (speed * Time.deltaTime);

        switch (waypoint)
        {
            case e_enemy_waypoint.FIRST:  transform.position = path.Get_first_path(m_range); break;

            case e_enemy_waypoint.SECOND: transform.position = path.Get_second_path(m_range); break;

            case e_enemy_waypoint.THIRD:
                waypoint = 0;
                break;
        }
        if(m_range>1f)
        {
            m_range = 0f;
            waypoint++;
        }
    }
}
