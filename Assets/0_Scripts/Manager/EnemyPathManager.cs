using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : SingletonLocal<EnemyPathManager>
{
    public EnemyPath[] paths = null;

    void Start()
    {
        paths = new EnemyPath[transform.childCount];

        for (int i = 0; i < paths.Length ; i++)
            paths[i] = transform.GetChild(i).GetComponent<EnemyPath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
