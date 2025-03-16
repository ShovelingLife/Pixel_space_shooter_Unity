using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTest : MonoBehaviour
{
    public EEnemyPathType pathType;
    EnemyPath             path;
    public EEnemyWaypoint waypoint;
    float                 range;
    public float          speed;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        path = EnemyInfoManager.inst.enemyPaths[pathType];
        range += (speed * Time.deltaTime);

        switch (waypoint)
        {
            case EEnemyWaypoint.FIRST:  transform.position = path.GetFirstPath(range); break;

            case EEnemyWaypoint.SECOND: transform.position = path.GetSecondPath(range); break;

            case EEnemyWaypoint.THIRD:
                waypoint = 0;
                break;
        }
        if (range > 1f)
        {
            range = 0f;
            waypoint++;
        }
    }
}
